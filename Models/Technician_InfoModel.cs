using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class Technician_InfoModel
    {
        [Key]
        public int TechnicianId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Phone number must be a 12-digit number")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phon { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Specialization { get; set; }
        //elcticle,mackenicle

        [Required(ErrorMessage = "Password is required")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 12 characters long")]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Location { get; set; }

        //        Notifications(One-to-Many relationship with NotificationsModel).
        //Services_Jobs(One-to-Many relationship with Services_JobModel).
        //Technician_Jobs(One-to-Many relationship with Technician_JobModel).

        // Technician =  One-to-Many with Notifications, Services_Job, and Technician_Job(one technician can have multiple notifications, services jobs, and technician jobs).

        // Navigation Properties
        public ICollection<NotificationsModel> Notifications { get; set; }
        public ICollection<Services_JobModel> Services_Jobs { get; set; }
        public ICollection<Technician_JobModel> Technician_Jobs { get; set; }
    }
}
