// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hypomos.IdentityServer
{
    using Hypomos.IdentityServer.Quickstart;
    using IdentityServer4;
    using IdentityServer4.Services;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        } 

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICorsPolicyService>(provider => 
                new DefaultCorsPolicyService(provider.GetService<ILogger<DefaultCorsPolicyService>>())
            {
                AllowedOrigins = { "http://localhost:5010", "http://localhost:3000" }
            });

            services.AddAuthentication()
                .AddMicrosoftAccount("Microsoft", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "66b69578-2ab1-4d0e-ac50-041bfa4efc50";
                    options.ClientSecret = "wj874ezZOoo1ReLQAml/EHRG/WvIJ:c.";

                    //options.Scope.Add("id_token");
                });

            // uncomment, if you want to add an MVC-based UI
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(Config.Ids)
                .AddInMemoryApiResources(Config.Apis)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users);

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment if you want to add MVC
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseIdentityServer(new IdentityServerMiddlewareOptions
            {
                AuthenticationMiddleware = builder => builder.UseCors(b => b.AllowAnyOrigin())
            });

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
