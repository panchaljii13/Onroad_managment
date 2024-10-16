using System.ComponentModel.DataAnnotations;

namespace OnRodeassisment.Models
{
    public class NotificationsModel
    {

        [Key]
        public int NotificationsId { get; set; }

        // Customer_Id As ForigenKey on Customer Table

        public int CustomerId { get; set; }

        // Technician_Id As ForigenKey on Technician Table

        public int TechnicianId { get; set; }

        [Required]
        public DateTime Notifications_Date { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [StringLength(1000)]
        //Completed ,rajister,proses
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }


        //        Customer(Many-to-One relationship with CustomerModel).
        //Technician(Many-to-One relationship with TechnicianInfoModel).
        // Notifications = Many-to-One with Customer and Technician(each notification is associated with one customer and one technician)

        // Navigation Properties
        public CustomerModel Customer { get; set; }
        public Technician_InfoModel Technician { get; set; }
    }
}
