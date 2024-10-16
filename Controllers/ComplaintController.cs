using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OnRodeassisment.Areas.Identity.Data;
using OnRodeassisment.Models;

namespace OnRodeassisment.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly AppDbContext IdentityDbContext;
        private readonly ILogger<ComplaintController> _logger;

        public ComplaintController(AppDbContext context, ILogger<ComplaintController> logger)
        {
            IdentityDbContext = context;
            _logger = logger;
        }

        // A user registers a new customer by filling out the form. 
        // The data is saved to the database, and the user is redirected to add a complaint if successful. 
        //If something goes wrong, an error message is displayed and logged.

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer( CustomerModel CustomerModel)
        {

           
            try
            {
                // Add the new customer to the database
                IdentityDbContext.Add(CustomerModel);
                await IdentityDbContext.SaveChangesAsync();
                TempData["CustomerId"] = CustomerModel.CustomerId;
                // Log information about the new registration
                _logger.LogInformation(" Customer Registered");
                // Set a success message in TempData
                TempData["ShowAlert"] = "Registration successful!";
                return RedirectToAction(nameof(AddComplaint));
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred during registration.");

                // Set an error message in TempData
                TempData["ShowAlert"] = "An error occurred during registration.";

                // Return the view with the current model to show the error message
                return View(CustomerModel);
            }
         
        }

        // A user submits a complaint after registering; the complaint is linked to their customer ID and saved to the database.
        // If the customer ID is missing, an error message is shown and the form is redisplayed.
        // On successful submission, the user is redirected to the complaints list page.

        public IActionResult AddComplaint()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComplaint( ComplaintModel complaint)
        {
            try
            {
                // Retrieve the customer ID from TempData or from user context
                var customerId = TempData["CustomerId"] as int?; // Example of retrieving from TempData
                if (customerId == null)
                {
                    // Handle the case where the customer ID is not available
                    TempData["ShowAlert"] = "Customer ID is required.";
                    return View(complaint);
                }

                complaint.CustomerId = customerId.Value;
                complaint.SubmissionTime = DateTime.UtcNow; // Auto-generate current date and time

                // Add the complaint to the database
                IdentityDbContext.Complaint.Add(complaint);
                await IdentityDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(ComplaintsList)); // Redirect to a list or details page
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred during complaint submission.");

                // Set an error message in TempData
                TempData["ShowAlert"] = "An error occurred during complaint submission.";

                // Return the view with the current model to show the error message
                return View(complaint);
            }
        }

        // Fetches all complaints from the database, including associated customer details.
        // The data is loaded asynchronously to ensure responsiveness and then passed to the view.
    


        public async Task<IActionResult> ComplaintsList()
        {
            var complaints = await IdentityDbContext.Complaint
                .Include(c => c.Customer) // Include related Customer entity
                .ToListAsync();

            return View(complaints);
        }

        // Validates the provided ID and returns a bad request if it's not positive.
        // Retrieves the complaint details from the database, including the associated customer, based on the given ID.
        // Returns a view displaying the complaint details or a not found result if the complaint does not exist.

        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var complaint = await IdentityDbContext.Complaint
                .Include(c => c.Customer) // Make sure Customer is included
                .FirstOrDefaultAsync(c => c.ComplaintId == id);

            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        //--------------------------------------------------------------------------------------------

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var complaint = await IdentityDbContext.Complaint
                .Include(c => c.Customer) // Include related entities if needed
                .FirstOrDefaultAsync(c => c.ComplaintId == id);

            if (complaint == null)
            {
                return NotFound();
            }
            IdentityDbContext.Complaint.Remove(complaint);
            await IdentityDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(ComplaintsList));
        }
        //-----------------------------------------------------------------------------------

        //Handles the GET request for editing an existing complaint.
        /// Checks if the provided complaint ID matches the ID in the database; if not, returns a NotFound result.
        // GET: Complaints/Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var complaint = await IdentityDbContext.Complaint
                .Include(c => c.Customer) // Ensure to include related Customer entity
                .FirstOrDefaultAsync(c => c.ComplaintId == id);

            if (complaint == null)
            {
                return NotFound();
            }

            // Fetch the list of customers for the dropdown (or other selection mechanism)
            ViewBag.Customers = new SelectList(await IdentityDbContext.Customer.ToListAsync(), "CustomerId", "Full_Name", complaint.CustomerId);

            return View(complaint);
        }

        // Handles the POST request for editing an existing complaint.
        // Checks if the provided complaint ID matches the ID in the database; if not, returns a NotFound result.
        // Retrieves the customer ID from TempData, sets the complaint’s submission time to the current date and time, 
        // updates the complaint in the database, and redirects to the complaints list.
        // If an error occurs, logs the exception, sets an error message, and returns the view with the current complaint model.
        // POST: Complaints/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComplaintId,VehicleModel,VehicleCompny,Nature_OF_Issue,IssueDescription, Urgency_Lavel,Date_Of_Incident,Privous_Riper_Date,Location,SubmitById,SubmissionTime,CustomerId")] ComplaintModel complaint)
        {
            if (id != complaint.ComplaintId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                // Retrieve the customer ID from TempData or from user context
                var customerId = TempData["CustomerId"] as int?; // Example of retrieving from TempData
                if (customerId == null)
                {
                    // Handle the case where the customer ID is not available
                    TempData["ShowAlert"] = "Customer ID is required.";
                    return View(complaint);
                }

                complaint.CustomerId = customerId.Value;
                complaint.SubmissionTime = DateTime.UtcNow; // Auto-generate current date and time

                // Add the complaint to the database
                IdentityDbContext.Complaint.Update(complaint);
                await IdentityDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(ComplaintsList)); // Redirect to a list or details page
            }
            catch (Exception ex)
            {
                // Log the exception details
                _logger.LogError(ex, "An error occurred during complaint submission.");

                // Set an error message in TempData
                TempData["ShowAlert"] = "An error occurred during complaint submission.";

                // Return the view with the current model to show the error message
                return View(complaint);
            }
        }

    }
}
