    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractMonthlyClaimSystem2.Data;
using ContractMonthlyClaimSystem2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ContractMonthlyClaimSystem2.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ContractMonthlyClaimSystem2Context _context;
        //private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ClaimsController(ContractMonthlyClaimSystem2Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Claim/Track/5
        public async Task<IActionResult> Track(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claim/UpdateStatus/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ProgrammeCoordinator,AcademicManager")]
        public async Task<IActionResult> UpdateStatus(int id, ClaimStatus newStatus)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = newStatus;
            claim.LastUpdated = DateTime.Now;
            claim.UpdatedBy = User.Identity?.Name ?? "Unknown";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Verify));
        }

        private bool ClaimExists(int id)
        {
            return _context.Claim.Any(e => e.Id == id);
        }

        // GET: Claim
        public async Task<IActionResult> Index()
        {
              return _context.Claim != null ? 
                          View(await _context.Claim.ToListAsync()) :
                          Problem("Entity set 'ContractMonthlyClaimSystem2Context.Claim'  is null.");
        }

        // GET: Claim/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Claim == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claim/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LecturerId,DateSubmitted,HoursWorked,HourlyRate,AdditionalNotes,Status,SupportingDocumentPath")] Claim claim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // GET: Claim/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Claim == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LecturerId,DateSubmitted,HoursWorked,HourlyRate,AdditionalNotes,Status,SupportingDocumentPath")] Claim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // GET: Claim/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Claim == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .FirstOrDefaultAsync(m => m.Id == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Claim == null)
            {
                return Problem("Entity set 'ContractMonthlyClaimSystem2Context.Claim'  is null.");
            }
            var claim = await _context.Claim.FindAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Verify()
        {
            var pendingClaims = await _context.Claim
                .Where(c => c.Status == ClaimStatus.Submitted || c.Status == ClaimStatus.UnderReview)
                .ToListAsync();
            return View(pendingClaims);
        }

    }
}
