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
    [Route("Pozwolenia")]
    public class PermissionEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pozwolenia/Lista
        [Route("Lista")]
        public async Task<IActionResult> Index()
        {
              return _context.PermissionEntity != null ? 
                          View(await _context.PermissionEntity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PermissionEntity'  is null.");
        }

        // GET: Pozwolenia/Detale/5
        [Route("Detale")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PermissionEntity == null)
            {
                return NotFound();
            }

            var permissionEntity = await _context.PermissionEntity
                .FirstOrDefaultAsync(m => m.IdPermission == id);
            if (permissionEntity == null)
            {
                return NotFound();
            }

            return View(permissionEntity);
        }

        // GET: Pozwolenia/Dodaj
        [HttpGet]
        [Route("Dodaj")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pozwolenia/Dodaj
        [HttpPost]
        [Route("Dodaj")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PermissionEntity permissionEntity)
        {
            if (ModelState.IsValid)
            {
                permissionEntity.PermissionDateAdded = DateTime.Now;
                _context.Add(permissionEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permissionEntity);
        }

        // GET: Pozwolenia/Edytuj/5
        [HttpGet]
        [Route("Edytuj/{id:int}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PermissionEntity == null)
            {
                return NotFound();
            }

            var permissionEntity = await _context.PermissionEntity.FindAsync(id);
            if (permissionEntity == null)
            {
                return NotFound();
            }
            return View(permissionEntity);
        }

        // POST: Pozwolenia/Edytuj/5
        [HttpPost]
        [Route("Edytuj/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PermissionEntity permissionEntity)
        {
            /*if (id != permissionEntity.IdPermission)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permissionEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionEntityExists(permissionEntity.IdPermission))
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
            return View(permissionEntity);
        }


        public static int CorrectIdPermission;

        // GET: Pozwolenia/Usun/5
        [HttpGet]
        [Route("Usun/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            CorrectIdPermission = id;

            if (id == null || _context.PermissionEntity == null)
            {
                return NotFound("Error_1");
            }

            var permissionEntity = await _context.PermissionEntity.FirstOrDefaultAsync(m => m.IdPermission == id);
            
            if (permissionEntity == null)
            {
                return NotFound("Error_2");
            }

            return View(permissionEntity);
        }

        // POST: PermissionEntities/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            if (_context.PermissionEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PermissionEntity'  is null.");
            }

            var permissionEntity = await _context.PermissionEntity.FindAsync(CorrectIdPermission);
            if (permissionEntity != null)
            {
                _context.PermissionEntity.Remove(permissionEntity);
            }
            else
            {
                return NotFound("Error_5 ; " + CorrectIdPermission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool PermissionEntityExists(int id)
        {
          return (_context.PermissionEntity?.Any(e => e.IdPermission == id)).GetValueOrDefault();
        }
    }
}
