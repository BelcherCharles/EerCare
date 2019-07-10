using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EerCare.Data;
using EerCare.Models;
using EerCare.Models.ViewModels;

namespace EerCare.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoice.Include(i => i.Provider).Include(i => i.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.Include(i => i.Provider).Include(i => i.Member).Include(i => i.InvoiceLineItems)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public async Task<IActionResult> Create()
        {
            List<Provider> activeProviders = await _context.Provider.ToListAsync();
            List<Member> activeMembers = await _context.Member.ToListAsync();

            var invoice = new InvoiceViewModel();

            invoice.allProviders = activeProviders.OrderBy(p => p.ProviderName).Select(p => new SelectListItem
            {
                Value = p.ProviderId.ToString(),
                Text = p.ProviderName
            }).ToList();

            invoice.allMembers = activeMembers.OrderBy(m => m.AlphaMemberByLast).Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.AlphaMemberByLast
            }).ToList();

            return View(invoice);
        }
        
        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,ProviderId,MemberId,InvoiceDate")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: LineItems/Create
        public async Task<IActionResult> NewLineItem(int id)
        {
            CreateLineItem lineItem = new CreateLineItem();
            lineItem.InvoiceId = id;
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();

            return View(lineItem);
        }

        // POST: LineItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewLineItem([Bind("InvoiceId,LineItemId,ProcedureCode,ProcedureDesc,AmtBilled,AmtSettled")] string returnUrl, LineItem lineItem, int id)
        {
            if (ModelState.IsValid)
            {
                lineItem.InvoiceId = id;
                _context.Add(lineItem);
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);
                //return RedirectToAction(nameof(Index));

            }
            return View(lineItem);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            
            if (invoice == null)
            {
                return NotFound();
            }

            var allProviders = await _context.Provider.ToListAsync();
            var allMembers = await _context.Member.ToListAsync();

            InvoiceViewModel editInvoice = new InvoiceViewModel();
            {
                editInvoice.Invoice = invoice;
                editInvoice.allProviders = allProviders.OrderBy(p => p.ProviderName).Select(p => new SelectListItem
                { 
                    Value = p.ProviderId.ToString(),
                    Text = p.ProviderName
                }).ToList();

                editInvoice.allMembers = allMembers.OrderBy(m => m.AlphaMemberByLast).Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.AlphaMemberByLast
                }).ToList();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            return View(editInvoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,ProviderId,MemberId,InvoiceDate,SettledDate")] Invoice invoice, string returnUrl)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.Include(i => i.Provider).Include(i => i.Member).Include(i => i.InvoiceLineItems)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoice.Include(i => i.InvoiceLineItems).FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice.InvoiceLineItems.Count > 0)
            {
                return View("DeleteErrorView");
            }

            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice.Any(e => e.InvoiceId == id);
        }
    }
}
