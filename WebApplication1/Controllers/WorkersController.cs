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
    public class WorkersController : Controller
    {
        private readonly recipeContext _context;

        public WorkersController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Materials
        public IActionResult Index(string surname, string firstname, string adress, string birthday, int page = 1, WorkersSortState sortOrder = WorkersSortState.SurnameAsc)
        {
            int pageSize = 10;
            IQueryable<Workers> source = _context.Workers;

            if (surname != null)
            {
                source = source.Where(x => x.Surname.Contains(surname));
            }

            if (firstname != null)
            {
                source = source.Where(x => x.Firstname.Contains(firstname));
            }

            if (adress != null)
            {
                source = source.Where(x => x.Adress.Contains(adress));
            }

            if (birthday != null)
            {
                source = source.Where(x => x.Birthday == DateTime.Parse(birthday));
            }

            switch (sortOrder)
            {
                case WorkersSortState.SurnameAsc:
                    source = source.OrderBy(x => x.Surname);
                    break;
                case WorkersSortState.SurnameDesc:
                    source = source.OrderByDescending(x => x.Surname);
                    break;
                case WorkersSortState.FirstnameAsc:
                    source = source.OrderBy(x => x.Firstname);
                    break;
                case WorkersSortState.FirstnsmeDesc:
                    source = source.OrderByDescending(x => x.Firstname);
                    break;
                case WorkersSortState.AdresslAsc:
                    source = source.OrderBy(x => x.Adress);
                    break;
                case WorkersSortState.AdresslDesc:
                    source = source.OrderByDescending(x => x.Adress);
                    break;
                case WorkersSortState.BirthdayAsc:
                    source = source.OrderBy(x => x.Birthday);
                    break;
                case WorkersSortState.BirthdayDesc:
                    source = source.OrderByDescending(x => x.Birthday);
                    break;
                default:
                    source = source.OrderBy(x => x.Surname);
                    break;
            }

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            WorkersViewModel ivm = new WorkersViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortWorkersViewModel(sortOrder),
                FilterViewModel = new FilterWorkersViewModel(surname, firstname, adress, birthday),
                Workers = items
            };
            return View(ivm);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workers = await _context.Workers
                .SingleOrDefaultAsync(m => m.IdWorker == id);
            if (workers == null)
            {
                return NotFound();
            }

            return View(workers);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdWorker,Surname,Firstname,Adress,Birthday")] Workers workers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workers);
        }
        [Authorize(Roles = "admin")]
        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workers = await _context.Workers.SingleOrDefaultAsync(m => m.IdWorker == id);
            if (workers == null)
            {
                return NotFound();
            }
            return View(workers);
        }

        // POST: Workers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdWorker,Surname,Firstname,Adress,Birthday")] Workers workers)
        {
            if (id != workers.IdWorker)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkersExists(workers.IdWorker))
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
            return View(workers);
        }
        [Authorize(Roles = "admin")]
        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workers = await _context.Workers
                .SingleOrDefaultAsync(m => m.IdWorker == id);
            if (workers == null)
            {
                return NotFound();
            }

            return View(workers);
        }
        [Authorize(Roles = "admin")]
        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workers = await _context.Workers.SingleOrDefaultAsync(m => m.IdWorker == id);
            _context.Workers.Remove(workers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkersExists(int id)
        {
            return _context.Workers.Any(e => e.IdWorker == id);
        }
    }
}
