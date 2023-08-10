using HospitalManagement.BusinessLogic.Enums;
using HospitalManagement.BusinessLogic.Services;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HospitalManagement.Api.Endpoints
{
    public static class AdminEndPoints
    {
        public static void AddAdminEndPoints(this WebApplication app)
        {
            app.MapPost("/Admin/AddUser", async (IHospitalManagementData data, [FromBody] UserModel user) =>
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
            .RequireAuthorization(AuthorizationPollicesEnum.AdminPanelAccess.ToString());

            app.MapPost("/Admin/EditUser", async (IHospitalManagementData data, [FromBody] UserModel user) =>
            {
                return await AdminEndPointMap.EditUser(user,data);
            })
            .WithMetadata(new SwaggerOperationAttribute("Update User", @"{
                    ""firstName"": ""string"",
                    ""lastName"": ""string"",
                    ""email"": ""string"",
                    ""role"": 1,
                    ""password"": ""string"",
                    ""confirmPassword"": ""string""
            }"))
            .RequireAuthorization(AuthorizationPollicesEnum.AdminPanelAccess.ToString());

            app.MapPost("/Admin/DeleteUser", async (IHospitalManagementData data, string email) =>
            {
                return await AdminEndPointMap.DeleteUser(email, data);
            })
            .WithMetadata(new SwaggerOperationAttribute("Delete User", "User will delete"))
            .RequireAuthorization(AuthorizationPollicesEnum.AdminPanelAccess.ToString());
        }
    }
}
