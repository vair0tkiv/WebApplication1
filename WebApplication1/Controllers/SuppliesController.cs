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
    public class SuppliesController : Controller
    {
        private readonly recipeContext _context;

        public SuppliesController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Materials
        public IActionResult Index(string datesupple, string startdate, string enddate, string namematerial, double? massmaterial, decimal? price, string surname, int page = 1, SuppliesSortState sortOrder = SuppliesSortState.DateSuppleAsc)
        {
            int pageSize = 10;
            IQueryable<Supplies> source = _context.Supplies.Include(s => s.IdMaterialNavigation).Include(s => s.IdProviderNavigation);
            if (datesupple != null)
            {
                source = source.Where(x => x.DateSupple == DateTime.Parse(datesupple));
            }
            if (startdate != null && enddate != null)
            {
                source = source.Where(x => x.DateSupple >= DateTime.Parse(startdate) && x.DateSupple <= DateTime.Parse(enddate));
            }
            if (namematerial != null )
            {
                source = source.Where(x => x.IdMaterialNavigation.NameMaterial == namematerial);
            }
            if (massmaterial != null && massmaterial != 0)
            {
                source = source.Where(x => x.IdMaterialNavigation.MassMaterial.Value == massmaterial.Value);
            }
            if (price != null && price != 0)
            {
                source = source.Where(x => x.Price.Value == price.Value);
            }
            if (surname != null)
            {
                source = source.Where(x => x.IdProviderNavigation.Surname == surname);
            }

            switch (sortOrder)
            {
                case SuppliesSortState.IdProviderAsc:
                    source = source.OrderBy(x => x.IdProvider);
                    break;
                case SuppliesSortState.IdProviderDesc:
                    source = source.OrderByDescending(x => x.IdProvider);
                    break;
                case SuppliesSortState.PriceAsc:
                    source = source.OrderBy(x => x.Price);
                    break;
                case SuppliesSortState.PriceDesc:
                    source = source.OrderByDescending(x => x.Price);
                    break;
                case SuppliesSortState.DateSuppleAsc:
                    source = source.OrderBy(x => x.DateSupple);
                    break;
                case SuppliesSortState.DateSuppleDesc:
                    source = source.OrderByDescending(x => x.DateSupple);
                    break;
                case SuppliesSortState.IdMaterialAsc:
                    source = source.OrderBy(x => x.IdMaterial);
                    break;
                case SuppliesSortState.IdMaterialDesc:
                    source = source.OrderByDescending(x => x.IdMaterial);
                    break;
                default:
                    source = source.OrderBy(x => x.DateSupple);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            SuppliesViewModel ivm = new SuppliesViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortSuppliesViewModel(sortOrder),
                FilterViewModel = new FilterSuppliesViewModel(datesupple, startdate, enddate, namematerial, massmaterial, price, surname),
                Supplies = items
            };
            return View(ivm);
        }

        [Authorize(Roles = "user, admin")]
        // GET: Supplies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplies = await _context.Supplies
                .Include(s => s.IdMaterialNavigation)
                .Include(s => s.IdProviderNavigation)
                .SingleOrDefaultAsync(m => m.IdSupple == id);
            if (supplies == null)
            {
                return NotFound();
            }

            return View(supplies);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Supplies/Create
        public IActionResult Create()
        {
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial");
            ViewData["IdProvider"] = new SelectList(_context.Providers, "IdProvider", "IdProvider");
            return View();
        }

        // POST: Supplies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSupple,IdProvider,Price,DateSupple,IdMaterial")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(supplies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", supplies.IdMaterial);
            ViewData["IdProvider"] = new SelectList(_context.Providers, "IdProvider", "IdProvider", supplies.IdProvider);
            return View(supplies);
        }
        [Authorize(Roles = "admin")]
        // GET: Supplies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplies = await _context.Supplies.SingleOrDefaultAsync(m => m.IdSupple == id);
            if (supplies == null)
            {
                return NotFound();
            }
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", supplies.IdMaterial);
            ViewData["IdProvider"] = new SelectList(_context.Providers, "IdProvider", "IdProvider", supplies.IdProvider);
            return View(supplies);
        }

        // POST: Supplies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSupple,IdProvider,Price,DateSupple,IdMaterial")] Supplies supplies)
        {
            if (id != supplies.IdSupple)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supplies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuppliesExists(supplies.IdSupple))
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
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", supplies.IdMaterial);
            ViewData["IdProvider"] = new SelectList(_context.Providers, "IdProvider", "IdProvider", supplies.IdProvider);
            return View(supplies);
        }
        [Authorize(Roles = "admin")]
        // GET: Supplies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplies = await _context.Supplies
                .Include(s => s.IdMaterialNavigation)
                .Include(s => s.IdProviderNavigation)
                .SingleOrDefaultAsync(m => m.IdSupple == id);
            if (supplies == null)
            {
                return NotFound();
            }

            return View(supplies);
        }
        [Authorize(Roles = "admin")]
        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supplies = await _context.Supplies.SingleOrDefaultAsync(m => m.IdSupple == id);
            _context.Supplies.Remove(supplies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuppliesExists(int id)
        {
            return _context.Supplies.Any(e => e.IdSupple == id);
        }
    }
}
