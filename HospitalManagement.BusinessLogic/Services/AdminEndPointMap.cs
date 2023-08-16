using FluentValidation.Results;
using HospitalManagement.BusinessLogic.Constants;
using HospitalManagement.BusinessLogic.Models;
using HospitalManagement.BusinessLogic.Validators;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace HospitalManagement.BusinessLogic.Services
{
    public class AdminEndPointMap
    {
        public async static Task<CommonResponse> EditUser(UserModel user, IHospitalManagementData data)
        {
            UserValidator validations = new UserValidator();
            ValidationResult validationResult = validations.Validate(user);

            if (!validationResult.IsValid)
            {
                return new CommonResponse
                {
                    StatusCode = (int)HttpStatusCode.ExpectationFailed,
                    Data = null,
                    Message = MessageConstants.InvalidInputs,
                    Success = false
                };
            }

            var result = await data.UpdateUser(user);

            if (result is null)
            {
                return new CommonResponse
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Data = null,
                    Message = MessageConstants.UserNotExist,
                    Success = true
                };
            }
            
            return new CommonResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = null,
                Message = MessageConstants.UserUpdated,
                Success = true
            };
        }

        public async static Task<CommonResponse> DeleteUser(string email, IHospitalManagementData data)
        {   
            if (!IsValid(email))
            {
                return new CommonResponse
                {
                    StatusCode = (int)HttpStatusCode.ExpectationFailed,
                    Data = null,
                    Message = MessageConstants.InvalidInputs,
                    Success = false
                };
            }

            var result = await data.DeleteUser(email);

            if (result is null)
            {
                return new CommonResponse
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Data = null,
                    Message = MessageConstants.UserNotExist,
                    Success = true
                };
            }

            return new CommonResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = null,
                Message = MessageConstants.UserUpdated,
                Success = true
            };
        }

        private static bool IsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
    }
}
