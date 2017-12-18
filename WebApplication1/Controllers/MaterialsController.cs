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
    public class MaterialsController : Controller
    {
        private readonly recipeContext _context;

        public MaterialsController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Materials
        public IActionResult Index(string namematerial, double? massmaterial, string units, string property, int page = 1, MaterialsSortState sortOrder = MaterialsSortState.NameMaterialAsc)
        {
            int pageSize = 10;
            IQueryable<Materials> source = _context.Materials;
            if (namematerial != null )
            {
                source = source.Where(x => x.NameMaterial == namematerial);
            }
            if (massmaterial != null && massmaterial != 0)
            {
                source = source.Where(x => x.MassMaterial.Value == massmaterial.Value);
            }
            if (units != null)
            {
                source = source.Where(x => x.Units == units);
            }
            if (property != null)
            {
                source = source.Where(x => x.Property == property);
            }

            switch (sortOrder)
            {
                case MaterialsSortState.NameMaterialAsc:
                    source = source.OrderBy(x => x.NameMaterial);
                    break;
                case MaterialsSortState.NameMaterialDesc:
                    source = source.OrderByDescending(x => x.NameMaterial);
                    break;
                case MaterialsSortState.MassMaterialAsc:
                    source = source.OrderBy(x => x.MassMaterial);
                    break;
                case MaterialsSortState.MassMaterialDesc:
                    source = source.OrderByDescending(x => x.MassMaterial);
                    break;
                case MaterialsSortState.UnitsAsc:
                    source = source.OrderBy(x => x.Units);
                    break;
                case MaterialsSortState.UnitsDesc:
                    source = source.OrderByDescending(x => x.Units);
                    break;
                case MaterialsSortState.PropertyAsc:
                    source = source.OrderBy(x => x.Property);
                    break;
                case MaterialsSortState.PropertyDesc:
                    source = source.OrderByDescending(x => x.Property);
                    break;
                default:
                    source = source.OrderBy(x => x.NameMaterial);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            MaterialsViewModel ivm = new MaterialsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortMaterialsViewModel(sortOrder),
                FilterViewModel = new FilterMaterialsViewModel(namematerial, massmaterial, units, property),
                Materials = items
            };
            return View(ivm);
        }

        [Authorize(Roles = "user, admin")]
        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _context.Materials
                .SingleOrDefaultAsync(m => m.IdMaterial == id);
            if (materials == null)
            {
                return NotFound();
            }

            return View(materials);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Materials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMaterial,NameMaterial,MassMaterial,Units,Property")] Materials materials)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materials);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materials);
        }
        [Authorize(Roles = "admin")]
        // GET: Materials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _context.Materials.SingleOrDefaultAsync(m => m.IdMaterial == id);
            if (materials == null)
            {
                return NotFound();
            }
            return View(materials);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMaterial,NameMaterial,MassMaterial,Units,Property")] Materials materials)
        {
            if (id != materials.IdMaterial)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materials);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialsExists(materials.IdMaterial))
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
            return View(materials);
        }
        [Authorize(Roles = "admin")]
        // GET: Materials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materials = await _context.Materials
                .SingleOrDefaultAsync(m => m.IdMaterial == id);
            if (materials == null)
            {
                return NotFound();
            }

            return View(materials);
        }
        [Authorize(Roles = "admin")]
        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materials = await _context.Materials.SingleOrDefaultAsync(m => m.IdMaterial == id);
            _context.Materials.Remove(materials);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialsExists(int id)
        {
            return _context.Materials.Any(e => e.IdMaterial == id);
        }
    }
}
