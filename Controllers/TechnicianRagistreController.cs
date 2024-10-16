using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnRodeassisment.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnRodeassisment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OnRodeassisment.Controllers
{
    public class TechnicianRagistreController : Controller
    {
        private readonly AppDbContext IdentityDbContext;
        private readonly ILogger<TechnicianRagistreController> _logger;

        public TechnicianRagistreController(AppDbContext context, ILogger<TechnicianRagistreController> logger)
        {
            IdentityDbContext = context;
            _logger = logger;
        }


        // Displays the technician registration view to the user.
        // Checks if the email is already registered, adds a new technician to the database if not, and logs the registration.
        // Handles errors by logging details and showing an appropriate message, then redirects to the login page or returns the view with errors.

        public IActionResult TechnicianRagistre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TechnicianRagistre([Bind("Name,Phon,Email,Specialization,Password,Location")] Technician_InfoModel technicianInfoModel)
        {
                try
                {
                    var existingTechnician = await IdentityDbContext.Technician
                        .AnyAsync(t => t.Email == technicianInfoModel.Email);

                    if (existingTechnician)
                    {
                        // Add a model state error if email already exists
                        ModelState.AddModelError("Email", "Email is already registered.");
                        return View(technicianInfoModel);
                    }

                // Add the new technician to the database
                IdentityDbContext.Add(technicianInfoModel);
                    await IdentityDbContext.SaveChangesAsync();

                    // Log information about the new registration
                    _logger.LogInformation("New Technician registered: {Technician}", technicianInfoModel.Name);
                    // Set a success message in TempData
                    TempData["ShowAlert"] = "Registration successful!";
                    return RedirectToAction(nameof(TechnicianLogIn));
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    _logger.LogError(ex, "An error occurred during registration.");
                    // Set an error message in TempData
                    TempData["ShowAlert"] = "An error occurred during registration.";
                    // Return the view with the current model to show the error message
                    return View(technicianInfoModel);
                }
            

           
           
        }
        // Renders the login view for technicians to enter their credentials.
        // Validates the provided credentials against the database and logs in the technician if they are correct.
        // Displays an error message for invalid login attempts and redirects to the home page upon successful login.



        public IActionResult TechnicianLogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TechnicianLogIn(Technician_InfoModel technicianInfoModel)
        {


            if (!ModelState.IsValid)
            {
                // Here you should add authentication logic
                var user = await IdentityDbContext.Technician
                    .FirstOrDefaultAsync(u => u.Email == technicianInfoModel.Email && u.Password == technicianInfoModel.Password);

                if (user != null)
                {
                    // Set authentication cookie or session
                    TempData["ShowAlert"] = " Technician Login successful!";
                    return RedirectToAction("Index", "Home"); // Redirect to a logged-in area
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(technicianInfoModel);

        }
    }
}
