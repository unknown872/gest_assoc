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
    public class RecettesController : Controller
    {
        private readonly gest_assoc_dbContext _context;

        public RecettesController(gest_assoc_dbContext context)
        {
            _context = context;
        }

        // GET: Recettes
        public async Task<IActionResult> Index()
        {
            var gest_assoc_dbContext = _context.Recettes.Include(r => r.Bureau).Include(r => r.TypeRecette);
            return View(await gest_assoc_dbContext.ToListAsync());
        }

        // GET: Recettes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recette = await _context.Recettes
                .Include(r => r.Bureau)
                .Include(r => r.TypeRecette)
                .FirstOrDefaultAsync(m => m.IdRecette == id);
            if (recette == null)
            {
                return NotFound();
            }

            return View(recette);
        }

        // GET: Recettes/Create
        public IActionResult Create()
        {
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau");
            ViewData["TypeRecetteId"] = new SelectList(_context.TypeRecettes, "IdTypeRecette", "Libelle");
            return View();
        }

        // POST: Recettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRecette,Libelle,TypeRecetteId,Montant,BureauId")] Recette recette)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", recette.BureauId);
            ViewData["TypeRecetteId"] = new SelectList(_context.TypeRecettes, "IdTypeRecette", "Libelle", recette.TypeRecetteId);
            return View(recette);
        }

        // GET: Recettes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recette = await _context.Recettes.FindAsync(id);
            if (recette == null)
            {
                return NotFound();
            }
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", recette.BureauId);
            ViewData["TypeRecetteId"] = new SelectList(_context.TypeRecettes, "IdTypeRecette", "Libelle", recette.TypeRecetteId);
            return View(recette);
        }

        // POST: Recettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecette,Libelle,TypeRecetteId,Montant,BureauId")] Recette recette)
        {
            if (id != recette.IdRecette)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecetteExists(recette.IdRecette))
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
            ViewData["BureauId"] = new SelectList(_context.Bureaus, "IdBureau", "NomBureau", recette.BureauId);
            ViewData["TypeRecetteId"] = new SelectList(_context.TypeRecettes, "IdTypeRecette", "Libelle", recette.TypeRecetteId);
            return View(recette);
        }

        // GET: Recettes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recette = await _context.Recettes
                .Include(r => r.Bureau)
                .Include(r => r.TypeRecette)
                .FirstOrDefaultAsync(m => m.IdRecette == id);
            if (recette == null)
            {
                return NotFound();
            }

            return View(recette);
        }

        // POST: Recettes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recette = await _context.Recettes.FindAsync(id);
            _context.Recettes.Remove(recette);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecetteExists(int id)
        {
            return _context.Recettes.Any(e => e.IdRecette == id);
        }
    }
}
