using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class Technician_JobModel
    {

        [Key]
        public int TechnicianJobId { get; set; }

        // Complaint_Id As ForigenKey on Complaint_info Table
        public int ComplaintId { get; set; }


        // Technician_Id As ForigenKey on Technician Table
        public int TechnicianId { get; set; }


        //        Complaint(Many-to-One relationship with ComplaintModel).
        //Technician(Many-to-One relationship with TechnicianInfoModel).
        //   Technician_Job = Many-to-One with Complaint and Technician(each technician job is associated with one complaint and one technician).

        // Navigation Properties
        public ComplaintModel Complaint { get; set; }
        public Technician_InfoModel Technician { get; set; }
        //public object Technician_Id { get; internal set; }
        //public object Technician_Id { get; internal set; }
    }
}
