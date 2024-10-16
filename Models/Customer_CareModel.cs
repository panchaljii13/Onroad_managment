using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class Customer_CareModel
    {
        [Key]
        public int CustomerCareId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Auth_Name { get; set; }

     //Auto Generate UId NUmber
        public string Uid_Number { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Phone number must be a 12-digit number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phon { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 12 characters long")]
        public string Password { get; set; }

    }
}
