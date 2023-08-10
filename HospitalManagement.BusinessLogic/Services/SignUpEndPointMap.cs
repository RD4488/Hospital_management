using FluentValidation.Results;
using HospitalManagement.BusinessLogic.Constants;
using HospitalManagement.BusinessLogic.Models;
using HospitalManagement.BusinessLogic.Validators;
using HospitalManagement.DataAccess.DataAccess;
using HospitalManagement.DataAccess.Models;
using System.Net;


namespace HospitalManagement.BusinessLogic.Services
{
    public class SignUpEndPointMap
    {
        public async static Task<CommonResponse> UserSignUp(UserModel user,IHospitalManagementData data)
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

            var result = await data.CreateUser(user);

            if (result is null)
            {
                return new CommonResponse
                {
                    StatusCode = (int)HttpStatusCode.NotAcceptable,
                    Data = null,
                    Message = MessageConstants.UserExist,
                    Success = true
                };
            }
            string message = $"{DateTime.Now} : {user.FirstName} SignUp Successfully";
            message.WriteDataInTextFile();
            return new CommonResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = null,
                Message = MessageConstants.UserCreated,
                Success = true
            };
        }
    }
}
