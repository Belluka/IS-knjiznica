using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models.BookstoreViewModels;
using web.Models;

namespace web.Controllers
{
    public class BookstoresViewController : Controller
    {
        private readonly LibraryContext _context;

        public BookstoresViewController(LibraryContext context)
        {
            _context = context;
        }

        // GET: BookstoresView
        public async Task<IActionResult> Index(int? id, int? bookstoreID)
        {
            var viewModel = new BookstoreIndexData();
            viewModel.Bookstores = await _context.Bookstores
                .Include(i => i.Books)
                .Include(i => i.Employees)
                    .ThenInclude(i => i.EmployeeID)
                .Include(i => i.Location)
                .AsNoTracking()
                .OrderBy(i => i.BookstoreId)
                .ToListAsync();
            
            if (id != null)
            {
                ViewData["BookstoreID"] = id.Value;
                Bookstore bookstore = viewModel.Bookstores.Where(
                    i => i.BookstoreId == id.Value).Single();
            }

            if (bookstoreID != null)
            {
                ViewData["BookstoreID"] = bookstoreID.Value;
            }

            return View(viewModel);
        }

        // GET: BookstoresView/Details/5
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

        // GET: BookstoresView/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BookstoresView/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookstoreId,Location,DateEdited")] Bookstore bookstore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookstore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookstore);
        }

        // GET: BookstoresView/Edit/5
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

        // POST: BookstoresView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookstoreId,Location,DateEdited")] Bookstore bookstore)
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

        // GET: BookstoresView/Delete/5
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

        // POST: BookstoresView/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
