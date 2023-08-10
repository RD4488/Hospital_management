using HospitalManagement.BusinessLogic.Constants;
using HospitalManagement.BusinessLogic.Models;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HospitalManagement.BusinessLogic.Services
{
    public class AuthenticationEndPointMap
    {
        public static async Task<CommonResponse> Authenticate(HttpContext httpContext, IConfiguration config, IHospitalManagementData data, string userName, string password)
        {
            var user = await ValidateCredentitals(data, userName, password);

            if (user is null)
            {
                return new CommonResponse
                {
                    Data = null,
                    Message = MessageConstants.UnauthorizedUser,
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Success = false
                };
            }
            var token = GenerateToken(config, user);
            httpContext.Session.Set("token", Encoding.ASCII.GetBytes(token));
            string message = $"{DateTime.Now} : {user.FirstName} SignIn Successfully";
            message.WriteDataInTextFile();
            return new CommonResponse
            {
                Data = token,
                Message = MessageConstants.AuthorizedUser,
                StatusCode = (int)HttpStatusCode.OK,
                Success = true
            };
        }

        private static string GenerateToken(IConfiguration config, UserModel user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetValue<string>("Authentication:SecretKey")));

            var singingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.Email));
            claims.Add(new(JwtRegisteredClaimNames.Name, user.FirstName));
            claims.Add(new("Role", user.Role.ToString()));

            var token = new JwtSecurityToken(
                config.GetValue<string>("Authentication:Issuer"),
                config.GetValue<string>("Authentication:Audience"),
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(1),
                singingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private async static Task<UserModel> ValidateCredentitals(IHospitalManagementData data, string userName, string password)
        {
            return await data.CheckCredentials(userName, password);
        }
    }
}
