using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebConversor.Models;

namespace WebConversor.Controllers
{
    public class MonedasController : Controller
    {
        private readonly DbContexto _context;

        public MonedasController(DbContexto context)
        {
            _context = context;
        }

        // GET: Monedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Monedas.ToListAsync());
        }

        // GET: Monedas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneda = await _context.Monedas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneda == null)
            {
                return NotFound();
            }

            return View(moneda);
        }

        // GET: Monedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Simbolo")] Moneda moneda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moneda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moneda);
        }

        // GET: Monedas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneda = await _context.Monedas.FindAsync(id);
            if (moneda == null)
            {
                return NotFound();
            }
            return View(moneda);
        }

        // POST: Monedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Simbolo")] Moneda moneda)
        {
            if (id != moneda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonedaExists(moneda.Id))
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
            return View(moneda);
        }

        // GET: Monedas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moneda = await _context.Monedas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneda == null)
            {
                return NotFound();
            }

            return View(moneda);
        }

        // POST: Monedas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moneda = await _context.Monedas.FindAsync(id);
            if (moneda != null)
            {
                _context.Monedas.Remove(moneda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonedaExists(int id)
        {
            return _context.Monedas.Any(e => e.Id == id);
        }
    }
}
