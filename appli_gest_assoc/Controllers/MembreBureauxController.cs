using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appli_gest_assoc.Models;

namespace appli_gest_assoc.Controllers
{
    public class MembreBureauxController : Controller
    {
        private readonly gest_assoc_dbContext _context;

        public MembreBureauxController(gest_assoc_dbContext context)
        {
            _context = context;
        }

        // GET: MembreBureaux
        public async Task<IActionResult> Index()
        {
            var gest_assoc_dbContext = _context.MembreBureaus.Include(m => m.Bureau).Include(m => m.Membre);
            return View(await gest_assoc_dbContext.ToListAsync());
        }

        // GET: MembreBureaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membreBureau = await _context.MembreBureaus
                .Include(m => m.Bureau)
                .Include(m => m.Membre)
                .FirstOrDefaultAsync(m => m.IdMembreBureau == id);
            if (membreBureau == null)
            {
                return NotFound();
            }

            return View(membreBureau);
        }

        // GET: MembreBureaux/Create
        public IActionResult Create()
        {
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau");
            ViewData["MembreId"] = new SelectList(_context.Membres, "IdMembre", "IdMembre");
            return View();
        }

        // POST: MembreBureaux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMembreBureau,MembreId,DateCreation,Poste,BureauId")] MembreBureau membreBureau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membreBureau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", membreBureau.BureauId);
            ViewData["MembreId"] = new SelectList(_context.Membres, "IdMembre", "IdMembre", membreBureau.MembreId);
            return View(membreBureau);
        }

        // GET: MembreBureaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membreBureau = await _context.MembreBureaus.FindAsync(id);
            if (membreBureau == null)
            {
                return NotFound();
            }
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", membreBureau.BureauId);
            ViewData["MembreId"] = new SelectList(_context.Membres, "IdMembre", "IdMembre", membreBureau.MembreId);
            return View(membreBureau);
        }

        // POST: MembreBureaux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMembreBureau,MembreId,DateCreation,Poste,BureauId")] MembreBureau membreBureau)
        {
            if (id != membreBureau.IdMembreBureau)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membreBureau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembreBureauExists(membreBureau.IdMembreBureau))
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
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", membreBureau.BureauId);
            ViewData["MembreId"] = new SelectList(_context.Membres, "IdMembre", "IdMembre", membreBureau.MembreId);
            return View(membreBureau);
        }

        // GET: MembreBureaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membreBureau = await _context.MembreBureaus
                .Include(m => m.Bureau)
                .Include(m => m.Membre)
                .FirstOrDefaultAsync(m => m.IdMembreBureau == id);
            if (membreBureau == null)
            {
                return NotFound();
            }

            return View(membreBureau);
        }

        // POST: MembreBureaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membreBureau = await _context.MembreBureaus.FindAsync(id);
            _context.MembreBureaus.Remove(membreBureau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembreBureauExists(int id)
        {
            return _context.MembreBureaus.Any(e => e.IdMembreBureau == id);
        }
    }
}
