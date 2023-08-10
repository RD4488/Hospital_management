using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.BusinessLogic.Models
{
    public class CommonResponse
    {
        /// <summary>
        /// Any Kind of data
        /// </summary>
        public object? Data { get; set; }
        /// <summary>
        /// Status code of responce
        /// </summary>
        [Required]
        public int StatusCode { get; set; }
        /// <summary>
        /// particular message related to operation
        /// </summary>
        [Required]
        public string Message { get; set; }
        /// <summary>
        /// true or false for success
        /// </summary>
        [Required]
        public bool Success { get; set; }
    }
}
