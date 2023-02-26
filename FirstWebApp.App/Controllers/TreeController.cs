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
    [Route("Trees")]
    public class TreeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Trees/List
        [Route("List")]
        public async Task<IActionResult> Index()
        {
              return _context.TreeEntity != null ? 
                          View(await _context.TreeEntity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TreeEntity'  is null.");
        }

        // GET: /Trees/Details/5
        [Route("Details")]
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

        // GET: /Trees/Add
        [Route("Add")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Trees/Add
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,DateAdded,Name,Description,LeafDescription,MaxHeight")] TreeEntity treeEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treeEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treeEntity);
        }

        // GET: /Trees/Edit/5
        [Route("Edit")]
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

        // POST: /TreeEntities/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateAdded,Name,Description,LeafDescription,MaxHeight")] TreeEntity treeEntity)
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

        // GET: /Trees/Delete/5
        [Route("Delete")]
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

        // POST: /Trees/Delete/5
        [HttpPost, ActionName("Delete")]
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