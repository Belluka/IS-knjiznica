using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace web.Controllers
{
    [Authorize]
    public class BookstoresController : Controller
    {
        private readonly LibraryContext _context;

        private readonly UserManager<ApplicationUser> _usermanager;

        public BookstoresController(LibraryContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        // GET: Bookstores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookstores.ToListAsync());
        }

        // GET: Bookstores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookstore = await _context.Bookstores
                .FirstOrDefaultAsync(m => m.BookstoreId == id);
            if (bookstore == null)
            {
                return NotFound();
            }

            return View(bookstore);
        }

        // GET: Bookstores/Create
        [Authorize(Roles="Administrator, Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookstores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator, Manager")]
        public async Task<IActionResult> Create([Bind("BookstoreId,Location")] Bookstore bookstore)
        {
            var currentUser = await _usermanager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                bookstore.DateEdited = DateTime.Now;
                bookstore.Owner = currentUser;
                _context.Add(bookstore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookstore);
        }

        // GET: Bookstores/Edit/5
        [Authorize(Roles="Administrator, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookstore = await _context.Bookstores.FindAsync(id);
            if (bookstore == null)
            {
                return NotFound();
            }
            return View(bookstore);
        }

        // POST: Bookstores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator, Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("BookstoreId,Location")] Bookstore bookstore)
        {
            if (id != bookstore.BookstoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookstore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookstoreExists(bookstore.BookstoreId))
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
            return View(bookstore);
        }

        // GET: Bookstores/Delete/5
        [Authorize(Roles="Administrator, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookstore = await _context.Bookstores
                .FirstOrDefaultAsync(m => m.BookstoreId == id);
            if (bookstore == null)
            {
                return NotFound();
            }

            return View(bookstore);
        }

        // POST: Bookstores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator, Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookstore = await _context.Bookstores.FindAsync(id);
            _context.Bookstores.Remove(bookstore);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookstoreExists(int id)
        {
            return _context.Bookstores.Any(e => e.BookstoreId == id);
        }
    }
}
