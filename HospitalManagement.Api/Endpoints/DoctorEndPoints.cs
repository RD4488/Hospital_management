using HospitalManagement.BusinessLogic.Enums;
using HospitalManagement.BusinessLogic.Services;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HospitalManagement.Api.Endpoints
{
    public static class DoctorEndPoints
    {
        public static void AddDoctorEndPoints(this WebApplication app)
        {
            app.MapPost("/Doctor/EditUser", async (IHospitalManagementData data, [FromBody] UserModel user) =>
            {
                return await AdminEndPointMap.EditUser(user, data);
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
        }
    }
}
