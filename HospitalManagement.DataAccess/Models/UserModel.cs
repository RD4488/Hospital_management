using HospitalManagement.DataAccess.Enums;
using System.ComponentModel.DataAnnotations;


namespace HospitalManagement.DataAccess.Models
{
    public class UserModel
    {
        /// <summary>
        /// Id Auto increment
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// User First Name Required
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// User Last Name Required
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Email must be valid and Unique
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User Can't Enter Role name only select
        /// </summary>
        [Required]
        [Range(1, 3)]
        public RoleEnum Role { get; set; }

        /// <summary>
        /// Password should greater than 7 character
        /// </summary>
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 8)]
        public string Password { get; set; }

        /// <summary>
        /// Password and Confirm Password should same
        /// </summary>
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
