namespace Hypomos.Web
{
    using System.Threading.Tasks;
    using Hypomos.Web.Cluster;
    using Hypomos.Web.Data;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.AzureAD.UI;
    using Microsoft.AspNetCore.Authentication.OpenIdConnect;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ClusterClientHostedService>();
            services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
            services.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddAzureAD(options => this.Configuration.Bind("AzureAd", options))
                .AddCookie();

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, ConfigureOpenIdConnectOptions);

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //var microsoftAccountHandler = app.ApplicationServices.GetService<MicrosoftAccountHandler>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void ConfigureOpenIdConnectOptions(OpenIdConnectOptions options)
        {
            options.TokenValidationParameters =
                new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    NameClaimType = "name"
                };

            options.Events = new OpenIdConnectEvents
            {
                OnTicketReceived = context =>
                {
                    // If your authentication logic is based on users then add your logic here
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    context.Response.Redirect("/Error");
                    context.HandleResponse(); // Suppress the exception
                    return Task.CompletedTask;
                },

                // If your application needs to authenticate single users, add your user validation below.
                OnTokenValidated = context => { return Task.CompletedTask; }
            };
        }
    }
}