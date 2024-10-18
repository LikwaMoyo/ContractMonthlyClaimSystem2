// Controllers/HomeController.cs
using ContractMonthlyClaimSystem2.Data;
using ContractMonthlyClaimSystem2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractMonthlyClaimSystem2
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContractMonthlyClaimSystem2Context _context;

        public HomeController(ILogger<HomeController> logger, ContractMonthlyClaimSystem2Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new Claim()); // Pass a new Claim object to the view
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitClaim(Claim claim, IFormFile supportingDocument)
        {
            if (ModelState.IsValid)
            {
                /*
                // Handle the claim submission
                // This is a simplified version. In a real application, you'd use a service to handle this logic
                claim.DateSubmitted = DateTime.Now;
                claim.Status = ClaimStatus.Pending;
                claim.LecturerId = User.Identity.Name ?? "TestUser"; // Use a test user if not authenticated


                if (supportingDocument != null)
                {
                    // Handle file upload
                    // This is a placeholder. Implement proper file handling in production
                    claim.SupportingDocumentPath = supportingDocument.FileName;
                }
                */

                // Here you would typically save the claim to the database
                // For testing, we'll just redirect to a success page
                //_context.Add(claim);

                _context.Claim.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SubmissionSuccess));

                
            }
            //return View("SubmissionSuccess", claim);
            return View(claim);
        }

        public IActionResult SubmissionSuccess()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}