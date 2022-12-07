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
    public class TypeRecettesController : Controller
    {
        private readonly gest_assoc_dbContext _context;

        public TypeRecettesController(gest_assoc_dbContext context)
        {
            _context = context;
        }

        // GET: TypeRecettes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeRecettes.ToListAsync());
        }

        // GET: TypeRecettes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRecette = await _context.TypeRecettes
                .FirstOrDefaultAsync(m => m.IdTypeRecette == id);
            if (typeRecette == null)
            {
                return NotFound();
            }

            return View(typeRecette);
        }

        // GET: TypeRecettes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeRecettes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTypeRecette,Libelle")] TypeRecette typeRecette)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeRecette);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeRecette);
        }

        // GET: TypeRecettes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRecette = await _context.TypeRecettes.FindAsync(id);
            if (typeRecette == null)
            {
                return NotFound();
            }
            return View(typeRecette);
        }

        // POST: TypeRecettes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTypeRecette,Libelle")] TypeRecette typeRecette)
        {
            if (id != typeRecette.IdTypeRecette)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeRecette);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeRecetteExists(typeRecette.IdTypeRecette))
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
            return View(typeRecette);
        }

        // GET: TypeRecettes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeRecette = await _context.TypeRecettes
                .FirstOrDefaultAsync(m => m.IdTypeRecette == id);
            if (typeRecette == null)
            {
                return NotFound();
            }

            return View(typeRecette);
        }

        // POST: TypeRecettes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeRecette = await _context.TypeRecettes.FindAsync(id);
            _context.TypeRecettes.Remove(typeRecette);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeRecetteExists(int id)
        {
            return _context.TypeRecettes.Any(e => e.IdTypeRecette == id);
        }
    }
}
