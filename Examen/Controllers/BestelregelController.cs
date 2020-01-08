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
    public class BestelregelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BestelregelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bestelregel
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bestelregel.Include(b => b.Bestelling).Include(b => b.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bestelregel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelregel = await _context.Bestelregel
                .Include(b => b.Bestelling)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelregel == null)
            {
                return NotFound();
            }

            return View(bestelregel);
        }

        // GET: Bestelregel/Create
        public IActionResult Create(string BestellingId)
        {

            var bestelling = _context.Bestelling.Single(m => m.Id == int.Parse(BestellingId));

            var bestelregel = new Bestelregel
            {
                Bestelling = bestelling,
                BestellingId = int.Parse(BestellingId)
            };
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: Bestelregel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BestellingId,ProductId,Aantal")] Bestelregel bestelregel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestelregel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Bestelling", new { Id = bestelregel.BestellingId });

            }

            return BadRequest();
        }

        // GET: Bestelregel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelregel = await _context.Bestelregel.FindAsync(id);
            if (bestelregel == null)
            {
                return NotFound();
            }
            ViewData["BestellingId"] = new SelectList(_context.Bestelling, "Id", "Name", bestelregel.BestellingId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", bestelregel.ProductId);
            return View(bestelregel);
        }

        // POST: Bestelregel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BestellingId,ProductId,Aantal")] Bestelregel bestelregel)
        {
            if (id != bestelregel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelregel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestelregelExists(bestelregel.Id))
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
            ViewData["BestellingId"] = new SelectList(_context.Bestelling, "Id", "Name", bestelregel.BestellingId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", bestelregel.ProductId);
            return View(bestelregel);
        }

        // GET: Bestelregel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelregel = await _context.Bestelregel
                .Include(b => b.Bestelling)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bestelregel == null)
            {
                return NotFound();
            }

            return View(bestelregel);
        }

        // POST: Bestelregel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelregel = await _context.Bestelregel.FindAsync(id);
            _context.Bestelregel.Remove(bestelregel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestelregelExists(int id)
        {
            return _context.Bestelregel.Any(e => e.Id == id);
        }
    }
}
