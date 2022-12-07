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
    public class DepensesController : Controller
    {
        private readonly gest_assoc_dbContext _context;

        public DepensesController(gest_assoc_dbContext context)
        {
            _context = context;
        }

        // GET: Depenses
        public async Task<IActionResult> Index()
        {
            var gest_assoc_dbContext = _context.Depenses.Include(d => d.Bureau).Include(d => d.TypeDepense);
            return View(await gest_assoc_dbContext.ToListAsync());
        }

        // GET: Depenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depenses
                .Include(d => d.Bureau)
                .Include(d => d.TypeDepense)
                .FirstOrDefaultAsync(m => m.IdDepense == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // GET: Depenses/Create
        public IActionResult Create()
        {
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau");
            ViewData["TypeDepenseId"] = new SelectList(_context.TypeDepenses, "IdTypeDepense", "Libelle");
            return View();
        }

        // POST: Depenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDepense,Libelle,TypeDepenseId,Montant,BureauId")] Depense depense)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", depense.BureauId);
            ViewData["TypeDepenseId"] = new SelectList(_context.TypeDepenses, "IdTypeDepense", "Libelle", depense.TypeDepenseId);
            return View(depense);
        }

        // GET: Depenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depenses.FindAsync(id);
            if (depense == null)
            {
                return NotFound();
            }
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", depense.BureauId);
            ViewData["TypeDepenseId"] = new SelectList(_context.TypeDepenses, "IdTypeDepense", "Libelle", depense.TypeDepenseId);
            return View(depense);
        }

        // POST: Depenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDepense,Libelle,TypeDepenseId,Montant,BureauId")] Depense depense)
        {
            if (id != depense.IdDepense)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depense);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepenseExists(depense.IdDepense))
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
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", depense.BureauId);
            ViewData["TypeDepenseId"] = new SelectList(_context.TypeDepenses, "IdTypeDepense", "Libelle", depense.TypeDepenseId);
            return View(depense);
        }

        // GET: Depenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depense = await _context.Depenses
                .Include(d => d.Bureau)
                .Include(d => d.TypeDepense)
                .FirstOrDefaultAsync(m => m.IdDepense == id);
            if (depense == null)
            {
                return NotFound();
            }

            return View(depense);
        }

        // POST: Depenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depense = await _context.Depenses.FindAsync(id);
            _context.Depenses.Remove(depense);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepenseExists(int id)
        {
            return _context.Depenses.Any(e => e.IdDepense == id);
        }
    }
}
