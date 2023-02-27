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
    [Route("Animal")]
    public class AnimalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Animal/List
        [Route("List")]
        public async Task<IActionResult> Index()
        {
              return _context.AnimalEntity != null ? 
                          View(await _context.AnimalEntity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnimalEntity'  is null.");
        }

        // GET: /Animal/Details/5
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalEntity == null)
            {
                return NotFound();
            }

            var animalEntity = await _context.AnimalEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalEntity == null)
            {
                return NotFound();
            }

            return View(animalEntity);
        }

        // GET: /Animal/Add
        [Route("Add")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Animal/Add
        [Route("AddConfirmed")]
        public IActionResult CreateConfirmed(AnimalEntity animalEntity)
        {
            if (AnimalEntityExists(animalEntity.Id))
            {
                return NotFound("Wpis o takim ID ju¿ istnieje");
                return RedirectToAction(nameof(Create));
            }
            else
            {


                var animal = new AnimalEntity()
                {
                    Id = animalEntity.Id,
                    DateAdded = animalEntity.DateAdded,
                    Name = animalEntity.Name,
                    Description = animalEntity.Description,
                    NumberInHerd = animalEntity.NumberInHerd,
                    Endangered = animalEntity.Endangered
                };

                _context.AnimalEntity.Add(animal);
                _context.SaveChanges();

            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Animal/Edit/5
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {

            if (id == null || _context.AnimalEntity == null)
            {
                return NotFound("Error1");
            }

            var animalEntity = await _context.AnimalEntity.FindAsync(id);
            if (animalEntity == null)
            {
                return NotFound("Error2");
            }

            return View(animalEntity);
        }

        // POST: /Animals/Edit/5
        [Route("EditConfirmed")]
        public async Task<IActionResult> EditConfirmed(int id, AnimalEntity animalEntity)
        {

            try
            {
                _context.Update(animalEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalEntityExists(animalEntity.Id))
                {
                    return NotFound("Error3");
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: /Animals/Delete/5
        [Route("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            AnimalEntity animal = new AnimalEntity() { Id = id };

            _context.AnimalEntity.Remove(animal);
            _context.SaveChanges();

            return View();
        }



        private bool AnimalEntityExists(int id)
        {
            return (_context.AnimalEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}