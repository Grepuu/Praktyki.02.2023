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
                        [HttpGet]
                        [Route("Dodaj")]
                        public IActionResult Create()
                        {
                            return View();
                        }


                        // POST: /Zwierzeta/Dodaj
                        [HttpPost]
                        [Route("Dodaj")]
                        public IActionResult Create(AnimalEntity animalEntity)
                        {
                            if (AnimalEntityExists(animalEntity.IdAnimal))
                            {
                                //return NotFound("Wpis o takim ID już istnieje");
                                return RedirectToAction(nameof(Create));
                            }
                            else
                            {

            
                            var animal = new AnimalEntity()
                            {                                                  
                                //IdAnimal = animalEntity.IdAnimal,
                                AnimalDateAdded = DateTime.Now,
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

                            }

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
                            [HttpGet]
                            [Route("Edytuj/{id:int}")]
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

                            // POST: /AnimalEntities/Edit/5
                            [HttpPost]
                            [Route("Edytuj/{id:int}")]
                            public async Task<IActionResult> Edit(int id, AnimalEntity animalEntity)
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
                                            return NotFound("Error3");
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

        public static int CorrectIdAnimal;

        // GET: /Drzewa/Usun/5
        [HttpGet]
        [Route("Usun/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            CorrectIdAnimal = id;

            if (id == null || _context.AnimalEntity == null)
            {
                return NotFound("Error_1");
            }

            var animalEntity = await _context.AnimalEntity.FirstOrDefaultAsync(m => m.IdAnimal == id);
            //tree.IdTree = id;

            if (animalEntity == null)
            {
                return NotFound("Error_2");
            }

            return View(animalEntity);
        }

        // POST: /Drzewa/Usun/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            if (_context.AnimalEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnimalEntity'  is null.");
            }

            var animalEntity = await _context.AnimalEntity.FindAsync(CorrectIdAnimal);
            if (animalEntity != null)
            {
                _context.AnimalEntity.Remove(animalEntity);
            }
            else
            {
                return NotFound("Error_5 ; Incorrect id: " + CorrectIdAnimal);
            }

 

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // --------------------------------

        private bool AnimalEntityExists(int id)
        {
          return (_context.AnimalEntity?.Any(e => e.IdAnimal == id)).GetValueOrDefault();
        }
    }
}
