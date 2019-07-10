using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EerCare.Data;
using EerCare.Models;

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
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["nameFilter"] = searchString;

            var providers = from p in _context.Provider
                          select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                providers = providers.Where(s => s.ProviderName.Contains(searchString));
            }

            return View(await providers.AsNoTracking().ToListAsync());
        }

        // GET: Providers/Details/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProviderId,ProviderName,Address1,Address2,City,State,Zip,Phone,MobilePhone,Email,Since,Archived")] Provider provider)
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("ProviderId,ProviderName,ProviderType,Address1,Address2,City,State,Zip,Phone,MobilePhone,Email,Since,Archived")] Provider provider)
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
                return RedirectToAction(nameof(Index));
            }
            return View(provider);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Provider.FindAsync(id);
            provider.Archived = DateTime.Today;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Provider.Any(e => e.ProviderId == id);
        }
    }
}
