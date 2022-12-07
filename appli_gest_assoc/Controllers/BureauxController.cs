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
    public class BureauxController : Controller
    {
        private readonly gest_assoc_dbContext _context;

        public BureauxController(gest_assoc_dbContext context)
        {
            _context = context;
        }

        // GET: Bureaux
        public async Task<IActionResult> Index()
        {
            var gest_assoc_dbContext = _context.Bureaus.Include(b => b.Activite).Include(b => b.Association);
            return View(await gest_assoc_dbContext.ToListAsync());
        }

        // GET: Bureaux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bureau = await _context.Bureaus
                .Include(b => b.Activite)
                .Include(b => b.Association)
                .FirstOrDefaultAsync(m => m.IdBureau == id);
            if (bureau == null)
            {
                return NotFound();
            }

            return View(bureau);
        }

        // GET: Bureaux/Create
        public IActionResult Create()
        {
            ViewData["ActiviteId"] = new SelectList(_context.Activites, "IdActivite", "Libelle");
            ViewData["AssociationId"] = new SelectList(_context.Associations, "IdAssociation", "NomAssociation");
            return View();
        }

        // POST: Bureaux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBureau,NomBureau,DateCreation,AssociationId,ActiviteId")] Bureau bureau)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bureau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActiviteId"] = new SelectList(_context.Activites, "IdActivite", "Libelle", bureau.ActiviteId);
            ViewData["AssociationId"] = new SelectList(_context.Associations, "IdAssociation", "NomAssociation", bureau.AssociationId);
            return View(bureau);
        }

        // GET: Bureaux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bureau = await _context.Bureaus.FindAsync(id);
            if (bureau == null)
            {
                return NotFound();
            }
            ViewData["ActiviteId"] = new SelectList(_context.Activites, "IdActivite", "Libelle", bureau.ActiviteId);
            ViewData["AssociationId"] = new SelectList(_context.Associations, "IdAssociation", "NomAssociation", bureau.AssociationId);
            return View(bureau);
        }

        // POST: Bureaux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBureau,NomBureau,DateCreation,AssociationId,ActiviteId")] Bureau bureau)
        {
            if (id != bureau.IdBureau)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bureau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BureauExists(bureau.IdBureau))
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
            ViewData["ActiviteId"] = new SelectList(_context.Activites, "IdActivite", "Libelle", bureau.ActiviteId);
            ViewData["AssociationId"] = new SelectList(_context.Associations, "IdAssociation", "NomAssociation", bureau.AssociationId);
            return View(bureau);
        }

        // GET: Bureaux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bureau = await _context.Bureaus
                .Include(b => b.Activite)
                .Include(b => b.Association)
                .FirstOrDefaultAsync(m => m.IdBureau == id);
            if (bureau == null)
            {
                return NotFound();
            }

            return View(bureau);
        }

        // POST: Bureaux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bureau = await _context.Bureaus.FindAsync(id);
            _context.Bureaus.Remove(bureau);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BureauExists(int id)
        {
            return _context.Bureaus.Any(e => e.IdBureau == id);
        }
    }
}
