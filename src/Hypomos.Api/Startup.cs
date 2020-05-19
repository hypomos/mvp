namespace Hypomos.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using Hypomos.Api.Cluster;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ClusterClientHostedService>();
            services.AddSingleton<IHostedService>(_ => _.GetService<ClusterClientHostedService>());
            services.AddSingleton(_ => _.GetService<ClusterClientHostedService>().Client);

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => 
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo {Title = "Hypomos API"});
                var options = swagger;

                var userScheme = new OpenApiSecurityScheme
                {
                    Name = "openid",
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri("http://localhost:5000/.well-known/openid-configuration")
                };

                options.AddSecurityDefinition("user", userScheme);

                options.AddSecurityDefinition("userOauth",
                    new OpenApiSecurityScheme
                    {
                        Name = "userImplicit",
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri("http://localhost:5000/connect/authorize"),
                                Scopes = new Dictionary<string, string>
                                {
                                    {"hypomos", "Hypomos API"}
                                }
                            }
                        },
                    });

                options.AddSecurityDefinition("client",
                    new OpenApiSecurityScheme
                    {
                        Name = "client",
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            ClientCredentials = new OpenApiOAuthFlow
                            {
                                TokenUrl = new Uri("http://localhost:5000/connect/token"),
                                Scopes =
                                {
                                    {"hypomos", "Hypomos API"}
                                }
                            }
                        }
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {userScheme, new List<string> {"hypomos"}}
                });

                options.SchemaFilter<EnumSchemaFilter>();
                options.OperationFilter<AssignOAuth2SecurityRequirements>();
            });
            services.AddSwaggerGenNewtonsoftSupport();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "hypomos";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");

                c.DisplayRequestDuration();
                c.OAuthClientId("swagger-api-id");
                c.OAuthClientSecret("some-even-more-secret-secret");
                c.OAuthRealm("http://localhost:5010");
                c.OAuthAppName("Swagger");
            });


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public sealed class AssignOAuth2SecurityRequirements : IOperationFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Security == null)
            {
                operation.Security = new List<OpenApiSecurityRequirement>();
            }

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "user"
                        }
                    },
                    new List<string>()
                },
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "client"
                        }
                    },
                    new List<string>()
                }
            };

            operation.Security.Add(securityRequirement);
        }
    }

    public sealed class EnumSchemaFilter : ISchemaFilter
    {
        /// <inheritdoc />
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Enum.Clear();
                var enumMembers = context.Type.GetMembers(BindingFlags.Public | BindingFlags.Static);
                foreach (var enumMember in enumMembers.Where(m => m.MemberType == MemberTypes.Field))
                {
                    schema.Enum.Add(new OpenApiString(enumMember.GetCustomAttribute<EnumMemberAttribute>()?.Value));
                }
            }
        }
    }
}