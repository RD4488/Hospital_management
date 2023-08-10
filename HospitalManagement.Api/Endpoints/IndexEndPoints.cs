using HospitalManagement.BusinessLogic.Enums;
using HospitalManagement.BusinessLogic.Services;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace HospitalManagement.Api.Endpoints
{
    public static class IndexEndPoints
    {
        /// <summary>
        /// End points of signup and signout
        /// </summary>
        /// <param name="app"></param>
        public static void AddIndexEndPoints(this WebApplication app)
        {
            app.MapPost("/SignUp", async (IHospitalManagementData data, [FromBody] UserModel user) =>
            {
                return await SignUpEndPointMap.UserSignUp(user, data);
            })
            .WithMetadata(new SwaggerOperationAttribute("Create User", @"{
                    ""firstName"": ""string"",
                    ""lastName"": ""string"",
                    ""email"": ""string"",
                    ""role"": 1,
                    ""password"": ""string"",
                    ""confirmPassword"": ""string""
            }"))
            .AllowAnonymous();


            app.MapGet("/SignOut", async (HttpContext httpContext) =>
            { 
                return await SignOutEndPointMap.UserSignOut(httpContext);
            })
            .WithMetadata(new SwaggerOperationAttribute("Signout User"))
            .AllowAnonymous();

            
            app.MapGet("/SignIn", async (HttpContext httpContext, IConfiguration config, IHospitalManagementData data, string userName, string password) =>
            {
                return await AuthenticationEndPointMap.Authenticate(httpContext, config, data, userName, password);
            })
            .WithMetadata(new SwaggerOperationAttribute("Authenticate User", "Get Token in Data Field"))
            .AllowAnonymous();
        }
    }
}
