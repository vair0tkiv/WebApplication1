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
    public class ProvidersController : Controller
    {
        private readonly recipeContext _context;

        public ProvidersController(recipeContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "user, admin")]
        // GET: Providers
        public IActionResult Index(string surname, string firstname, string adress, string birthday, int page = 1, ProvidersSortState sortOrder = ProvidersSortState.SurnameAsc)
        {
            int pageSize = 10;
            IQueryable<Providers> source = _context.Providers;

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
                case ProvidersSortState.SurnameAsc:
                    source = source.OrderBy(x => x.Surname);
                    break;
                case ProvidersSortState.SurnameDesc:
                    source = source.OrderByDescending(x => x.Surname);
                    break;
                case ProvidersSortState.FirstnameAsc:
                    source = source.OrderBy(x => x.Firstname);
                    break;
                case ProvidersSortState.FirstnsmeDesc:
                    source = source.OrderByDescending(x => x.Firstname);
                    break;
                case ProvidersSortState.AdressAsc:
                    source = source.OrderBy(x => x.Adress);
                    break;
                case ProvidersSortState.AdressDesc:
                    source = source.OrderByDescending(x => x.Adress);
                    break;
                case ProvidersSortState.BirthdayAsc:
                    source = source.OrderBy(x => x.Birthday);
                    break;
                case ProvidersSortState.BirthdayDesc:
                    source = source.OrderByDescending(x => x.Birthday);
                    break;
                default:
                    source = source.OrderBy(x => x.Surname);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            ProvidersViewModel ivm = new ProvidersViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortProvidersViewModel(sortOrder),
                FilterViewModel = new FilterProvidersViewModel(surname, firstname, adress, birthday),
                Providers = items
            };
            return View(ivm);
        }

        [Authorize(Roles = "user, admin")]
        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers
                .SingleOrDefaultAsync(m => m.IdProvider == id);
            if (providers == null)
            {
                return NotFound();
            }

            return View(providers);
        }
        [Authorize(Roles = "user, admin")]
        // GET: Providers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "user, admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProvider,Surname,Firstname,Adress,Birthday")] Providers providers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(providers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(providers);
        }
        [Authorize(Roles = "admin")]
        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers.SingleOrDefaultAsync(m => m.IdProvider == id);
            if (providers == null)
            {
                return NotFound();
            }
            return View(providers);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProvider,Surname,Firstname,Adress,Birthday")] Providers providers)
        {
            if (id != providers.IdProvider)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(providers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvidersExists(providers.IdProvider))
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
            return View(providers);
        }
        [Authorize(Roles = "admin")]
        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers
                .SingleOrDefaultAsync(m => m.IdProvider == id);
            if (providers == null)
            {
                return NotFound();
            }

            return View(providers);
        }
        [Authorize(Roles = "admin")]
        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var providers = await _context.Providers.SingleOrDefaultAsync(m => m.IdProvider == id);
            _context.Providers.Remove(providers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvidersExists(int id)
        {
            return _context.Providers.Any(e => e.IdProvider == id);
        }
    }
}
