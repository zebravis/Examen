using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen.Data;
using Examen.Models;

namespace Examen.Controllers
{
    public class BestellingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BestellingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bestelling
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bestelling.Include(b => b.Reservering);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bestelling/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .Include(b => b.Reservering)
                .Include(b => b.Bestelregel)
                .ThenInclude(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // GET: Bestelling/Create
        public IActionResult Create(string ReserveringId)
        {
            var reservering = _context.Reservering.Single(m => m.Id == int.Parse(ReserveringId));

            var bestelling = new Bestelling
            {
                Reservering = reservering,
                ReserveringId = int.Parse(ReserveringId)
            };

            return View(bestelling);
        }

        // POST: Bestelling/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tafel,ReserveringId")] Bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Bestelregel", new { BestellingId = bestelling.Id });
            }

            return BadRequest();
        }

        // GET: Bestelling/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling.FindAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }
            ViewData["ReserveringId"] = new SelectList(_context.Reservering, "Id", "Name", bestelling.ReserveringId);
            return View(bestelling);
        }

        // POST: Bestelling/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tafel,ReserveringId")] Bestelling bestelling)
        {
            if (id != bestelling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(bestelling.Id))
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
            ViewData["ReserveringId"] = new SelectList(_context.Reservering, "Id", "Name", bestelling.ReserveringId);
            return View(bestelling);
        }

        // GET: Bestelling/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .Include(b => b.Reservering)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Bestelling/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestelling.FindAsync(id);
            _context.Bestelling.Remove(bestelling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestelling.Any(e => e.Id == id);
        }
    }
}
