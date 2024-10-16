using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class ComplaintModel
    {

        [Key]
        public int ComplaintId { get; set; }


        [StringLength(100)]
        public string VehicleModel { get; set; }

        [Required]
        [StringLength(100)]
        public string VehicleCompny { get; set; }

        [Required]
        [StringLength(150)]
        public string Nature_OF_Issue { get; set; }

        [Required]
        [StringLength(1000)]
        public string IssueDescription { get; set; }

        [Required]
        [StringLength(100)]
        public string Urgency_Lavel { get; set; }

        [Required]

        public DateTime Date_Of_Incident { get; set; }

        public DateTime Privous_Riper_Date { get; set; }

        [Required]
        [StringLength(250)]
        public string Location { get; set; }


        [StringLength(25)]
        public string? SubmitById { get; set; }

        [Required]
        public DateTime SubmissionTime { get; set; } = DateTime.UtcNow;

        // Customer_Id As ForigenKey on Customer Table
        [Required]
        public int CustomerId { get; set; }

        public CustomerModel Customer { get; set; }


        //        Customer(Many-to-One relationship with CustomerModel).
        //Services_Jobs(One-to-Many relationship with Services_JobModel).
        //Technician_Jobs(One-to-Many relationship with Technician_JobModel).
        // Complaint =  One-to-Many with Services_Job and Technician_Job(one complaint can be linked to multiple services jobs and technician jobs).
        //Many-to-One with Customer(many complaints can be from one customer).
        // Navigation Property

        public ICollection<Services_JobModel> Services_Jobs { get; set; }
        public ICollection<Technician_JobModel> Technician_Jobs { get; set; }
    }
}
