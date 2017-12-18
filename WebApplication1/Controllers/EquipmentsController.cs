using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.ViewModels;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    public class EquipmentsController : Controller
    {
        private readonly recipeContext _context;

        public EquipmentsController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Experiments
        public IActionResult Index(string nameequipment, string daterelease, int page = 1, EquipmentsSortState sortOrder = EquipmentsSortState.NameEquipmentAsc)
        {
            int pageSize = 10;
            IQueryable<Equipments> source = _context.Equipments;
            if (nameequipment != null)
            {
                source = source.Where(x => x.NameEquipment == nameequipment);
            }
            if (daterelease != null)
            {
                source = source.Where(x => x.DateRelease == DateTime.Parse(daterelease));
            }

            switch (sortOrder)
            {
                case EquipmentsSortState.NameEquipmentAsc:
                    source = source.OrderBy(x => x.NameEquipment);
                    break;
                case EquipmentsSortState.NameEquipmentDesc:
                    source = source.OrderByDescending(x => x.NameEquipment);
                    break;
                case EquipmentsSortState.DateReleaseAsc:
                    source = source.OrderBy(x => x.DateRelease);
                    break;
                case EquipmentsSortState.DateReleaseDesc:
                    source = source.OrderByDescending(x => x.DateRelease);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            EquipmentsViewModel ivm = new EquipmentsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortEquipmentsViewModel(sortOrder),
                FilterViewModel = new FilterEquipmentsViewModel(nameequipment, daterelease),
                Equipments = items
            };
            return View(ivm);
        }

        [Authorize(Roles = "user, admin")]
        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipments = await _context.Equipments
                .SingleOrDefaultAsync(m => m.IdEquipment == id);
            if (equipments == null)
            {
                return NotFound();
            }

            return View(equipments);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Equipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEquipment,NameEquipment,DateRelease")] Equipments equipments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipments);
        }
        [Authorize(Roles = "admin")]
        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipments = await _context.Equipments.SingleOrDefaultAsync(m => m.IdEquipment == id);
            if (equipments == null)
            {
                return NotFound();
            }
            return View(equipments);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEquipment,NameEquipment,DateRelease")] Equipments equipments)
        {
            if (id != equipments.IdEquipment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentsExists(equipments.IdEquipment))
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
            return View(equipments);
        }

        // GET: Equipments/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipments = await _context.Equipments
                .SingleOrDefaultAsync(m => m.IdEquipment == id);
            if (equipments == null)
            {
                return NotFound();
            }

            return View(equipments);
        }

        // POST: Equipments/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipments = await _context.Equipments.SingleOrDefaultAsync(m => m.IdEquipment == id);
            _context.Equipments.Remove(equipments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentsExists(int id)
        {
            return _context.Equipments.Any(e => e.IdEquipment == id);
        }
    }
}
