using HospitalManagement.BusinessLogic.Constants;
using HospitalManagement.BusinessLogic.Enums;
using HospitalManagement.BusinessLogic.Models;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Enums;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace HospitalManagement.Api.StartUpConfig;

public static class DependancyInjectionExtensions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.AddSwaggerServices();
    }
    private static void AddSwaggerServices(this WebApplicationBuilder builder)
    {
        var securitySchem = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Description = "JWT Authorizaton header info",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        var securityRequirement = new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="bearerAuth"
                    }
                },
                new string[] { }
            }
        };

        // Add FV Rules to swagger
        builder.Services.AddFluentValidationRulesToSwagger();

        builder.Services.AddSwaggerGen(opts =>
        {
            opts.AddSecurityDefinition("bearerAuth", securitySchem);
            opts.AddSecurityRequirement(securityRequirement);
            opts.EnableAnnotations();
            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Hospital Management API",
                Description = "An ASP.NET Core Minimal API for Hotel Management",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        });
    }
    public static void AddCustomeServices(this WebApplicationBuilder builder)
    {
        //Dataaccess
        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        builder.Services.AddSingleton<IHospitalManagementData, HospitalManagementData>();
    }
    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
        //Authorization
        builder.Services.AddAuthorization(opts =>
        {
            opts.AddPolicy(AuthorizationPollicesEnum.AdminPanelAccess.ToString(), policy =>
            {
                policy.RequireClaim("Role", RoleEnum.Admin.ToString());
            });

            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();

        });
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        builder.Configuration.GetValue<string>("Authentication:SecretKey")))
                };

                opts.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.Headers.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new CommonResponse
                        {
                            Data = null,
                            Success = false,
                            Message = MessageConstants.UnauthorizedUser,
                            StatusCode = (int)HttpStatusCode.Unauthorized
                        }));
                    }
                };
            });
    }
}
