using FluentValidation;
using HospitalManagement.DataAccess.Models;

namespace HospitalManagement.BusinessLogic.Validators
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("User First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("User Last Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("User Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("User Password is required");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("USer Confirm Password is required");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Password Don't Match");
            RuleFor(x => x.Role.ToString()).Must(Role_Length).WithMessage("Choose correct role");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email Address is not correct");
            RuleFor(x => x.Password).Must(PAssword_Length).WithMessage("Password Length must be equal  or greater than 8");
        }
        private bool PAssword_Length(string pass_)
        {
            if (pass_.Length < 8)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool Role_Length(string pass_)
        {
            if (pass_.Length < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
