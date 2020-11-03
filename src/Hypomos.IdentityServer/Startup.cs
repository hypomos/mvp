// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Hypomos.IdentityServer
{
    using System.Linq;
    using System.Reflection;
    using Hypomos.IdentityServer.Middleware;
    using IdentityServer4.EntityFramework.DbContexts;
    using IdentityServer4.EntityFramework.Mappers;
    using IdentityServer4.Services;
    using IdentityServerHost.Quickstart.UI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.Environment = environment;
            this.Configuration = configuration;
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.Configure<ForwardedHeadersOptions>(
                options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                });

            services.AddAuthentication()
                .AddMicrosoftAccount(
                    "Microsoft",
                    options =>
                    {
                        this.Configuration.GetSection("MicrosoftAccountOptions")
                            .Bind(options);
                    });

            var migrationsAssembly = typeof(Startup).GetTypeInfo()
                .Assembly.GetName()
                .Name;
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");

            var builder = services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = "https://localhost:5101/identity-server";
                        // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                        // options.EmitStaticAudienceClaim = true;
                    })
                .AddTestUsers(TestUsers.Users)
                .AddOperationalStore(
                    options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    })
                .AddConfigurationStore(
                    options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    });

            services.AddSingleton<ICorsPolicyService>(
                provider =>
                {
                    var logger = provider.GetService<ILogger<DefaultCorsPolicyService>>();

                    return new DefaultCorsPolicyService(logger)
                    {
                        AllowAll = true
                    };
                });

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<HttpInspectorMiddleware>();

            app.UseCookiePolicy();
            app.UseForwardedHeaders();
            app.UsePathBase("/identity-server");

            if (this.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            this.InitializeDatabase(app);

            app.Use(
                async (context, next) =>
                {
                    context.Request.Scheme = "https";
                    context.Request.PathBase = "/identity-server";
                    context.Request.Host = new HostString("localhost", 5101);

                    await next.Invoke();
                });

            app.UseIdentityServer();

            app.UseRouting()
                .UseCors()
                .UseAuthentication()
                .UseAuthorization()
                .UseStaticFiles()
                .UseEndpoints(
                    builder =>
                    {
                        builder.MapDefaultControllerRoute();
                    });
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("Initliazing DB...");
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>()
                .Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                logger.LogInformation("No Clients found, thus initialize with Config.Clients");
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                logger.LogInformation("No IdentityResources found, thus initializing...");
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                logger.LogInformation("No APIs found, thus initializing...");
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
        }
    }
}