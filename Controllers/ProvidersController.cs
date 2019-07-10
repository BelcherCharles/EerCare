using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EerCare.Data;
using EerCare.Models;
using Microsoft.AspNetCore.Authorization;

namespace EerCare.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvidersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Providers
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string cityString, string specialtyString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["nameFilter"] = searchString;
            ViewData["cityFilter"] = cityString;
            ViewData["specialtyFilter"] = specialtyString;

            var providers = from p in _context.Provider
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                providers = providers.Where(s => s.ProviderName.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(cityString))
            {
                providers = providers.Where(s => s.City.Contains(cityString));
            }

            if (!String.IsNullOrEmpty(specialtyString))
            {
                providers = providers.Where(s => s.ProviderType.Contains(specialtyString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    providers = providers.OrderByDescending(m => m.ProviderName);
                    break;
                default:
                    providers = providers.OrderBy(m => m.ProviderName);
                    break;
            }

            return View(await providers.AsNoTracking().ToListAsync());
        }

        // GET: Providers/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.ProviderId == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProviderId,ProviderName,ProviderType,Address1,Address2,City,State,Zip,Phone,MobilePhone,Email,Since,Archived")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                provider.Since = DateTime.Today;
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        // GET: Providers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            var provider = await _context.Provider.FindAsync(id);
            if (provider == null)
            {
                return NotFound();
            }
            return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProviderId,ProviderName,ProviderType,Address1,Address2,City,State,Zip,Phone,MobilePhone,Email,Since,Archived")] string returnUrl, Provider provider)
        {
            if (id != provider.ProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProviderExists(provider.ProviderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect(returnUrl);
            }
            return View(provider);
        }

        // GET: Providers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.ProviderId == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string returnUrl, int id)
        {
            var provider = await _context.Provider.FindAsync(id);

            if (provider.Archived == null)
            {
                provider.Archived = DateTime.Today;
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);
            }

            return RedirectToAction("ProviderArchiveError", new { id });
        }

        // GET: Providers/Delete/5
        [Authorize]
        public async Task<IActionResult> ProviderArchiveError(int? id)
        {
            var provider = await _context.Provider
                .FirstOrDefaultAsync(m => m.ProviderId == id);

            return View(provider);
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.ProviderId == id);
        }
    }
}
