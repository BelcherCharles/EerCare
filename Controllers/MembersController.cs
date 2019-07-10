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
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Members
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string searchString, string ssnSearchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["DODSortParm"] = sortOrder== "dodDate" ? "dod_date_desc" : "dodDate";
            ViewData["nameFilter"] = searchString;
            ViewData["ssnFilter"] = ssnSearchString;

            var members = from m in _context.Member
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.LastName.Contains(searchString));               
                //|| s.SSN.Equals(searchString));
            }

            if (!String.IsNullOrEmpty(ssnSearchString))
            {
                members = members.Where(s => s.SSN.Equals(ssnSearchString));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    members = members.OrderByDescending(m => m.LastName);
                    break;
                case "Date":
                    members = members.OrderBy(m => m.DOB != null).ThenBy(m => m.DOB);
                    break;
                case "date_desc":
                    members = members.OrderByDescending(m => m.DOB != null).ThenByDescending(m => m.DOB);
                    break;
                case "dodDate":
                    members = members.OrderBy(m => m.DOD != null).ThenBy(m => m.DOD);
                    break;
                case "dod_date_desc":
                    members = members.OrderByDescending(m => m.DOD != null).ThenByDescending(m => m.DOD);
                    break;
                default:
                    members = members.OrderBy(m => m.LastName);
                    break;
            }
            return View(await members.AsNoTracking().ToListAsync());
        }

        // GET: Members/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        [Authorize]
        public IActionResult Create()
        {

            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MI,LastName,SSN,DOB,DOD,Address1,Address2,City,State,Zip,Phone,MobilePhone,Email")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.Since = DateTime.Today;
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MI,LastName,SSN,DOB,DOD,Address1,Address2,City,State,Zip,Phone,MobilePhone,Email,Since,Archived")] string returnUrl, Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            ViewBag.returnUrl = Request.Headers["Referer"].ToString();
            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string returnUrl, int id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member.Archived == null)
            {
                member.Archived = DateTime.Today;
                await _context.SaveChangesAsync();
                return Redirect(returnUrl);
            }

            return RedirectToAction("MemberArchiveError", new { id } );
        }

        // GET: Members/Delete/5
        [Authorize]
        public async Task<IActionResult> MemberArchiveError(int? id)
        {
            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.Id == id);

            return View(member);
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
    }
}
