﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentenBeheer.Data;
using StudentenBeheer.Models;

namespace StudentenBeheer.Controllers
{
    public class GendersController : Controller
    {
        private readonly ApplicationContext _context;

        public GendersController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Gender.ToListAsync());
        }

        public async Task<IActionResult> Details(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Gender gender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        public async Task<IActionResult> Edit(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender.FindAsync(id);
            if (gender == null)
            {
                return NotFound();
            }
            return View(gender);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(char id, [Bind("ID,Name")] Gender gender)
        {
            if (id != gender.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenderExists(gender.ID))
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
            return View(gender);
        }

        public async Task<IActionResult> Delete(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gender = await _context.Gender
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gender == null)
            {
                return NotFound();
            }

            return View(gender);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(char id)
        {
            var gender = await _context.Gender.FindAsync(id);
            _context.Gender.Remove(gender);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenderExists(char id)
        {
            return _context.Gender.Any(e => e.ID == id);
        }
    }
}
