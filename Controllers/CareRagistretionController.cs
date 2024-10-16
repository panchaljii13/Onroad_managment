using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnRodeassisment.Areas.Identity.Data;
using OnRodeassisment.Models;

namespace OnRodeassisment.Controllers
{
    public class CareRagistretionController : Controller
    {
        private readonly AppDbContext IdentityDbContext;
        private readonly ILogger<CareRagistretionController> _logger;

        public CareRagistretionController(AppDbContext context, ILogger<CareRagistretionController> logger)
        {
            IdentityDbContext = context;
            _logger = logger;
        }


        // Checks if the email already exists in the database and returns an error if it does.
        // Generates a unique UID for the new customer and saves the customer data to the database.
        // Logs the registration details and redirects to the login page, or handles and logs any exceptions.

        public IActionResult CareRagistre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CareRagistre([Bind("Auth_Name,Phon,email,Address,Password")] Customer_CareModel customerCareModel)
        {
           
                try
                {

                var existingCustomer = await IdentityDbContext.CustomerCare
                       .AnyAsync(c => c.email == customerCareModel.email);

                    if (existingCustomer)
                    {
                        // Add a model state error if UID already exists
                        ModelState.AddModelError("email", "Email is already registered.");
                        return View(customerCareModel);
                    }
                    // Generate a unique UID for the new customer
                    customerCareModel.Uid_Number = GenerateUniqueUid();

                    // Add the new customer to the database
                    IdentityDbContext.Add(customerCareModel);
                    await IdentityDbContext.SaveChangesAsync();


                    // Log information about the new registration
                    _logger.LogInformation("New customer registered with UID: {Uid}", customerCareModel.Uid_Number);
                    // Set a success message in TempData
                    TempData["ShowAlert"] = "Registration successful!";
                    return RedirectToAction(nameof(CareLogIn));
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    _logger.LogError(ex, "An error occurred during registration.");

                    // Set an error message in TempData
                    TempData["ShowAlert"] = "An error occurred during registration.";

                    // Return the view with the current model to show the error message
                    return View(customerCareModel);
                }
               
        }
        // this methode Generate a unique UID for the new customer UID
        private string GenerateUniqueUid()
        {
            var random = new Random();
            var uidNumber = random.Next(1, 1000000000).ToString("D9"); // Generates numbers between 000000001 and 999999999
            return uidNumber;
        }


        // Displays the login view for the user to enter credentials.
        // Checks the provided credentials against the database and logs in the user if they are valid.
        // Displays an error message for invalid login attempts and redirects to the home page upon successful login.

        public IActionResult CareLogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CareLogIn(Customer_CareModel customerCareModel)
        {


            if (!ModelState.IsValid)
            {
                // Here you should add authentication logic
                var user = await IdentityDbContext.CustomerCare
                    .FirstOrDefaultAsync(u => u.email == customerCareModel.email && u.Password == customerCareModel.Password);

                if (user != null)
                {
                    // Set authentication cookie or session
                    TempData["ShowAlert"] = "Login successful!";
                    return RedirectToAction("AddCustomer", "Complaint"); // Redirect to a logged-in area
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(customerCareModel);

        }
    }
}
