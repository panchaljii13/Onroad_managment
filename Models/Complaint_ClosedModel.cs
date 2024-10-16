using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class Complaint_ClosedModel
    {
        [Key]
        public int ComplaintClosedId { get; set; }

        // Services_Id As ForigenKey on Services Table
        public int ServicesId { get; set; }



        public DateTime Close_date { get; set; }



        // Complaint_Closed = Many-to-One with Services_Job(a closed complaint relates to one services job).
        //Services_Job(Many-to-One relationship with Services_JobModel).
        // Navigation Property
        public Services_JobModel Services_Job { get; set; }
    }
}
