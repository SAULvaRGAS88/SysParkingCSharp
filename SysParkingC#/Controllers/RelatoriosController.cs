using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysParkingC_.Data;
using SysParkingC_.Models;

namespace SysParkingC_.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly SysParkingC_Context _context;

        public RelatoriosController(SysParkingC_Context context)
        {
            _context = context;
        }

        // GET: Relatorios
        public async Task<IActionResult> Index()
        {
              return _context.Relatorio != null ? 
                          View(await _context.Relatorio.ToListAsync()) :
                          Problem("Entity set 'SysParkingC_Context.Relatorio'  is null.");
        }

        // GET: Relatorios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Relatorio == null)
            {
                return NotFound();
            }

            var relatorio = await _context.Relatorio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relatorio == null)
            {
                return NotFound();
            }

            return View(relatorio);
        }

        // GET: Relatorios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Relatorios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalVeiculosEstacionados,TotalArrecadado,TempoTotalPermanenciaHoras,PeriodoInicio,PeriodoFim")] Relatorio relatorio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relatorio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relatorio);
        }

        // GET: Relatorios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Relatorio == null)
            {
                return NotFound();
            }

            var relatorio = await _context.Relatorio.FindAsync(id);
            if (relatorio == null)
            {
                return NotFound();
            }
            return View(relatorio);
        }

        // POST: Relatorios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalVeiculosEstacionados,TotalArrecadado,TempoTotalPermanenciaHoras,PeriodoInicio,PeriodoFim")] Relatorio relatorio)
        {
            if (id != relatorio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relatorio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelatorioExists(relatorio.Id))
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
            return View(relatorio);
        }

        // GET: Relatorios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Relatorio == null)
            {
                return NotFound();
            }

            var relatorio = await _context.Relatorio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relatorio == null)
            {
                return NotFound();
            }

            return View(relatorio);
        }

        // POST: Relatorios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Relatorio == null)
            {
                return Problem("Entity set 'SysParkingC_Context.Relatorio'  is null.");
            }
            var relatorio = await _context.Relatorio.FindAsync(id);
            if (relatorio != null)
            {
                _context.Relatorio.Remove(relatorio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelatorioExists(int id)
        {
          return (_context.Relatorio?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
