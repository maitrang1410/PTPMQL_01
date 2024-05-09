using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class HethongphanphoiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HethongphanphoiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hethongphanphoi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hethongphanphois.ToListAsync());
        }

        // GET: Hethongphanphoi/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hethongphanphoi = await _context.Hethongphanphois
                .FirstOrDefaultAsync(m => m.MaHTPP == id);
            if (hethongphanphoi == null)
            {
                return NotFound();
            }

            return View(hethongphanphoi);
        }

        // GET: Hethongphanphoi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hethongphanphoi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHTPP,TenHTPP")] Hethongphanphoi hethongphanphoi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hethongphanphoi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hethongphanphoi);
        }

        // GET: Hethongphanphoi/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hethongphanphoi = await _context.Hethongphanphois.FindAsync(id);
            if (hethongphanphoi == null)
            {
                return NotFound();
            }
            return View(hethongphanphoi);
        }

        // POST: Hethongphanphoi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHTPP,TenHTPP")] Hethongphanphoi hethongphanphoi)
        {
            if (id != hethongphanphoi.MaHTPP)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hethongphanphoi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HethongphanphoiExists(hethongphanphoi.MaHTPP))
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
            return View(hethongphanphoi);
        }

        // GET: Hethongphanphoi/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hethongphanphoi = await _context.Hethongphanphois
                .FirstOrDefaultAsync(m => m.MaHTPP == id);
            if (hethongphanphoi == null)
            {
                return NotFound();
            }

            return View(hethongphanphoi);
        }

        // POST: Hethongphanphoi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hethongphanphoi = await _context.Hethongphanphois.FindAsync(id);
            if (hethongphanphoi != null)
            {
                _context.Hethongphanphois.Remove(hethongphanphoi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HethongphanphoiExists(string id)
        {
            return _context.Hethongphanphois.Any(e => e.MaHTPP == id);
        }
    }
}
