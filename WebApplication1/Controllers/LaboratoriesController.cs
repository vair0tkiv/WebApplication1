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
    public class LaboratoriesController : Controller
    {
        private readonly recipeContext _context;

        public LaboratoriesController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Laboratories
        public IActionResult Index(int? numberlaboratory, string nameequipment, int page = 1, LaboratoriesSortState sortOrder = LaboratoriesSortState.NumberLaboratoryAsc)
        {
            int pageSize = 10;
            IQueryable<Laboratories> source = _context.Laboratories.Include(l => l.IdEquipmentNavigation);

            if (numberlaboratory != null && numberlaboratory != 0)
            {
                source = source.Where(x => x.NumberLaboratory == numberlaboratory);
            }
            if (nameequipment != null )
            {
                source = source.Where(x => x.IdEquipmentNavigation.NameEquipment == nameequipment);
            }

            switch (sortOrder)
            {
                case LaboratoriesSortState.NumberLaboratoryAsc:
                    source = source.OrderBy(x => x.NumberLaboratory);
                    break;
                case LaboratoriesSortState.NumberLaboratoryDesc:
                    source = source.OrderByDescending(x => x.NumberLaboratory);
                    break;
                case LaboratoriesSortState.IdEquipmentAsc:
                    source = source.OrderBy(x => x.IdEquipment);
                    break;
                case LaboratoriesSortState.IdEquipmentDesc:
                    source = source.OrderByDescending(x => x.IdEquipment);
                    break;
            }

                    var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            LaboratoriesViewModel ivm = new LaboratoriesViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortLaboratoriesViewModel(sortOrder),
                FilterViewModel = new FilterLaboratoriesViewModel(numberlaboratory, nameequipment),
                Laboratories = items
            };
            return View(ivm);
        }


        [Authorize(Roles = "user, admin")]
        // GET: Laboratories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratories = await _context.Laboratories
                .Include(l => l.IdEquipmentNavigation)
                .SingleOrDefaultAsync(m => m.IdLaboratory == id);
            if (laboratories == null)
            {
                return NotFound();
            }

            return View(laboratories);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Laboratories/Create
        public IActionResult Create()
        {
            ViewData["IdEquipment"] = new SelectList(_context.Equipments, "IdEquipment", "IdEquipment");
            return View();
        }

        // POST: Laboratories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLaboratory,NumberLaboratory,IdEquipment")] Laboratories laboratories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laboratories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipment"] = new SelectList(_context.Equipments, "IdEquipment", "IdEquipment", laboratories.IdEquipment);
            return View(laboratories);
        }
        [Authorize(Roles = "admin")]
        // GET: Laboratories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratories = await _context.Laboratories.SingleOrDefaultAsync(m => m.IdLaboratory == id);
            if (laboratories == null)
            {
                return NotFound();
            }
            ViewData["IdEquipment"] = new SelectList(_context.Equipments, "IdEquipment", "IdEquipment", laboratories.IdEquipment);
            return View(laboratories);
        }

        // POST: Laboratories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLaboratory,NumberLaboratory,IdEquipment")] Laboratories laboratories)
        {
            if (id != laboratories.IdLaboratory)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laboratories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaboratoriesExists(laboratories.IdLaboratory))
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
            ViewData["IdEquipment"] = new SelectList(_context.Equipments, "IdEquipment", "IdEquipment", laboratories.IdEquipment);
            return View(laboratories);
        }
        [Authorize(Roles = "admin")]
        // GET: Laboratories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laboratories = await _context.Laboratories
                .Include(l => l.IdEquipmentNavigation)
                .SingleOrDefaultAsync(m => m.IdLaboratory == id);
            if (laboratories == null)
            {
                return NotFound();
            }

            return View(laboratories);
        }
        [Authorize(Roles = "admin")]
        // POST: Laboratories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laboratories = await _context.Laboratories.SingleOrDefaultAsync(m => m.IdLaboratory == id);
            _context.Laboratories.Remove(laboratories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaboratoriesExists(int id)
        {
            return _context.Laboratories.Any(e => e.IdLaboratory == id);
        }
    }
}
