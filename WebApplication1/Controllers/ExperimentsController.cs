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
    public class ExperimentsController : Controller
    {
        private readonly recipeContext _context;

        public ExperimentsController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Experiments
        public IActionResult Index(int idexperiment, string namesample, DateTime? dates, TimeSpan? starttime, TimeSpan? endtime, double? supposemass, double? receivemass, int? numberlaboratory, string surname ,int page = 1, ExperimentsSortState sortOrder = ExperimentsSortState.IdSampleAsc)
        {
            int pageSize = 10;
            IQueryable<Experiments> source = _context.Experiments.Include(e => e.IdLaboratoryNavigation).Include(e => e.IdSampleNavigation).Include(e => e.IdWorkerNavigation);
            if (idexperiment != 0)
            {
                source = source.Where(x => x.IdExperiment == idexperiment);
            }
            if (namesample != null )
            {
                source = source.Where(x => x.IdSampleNavigation.NameSample == namesample);
            }
            if (dates != null)
            {
                source = source.Where(x => x.Dates == dates);
            }
            if (starttime != null)
            {
                source = source.Where(x => x.StartTime == starttime);
            }
            if (endtime != null)
            {
                source = source.Where(x => x.EndTime == endtime);
            }
            if (supposemass != null && supposemass != 0)
            {
                source = source.Where(x => x.SupposeMass.Value == supposemass.Value);
            }
            if (receivemass != null && receivemass != 0)
            {
                source = source.Where(x => x.ReceiveMass.Value == receivemass.Value);
            }
            if (numberlaboratory != null && numberlaboratory != 0)
            {
                source = source.Where(x => x.IdLaboratoryNavigation.NumberLaboratory == numberlaboratory);
            }
            if (surname != null )
            {
                source = source.Where(x => x.IdWorkerNavigation.Surname == surname);
            }

            switch (sortOrder)
            {
                case ExperimentsSortState.IdExperimentAsc:
                    source = source.OrderBy(x => x.IdExperiment);
                    break;
                case ExperimentsSortState.IdExperimentDesc:
                    source = source.OrderByDescending(x => x.IdExperiment);
                    break;
                case ExperimentsSortState.IdSampleAsc:
                    source = source.OrderBy(x => x.IdSample);
                    break;
                case ExperimentsSortState.IdSampleDesc:
                    source = source.OrderByDescending(x => x.IdSample);
                    break;
                case ExperimentsSortState.DatesAsc:
                    source = source.OrderBy(x => x.Dates);
                    break;
                case ExperimentsSortState.DatesDesc:
                    source = source.OrderBy(x => x.Dates);
                    break;
                case ExperimentsSortState.StartTimeAsc:
                    source = source.OrderBy(x => x.StartTime);
                    break;
                case ExperimentsSortState.StartTimeDesc:
                    source = source.OrderBy(x => x.StartTime);
                    break;
                case ExperimentsSortState.EndTimeAsc:
                    source = source.OrderBy(x => x.EndTime);
                    break;
                case ExperimentsSortState.EndTimeDesc:
                    source = source.OrderBy(x => x.EndTime);
                    break;
                case ExperimentsSortState.SupposeMassAsc:
                    source = source.OrderBy(x => x.SupposeMass);
                    break;
                case ExperimentsSortState.SupposeMassDesc:
                    source = source.OrderBy(x => x.SupposeMass);
                    break;
                case ExperimentsSortState.ReceiveMassAsc:
                    source = source.OrderBy(x => x.ReceiveMass);
                    break;
                case ExperimentsSortState.ReceiveMassDesc:
                    source = source.OrderBy(x => x.ReceiveMass);
                    break;
                case ExperimentsSortState.IdLaboratoryAsc:
                    source = source.OrderBy(x => x.IdLaboratory);
                    break;
                case ExperimentsSortState.IdLaboratoryDesc:
                    source = source.OrderBy(x => x.IdLaboratory);
                    break;
                case ExperimentsSortState.IdWorkerAsc:
                    source = source.OrderBy(x => x.IdWorker);
                    break;
                case ExperimentsSortState.IdWorkerDesc:
                    source = source.OrderBy(x => x.IdWorker);
                    break;
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            ExperimentsViewModel ivm = new ExperimentsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortExperimentsViewModel(sortOrder),
                FilterViewModel = new FilterExperimentsViewModel(idexperiment, namesample,dates,starttime, endtime, supposemass, receivemass, numberlaboratory, surname),
                Experiments = items
            };
            return View(ivm);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Experiments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiments = await _context.Experiments
                .Include(e => e.IdLaboratoryNavigation)
                .Include(e => e.IdSampleNavigation)
                .Include(e => e.IdWorkerNavigation)
                .SingleOrDefaultAsync(m => m.IdExperiment == id);
            if (experiments == null)
            {
                return NotFound();
            }

            return View(experiments);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Experiments/Create
        public IActionResult Create()
        {
            ViewData["IdLaboratory"] = new SelectList(_context.Laboratories, "IdLaboratory", "IdLaboratory");
            ViewData["IdSample"] = new SelectList(_context.Samples, "IdSample", "IdSample");
            ViewData["IdWorker"] = new SelectList(_context.Workers, "IdWorker", "IdWorker");
            return View();
        }

        // POST: Experiments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdExperiment,IdSample,Dates,StartTime,EndTime,SupposeMass,ReceiveMass,IdLaboratory,IdWorker")] Experiments experiments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experiments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLaboratory"] = new SelectList(_context.Laboratories, "IdLaboratory", "IdLaboratory", experiments.IdLaboratory);
            ViewData["IdSample"] = new SelectList(_context.Samples, "IdSample", "IdSample", experiments.IdSample);
            ViewData["IdWorker"] = new SelectList(_context.Workers, "IdWorker", "IdWorker", experiments.IdWorker);
            return View(experiments);
        }
        [Authorize(Roles = "admin")]
        // GET: Experiments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiments = await _context.Experiments.SingleOrDefaultAsync(m => m.IdExperiment == id);
            if (experiments == null)
            {
                return NotFound();
            }
            ViewData["IdLaboratory"] = new SelectList(_context.Laboratories, "IdLaboratory", "IdLaboratory", experiments.IdLaboratory);
            ViewData["IdSample"] = new SelectList(_context.Samples, "IdSample", "IdSample", experiments.IdSample);
            ViewData["IdWorker"] = new SelectList(_context.Workers, "IdWorker", "IdWorker", experiments.IdWorker);
            return View(experiments);
        }

        // POST: Experiments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdExperiment,IdSample,Dates,StartTime,EndTime,SupposeMass,ReceiveMass,IdLaboratory,IdWorker")] Experiments experiments)
        {
            if (id != experiments.IdExperiment)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experiments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentsExists(experiments.IdExperiment))
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
            ViewData["IdLaboratory"] = new SelectList(_context.Laboratories, "IdLaboratory", "IdLaboratory", experiments.IdLaboratory);
            ViewData["IdSample"] = new SelectList(_context.Samples, "IdSample", "IdSample", experiments.IdSample);
            ViewData["IdWorker"] = new SelectList(_context.Workers, "IdWorker", "IdWorker", experiments.IdWorker);
            return View(experiments);
        }
        [Authorize(Roles = "admin")]
        // GET: Experiments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiments = await _context.Experiments
                .Include(e => e.IdLaboratoryNavigation)
                .Include(e => e.IdSampleNavigation)
                .Include(e => e.IdWorkerNavigation)
                .SingleOrDefaultAsync(m => m.IdExperiment == id);
            if (experiments == null)
            {
                return NotFound();
            }

            return View(experiments);
        }
        [Authorize(Roles = "admin")]
        // POST: Experiments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experiments = await _context.Experiments.SingleOrDefaultAsync(m => m.IdExperiment == id);
            _context.Experiments.Remove(experiments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentsExists(int id)
        {
            return _context.Experiments.Any(e => e.IdExperiment == id);
        }
    }
}
