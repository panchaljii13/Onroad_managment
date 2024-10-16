using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class CustomerModel
    {
        internal string customerId;

        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Phone number must be a 12-digit number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phon { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }


        //        Complaints(One-to-Many relationship with ComplaintModel).
        //Notifications(One-to-Many relationship with NotificationsModel).
        // Customer =   One-to-Many with Complaint and Notifications(one customer can have multiple complaints and notifications)
        // Navigation Properties
        public ICollection<ComplaintModel> Complaints { get; set; }
        public ICollection<NotificationsModel> Notifications { get; set; }
    }
}
