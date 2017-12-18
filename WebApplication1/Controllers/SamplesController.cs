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
    public class SamplesController : Controller
    {
        private readonly recipeContext _context;

        public SamplesController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Materials
        public IActionResult Index(string material, double? mass, string property, int page = 1, SamplesSortState sortOrder = SamplesSortState.NameSampleAsc)
        {
            int pageSize = 10;
            IQueryable<Samples> source = _context.Samples.Include(s => s.IdMaterialNavigation);
            
            if (material != null)
            {
                source = source.Where(x => x.IdMaterialNavigation.NameMaterial == material);
            }

            if (mass != null && mass != 0)
            {
                source = source.Where(x => x.IdMaterialNavigation.MassMaterial.Value == mass.Value);
            }

            if (property != null)
            {
                source = source.Where(x => x.IdMaterialNavigation.Property == property);
            }

            switch (sortOrder)
            {
                case SamplesSortState.NameSampleAsc:
                    source = source.OrderBy(x => x.NameSample);
                    break;
                case SamplesSortState.NameSampleDesc:
                    source = source.OrderByDescending(x => x.NameSample);
                    break;
                case SamplesSortState.MassSampleAsc:
                    source = source.OrderBy(x => x.MassSample);
                    break;
                case SamplesSortState.MassSampleDesc:
                    source = source.OrderByDescending(x => x.MassSample);
                    break;
                case SamplesSortState.IdMaterialAsc:
                    source = source.OrderBy(x => x.IdMaterial);
                    break;
                case SamplesSortState.IdMaterialDesc:
                    source = source.OrderByDescending(x => x.IdMaterial);
                    break;
                default:
                    source = source.OrderBy(x => x.NameSample);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            SamplesViewModel ivm = new SamplesViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortSamplesViewModel(sortOrder),
                FilterViewModel = new FilterSamplesViewModel(material, mass, property),
                Samples = items
            };
            return View(ivm);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Samples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples
                .Include(s => s.IdMaterialNavigation)
                .SingleOrDefaultAsync(m => m.IdSample == id);
            if (samples == null)
            {
                return NotFound();
            }

            return View(samples);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Samples/Create
        public IActionResult Create()
        {
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial");
            return View();
        }

        // POST: Samples/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSample,NameSample,MassSample,IdMaterial")] Samples samples)
        {
            if (ModelState.IsValid)
            {
                _context.Add(samples);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", samples.IdMaterial);
            return View(samples);
        }
        [Authorize(Roles = "admin")]
        // GET: Samples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples.SingleOrDefaultAsync(m => m.IdSample == id);
            if (samples == null)
            {
                return NotFound();
            }
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", samples.IdMaterial);
            return View(samples);
        }

        // POST: Samples/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSample,NameSample,MassSample,IdMaterial")] Samples samples)
        {
            if (id != samples.IdSample)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(samples);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamplesExists(samples.IdSample))
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
            ViewData["IdMaterial"] = new SelectList(_context.Materials, "IdMaterial", "IdMaterial", samples.IdMaterial);
            return View(samples);
        }
        [Authorize(Roles = "admin")]
        // GET: Samples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samples = await _context.Samples
                .Include(s => s.IdMaterialNavigation)
                .SingleOrDefaultAsync(m => m.IdSample == id);
            if (samples == null)
            {
                return NotFound();
            }

            return View(samples);
        }
        [Authorize(Roles = "admin")]
        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var samples = await _context.Samples.SingleOrDefaultAsync(m => m.IdSample == id);
            _context.Samples.Remove(samples);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SamplesExists(int id)
        {
            return _context.Samples.Any(e => e.IdSample == id);
        }
    }
}
