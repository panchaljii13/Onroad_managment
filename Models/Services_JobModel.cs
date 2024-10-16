using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class Services_JobModel
    {
        [Key]

        public int ServicesJobId { get; set; }

        // ComplaintId As ForigenKey on Complaint_info Table
        [Required]
        public int ComplaintId { get; set; }


        // Technician_Id As ForigenKey on Technician Table
        [Required]
        public int TechnicianId { get; set; }

        //[ForeignKey(nameof(TechnicianId))]

        [Required]
        public DateTime Services_Date { get; set; }

        [Required]
        [StringLength(1000)]
        public string Services_Ditaiels { get; set; }



        [Required]
        [StringLength(50)]
        public string Status { get; set; }


        //        Complaint(Many-to-One relationship with ComplaintModel).
        //Technician(Many-to-One relationship with TechnicianInfoModel).
        //Complaint_Closed(One-to-Many relationship with Complaint_ClosedModel).
        // Services_Job = Many-to-One with Complaint and Technician(each services job relates to one complaint and one technician).
        //One-to-Many with Complaint_Closed(one services job can have multiple closed complaints).
        // Navigation Properties
        public ComplaintModel Complaint { get; set; }
        public Technician_InfoModel Technician { get; set; }
        public ICollection<Complaint_ClosedModel> Complaint_Closed { get; set; }
    }
}
