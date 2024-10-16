using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnRodeassisment.Areas.Identity.Data;
using OnRodeassisment.Models;

namespace OnRodeassisment.Controllers
{
    public class TechnicianController : Controller

    {
        private readonly AppDbContext IdentityDbContext;
        private readonly ILogger<TechnicianController> _logger;

        public TechnicianController(AppDbContext context, ILogger<TechnicianController> logger)
        {
            IdentityDbContext = context;
            _logger = logger;
        }

        // GET: Technicians/TechniciansList
        public async Task<IActionResult> TechniciansList()
        {
            var technicians = await IdentityDbContext.Technician.ToListAsync();
            return View(technicians);
        }

        // GET: Technicians/SendComplaint/5
        public async Task<IActionResult> SendComplaint(int id)
        {
            var complaint = await IdentityDbContext.Complaint.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }

            ViewData["ComplaintId"] = id;
            ViewData["TechnicianId"] = id;

            // Fetch the list of technicians to select from
            var technicians = await IdentityDbContext.Technician.ToListAsync();
            return View(technicians);
        }

        // POST: Technicians/SendComplaint
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendComplaint(int complaintId, int technicianId)
        {
            try
            {
                // Find the complaint
                var complaint = await IdentityDbContext.Complaint.FindAsync(complaintId);
                if (complaint == null)
                {
                    return NotFound();
                }

                // Create a new Technician_JobModel entry
                var technicianJob = new Technician_JobModel
                {
                    ComplaintId = complaintId,
                    TechnicianId = technicianId
                };

                // Add the Technician_JobModel to the database
                IdentityDbContext.Technician_Job.Add(technicianJob);
                await IdentityDbContext.SaveChangesAsync();

                _logger.LogInformation($"Complaint {complaintId} has been assigned to technician {technicianId}.");

                TempData["ShowAlert"] = "Complaint has been sent to the technician.";
                return RedirectToAction("TechniciansList");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending the complaint.");

                TempData["ShowAlert"] = "An error occurred while sending the complaint.";
                return View();
            }
        }
    }
}
