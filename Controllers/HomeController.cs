// Controllers/HomeController.cs
using ContractMonthlyClaimSystem2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

            // Here you would typically save the claim to the database
            // For testing, we'll just redirect to a success page
            return RedirectToAction(nameof(SubmissionSuccess));
        }
        return View("SubmissionSuccess", claim);
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