#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoronavirusBase;

namespace CoronavirusBase.Controllers
{
    public class VirusesController : Controller
    {
        private readonly CovidContext _context;

        public VirusesController(CovidContext context)
        {
            _context = context;
        }

        // GET: Viruses
        public async Task<IActionResult> Index()
        {
            var covidContext = _context.Viruses.Include(v => v.IdVirusGroupNavigation);
            return View(await covidContext.ToListAsync());
        }

        // GET: Viruses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virus = await _context.Viruses
                .Include(v => v.IdVirusGroupNavigation)
                .FirstOrDefaultAsync(m => m.IdVirus == id);
            if (virus == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Variants", new { id = virus.IdVirus, name = virus.VirusName });

        }

        // GET: Viruses/Create
        public IActionResult Create()
        {
            ViewData["IdVirusGroup"] = new SelectList(_context.VirusGroups, "IdGroup", "IdGroup");
            return View();
        }

        // POST: Viruses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVirus,VirusName,DateDiscovered,IdVirusGroup")] Virus virus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(virus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVirusGroup"] = new SelectList(_context.VirusGroups, "IdGroup", "IdGroup", virus.IdVirusGroup);
            return View(virus);
        }

        // GET: Viruses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virus = await _context.Viruses.FindAsync(id);
            if (virus == null)
            {
                return NotFound();
            }
            ViewData["IdVirusGroup"] = new SelectList(_context.VirusGroups, "IdGroup", "IdGroup", virus.IdVirusGroup);
            return View(virus);
        }

        // POST: Viruses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVirus,VirusName,DateDiscovered,IdVirusGroup")] Virus virus)
        {
            if (id != virus.IdVirus)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(virus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VirusExists(virus.IdVirus))
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
            ViewData["IdVirusGroup"] = new SelectList(_context.VirusGroups, "IdGroup", "IdGroup", virus.IdVirusGroup);
            return View(virus);
        }

        // GET: Viruses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virus = await _context.Viruses
                .Include(v => v.IdVirusGroupNavigation)
                .FirstOrDefaultAsync(m => m.IdVirus == id);
            if (virus == null)
            {
                return NotFound();
            }

            return View(virus);
        }

        // POST: Viruses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var virus = await _context.Viruses.FindAsync(id);
            _context.Viruses.Remove(virus);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VirusExists(int id)
        {
            return _context.Viruses.Any(e => e.IdVirus == id);
        }
    }
}
