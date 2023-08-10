using HospitalManagement.BusinessLogic.Constants;
using HospitalManagement.BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace HospitalManagement.BusinessLogic.Services
{
    public class SignOutEndPointMap
    {
        public async static Task<CommonResponse> UserSignOut(HttpContext httpContext)
        {
            string message = $"{DateTime.Now} : User SignOut Successfully";
            message.WriteDataInTextFile();
            httpContext.Session.Remove("token");
            return new CommonResponse
            {
                Data = null,
                Message = MessageConstants.UserSignOut,
                StatusCode = (int)HttpStatusCode.OK,
                Success = true
            };
        }
    }
}
