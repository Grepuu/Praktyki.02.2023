using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FirstWebApp.App.Data;
using FirstWebApp.App.Models;

namespace FirstWebApp.App.Controllers
{
    [Route("Drzewa")]
    public class TreeEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreeEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Drzewa/Lista
        [Route("Lista")]
        public async Task<IActionResult> Index()
        {
              return _context.TreeEntity != null ? 
                          View(await _context.TreeEntity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TreeEntity'  is null.");
        }

        // GET: /Drzewa/Detale/5
        [Route("Detale")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TreeEntity == null)
            {
                return NotFound();
            }

            var treeEntity = await _context.TreeEntity
                .FirstOrDefaultAsync(m => m.IdTree == id);
            if (treeEntity == null)
            {
                return NotFound();
            }

            return View(treeEntity);
        }

        // GET: /Drzewa/Dodaj
        [Route("Dodaj")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Drzewa/Dodaj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTree,TreeDateAdded,TreeName,TreeDescription,LeafDescription,MaxHeight")] TreeEntity treeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treeEntity);
        }

        // GET: /Drzewa/Edytuj/5
        [Route("Edytuj")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TreeEntity == null)
            {
                return NotFound();
            }

            var treeEntity = await _context.TreeEntity.FindAsync(id);
            if (treeEntity == null)
            {
                return NotFound();
            }
            return View(treeEntity);
        }

        // POST: /Drzewa/Edytuj/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTree,TreeDateAdded,TreeName,TreeDescription,LeafDescription,MaxHeight")] TreeEntity treeEntity)
        {
            if (id != treeEntity.IdTree)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treeEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreeEntityExists(treeEntity.IdTree))
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
            return View(treeEntity);
        }

        // GET: /Drzewa/Usun/5
        [Route("Usun")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TreeEntity == null)
            {
                return NotFound();
            }

            var treeEntity = await _context.TreeEntity
                .FirstOrDefaultAsync(m => m.IdTree == id);
            if (treeEntity == null)
            {
                return NotFound();
            }

            return View(treeEntity);
        }

        // POST: /Drzewa/Usun/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TreeEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TreeEntity'  is null.");
            }
            var treeEntity = await _context.TreeEntity.FindAsync(id);
            if (treeEntity != null)
            {
                _context.TreeEntity.Remove(treeEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreeEntityExists(int id)
        {
          return (_context.TreeEntity?.Any(e => e.IdTree == id)).GetValueOrDefault();
        }
    }
}
