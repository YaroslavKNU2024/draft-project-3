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
    public class VariantsController : Controller
    {
        private readonly CovidContext _context;

        public VariantsController(CovidContext context)
        {
            _context = context;
        }

        // GET: Variants
        //public async Task<IActionResult> Index()
        //{
        //    var covidContext = _context.Variants.Include(v => v.IdVirusNavigation);
        //    return View(await covidContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Viruses", "Index");
            ViewBag.IdVirus = id;
            ViewBag.VirusName = name;
            var variantsByVirus = _context.Variants.Where(v => v.IdVirus == id);

            return View(await variantsByVirus.ToListAsync());
        }

        // GET: Variants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variant = await _context.Variants
                .Include(v => v.IdVirusNavigation)
                .FirstOrDefaultAsync(m => m.IdVariant == id);
            if (variant == null)
            {
                return NotFound();
            }

            return View(variant);
        }

        // GET: Variants/Create
        public IActionResult Create()
        {
            ViewData["IdVirus"] = new SelectList(_context.Viruses, "IdVirus", "VirusName");
            return View();
        }

        // POST: Variants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVariant,VariantName,VariantOrigin,IdVirus,DateDiscovered")] Variant variant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(variant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdVirus"] = new SelectList(_context.Viruses, "IdVirus", "VirusName", variant.IdVirus);
            return View(variant);
        }

        // GET: Variants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variant = await _context.Variants.FindAsync(id);
            if (variant == null)
            {
                return NotFound();
            }
            ViewData["IdVirus"] = new SelectList(_context.Viruses, "IdVirus", "VirusName", variant.IdVirus);
            return View(variant);
        }

        // POST: Variants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVariant,VariantName,VariantOrigin,IdVirus,DateDiscovered")] Variant variant)
        {
            if (id != variant.IdVariant)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(variant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VariantExists(variant.IdVariant))
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
            ViewData["IdVirus"] = new SelectList(_context.Viruses, "IdVirus", "VirusName", variant.IdVirus);
            return View(variant);
        }

        // GET: Variants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var variant = await _context.Variants
                .Include(v => v.IdVirusNavigation)
                .FirstOrDefaultAsync(m => m.IdVariant == id);
            if (variant == null)
            {
                return NotFound();
            }

            return View(variant);
        }

        // POST: Variants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var variant = await _context.Variants.FindAsync(id);
            _context.Variants.Remove(variant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VariantExists(int id)
        {
            return _context.Variants.Any(e => e.IdVariant == id);
        }
    }
}
