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
   // [ApiController]
    [Route("Zwierzeta")]
    public class AnimalEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimalEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Zwierzeta/Lista
        [Route("Lista")]
        public async Task<IActionResult> Index()
        {
              return _context.AnimalEntity != null ? 
                          View(await _context.AnimalEntity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnimalEntity'  is null.");
        }

        // GET: /Zwierzeta/Detale/5
        [Route("Detale/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalEntity == null)
            {
                return NotFound();
            }

            var animalEntity = await _context.AnimalEntity.FirstOrDefaultAsync(m => m.IdAnimal == id);
            if (animalEntity == null)
            {
                return NotFound();
            }

            return View(animalEntity);
        }

        /*
                // GET: /Zwierzeta/Dodaj
                [Route("Dodaj")]
                public IActionResult Create()
                {
                    return View();
                }

                // POST: /Zwierzeta/Dodaj
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Create([Bind("IdAnimal,AnimalName,AnimalDescription,NumberOfIndividuals,Endangered")] AnimalEntity animalEntity)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(animalEntity);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(animalEntity);
                }
        */

        // -----------------------------------

                        [Route("Dodaj")]
                        public IActionResult Create()
                        {
                            return View();
                        }


                        // POST: /Zwierzeta/Dodaj
                        [Route("DodajPotwierdzone")]
                        public IActionResult CreateConfirmed(AnimalEntity animalEntity)
                        {

                            var animal = new AnimalEntity()
                            {
                                                    
                                IdAnimal = animalEntity.IdAnimal,
                                AnimalDateAdded = animalEntity.AnimalDateAdded,
                                AnimalName = animalEntity.AnimalName,
                                AnimalDescription = animalEntity.AnimalDescription,
                                NumberOfIndividuals = animalEntity.NumberOfIndividuals,
                                Endangered = animalEntity.Endangered

                                /*IdAnimal = 5,
                                AnimalDateAdded = DateTime.Now.AddDays(-10),
                                AnimalName = "Jenotek3",
                                AnimalDescription = "czarno-biały",
                                NumberOfIndividuals = 5,
                                Endangered = false*/
                            };

                            _context.AnimalEntity.Add(animal);
                            _context.SaveChanges();

 

                            return RedirectToAction(nameof(Index));
                        }


        /*        // GET: /Zwierzeta/Edytuj/5
                [Route("Edytuj/{id:int}")]
                public async Task<IActionResult> Edit(int? id)
                {
                    if (id == null || _context.AnimalEntity == null)
                    {
                        return NotFound();
                    }

                    var animalEntity = await _context.AnimalEntity.FindAsync(id);
                    if (animalEntity == null)
                    {
                        return NotFound();
                    }
                    return View(animalEntity);
                }

                // POST: /AnimalEntities/Edit/5
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(int id, [Bind("IdAnimal,AnimalDateAdded,AnimalName,AnimalDescription,NumberOfIndividuals,Endangered")] AnimalEntity animalEntity)
                {
                    if (id != animalEntity.IdAnimal)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(animalEntity);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AnimalEntityExists(animalEntity.IdAnimal))
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
                    return View(animalEntity);
                }*/

        // --------------------------------


                            // GET: /Zwierzeta/Edytuj/5
                            [Route("Edytuj/{id:int}")]
                            public async Task<IActionResult> Edit(int id)
                            {

                                if (id == null || _context.AnimalEntity == null)
                                {
                                    return NotFound("Tutaj1");
                                }

                                var animalEntity = await _context.AnimalEntity.FindAsync(id);
                                if (animalEntity == null)
                                {
                                    return NotFound("Tutaj2");
                                }

                                return View(animalEntity);
                            }

                            // POST: /AnimalEntities/Edit/5
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
                                        if (!AnimalEntityExists(animalEntity.IdAnimal))
                                        {
                                            return NotFound("Tutaj4");
                                        }
                                        else
                                        {
                                            throw;
                                        }
                                    }
                                    return RedirectToAction(nameof(Index));
                            }




        // --------------------------------

        // GET: /Zwierzeta/Usun/5
        /*        [Route("Usun/{id:int}")]
                public async Task<IActionResult> Delete(int? id)
                {
                    if (id == null || _context.AnimalEntity == null)
                    {
                        return NotFound();
                    }

                    var animalEntity = await _context.AnimalEntity.FirstOrDefaultAsync(m => m.IdAnimal == id);
                    if (animalEntity == null)
                    {
                        return NotFound();
                    }
                   // _context.AnimalEntity.Remove(animalEntity);
                    return View(animalEntity);
                }

                // POST: /Zwierzeta/Usun/5
                [HttpDelete, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    if (_context.AnimalEntity == null)
                    {
                        return Problem("Entity set 'ApplicationDbContext.AnimalEntity'  is null.");
                    }
                    var animalEntity = await _context.AnimalEntity.FindAsync(id);
                    if (animalEntity != null)
                    {
                        _context.AnimalEntity.Remove(animalEntity);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }*/


        // --------------------------------

        [Route("Usun/{id:int}")]
                        public IActionResult Delete(int id)
                        {
                            AnimalEntity animal = new AnimalEntity() { IdAnimal = id };

                            _context.AnimalEntity.Remove(animal);
                            _context.SaveChanges();

                            return View();
                        }


                        /*[Route("DeleteConfirmed/{id:int}")]
                        public IActionResult DeleteConfirmed(int id)
                        {
                            AnimalEntity animal = new AnimalEntity() { IdAnimal = id };


                            _context.AnimalEntity.Remove(animal);
                            _context.SaveChanges();

                            return RedirectToAction(nameof(Index));
                        }*/


        // --------------------------------

        private bool AnimalEntityExists(int id)
        {
          return (_context.AnimalEntity?.Any(e => e.IdAnimal == id)).GetValueOrDefault();
        }
    }
}
