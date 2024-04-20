using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authentication.Data;
using Authentication.Models;
using Authentication.ViewModels;

namespace Authentication.Controllers
{
    public class AuthsController : Controller
    {
        private readonly AppDbContext _context;

        public AuthsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Auths
        public async Task<IActionResult> Index()
        {
            var viewModel = new AuthViewModel
            {
                Auths = await _context.Auths.ToListAsync(),
                NewAuth = new Auth()
            };
            return View(viewModel);
        }

        // GET: Auths/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // GET: Auths/Create
        public IActionResult Create()
        {
            var auth = new Auth(); // Create a new instance of Auth
            return View(auth);
        }

        // POST: Auths/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Explicitly validate the NewAuth object
                    if (TryValidateModel(viewModel.NewAuth))
                    {
                        _context.Add(viewModel.NewAuth);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception for debugging purposes
                    // This will help identify any unexpected errors during data saving
                    // Log.Error(ex, "Error occurred while saving new Auth record");

                    ModelState.AddModelError("", "An error occurred while saving the new user. Please try again.");
                }
            }

            // If model state is not valid, reload the view with the ViewModel
            viewModel.Auths = await _context.Auths.ToListAsync();
            return View("Index", viewModel);
        }




        // GET: Auths/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths.FindAsync(id);
            if (auth == null)
            {
                return NotFound();
            }
            return View(auth);
        }

        // POST: Auths/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Expiry")] Auth auth)
        {
            if (id != auth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(auth);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthExists(auth.Id))
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
            return View(auth);
        }

        // GET: Auths/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auth = await _context.Auths
                .FirstOrDefaultAsync(m => m.Id == id);
            if (auth == null)
            {
                return NotFound();
            }

            return View(auth);
        }

        // POST: Auths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auth = await _context.Auths.FindAsync(id);
            if (auth != null)
            {
                _context.Auths.Remove(auth);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthExists(int id)
        {
            return _context.Auths.Any(e => e.Id == id);
        }
    }
}
