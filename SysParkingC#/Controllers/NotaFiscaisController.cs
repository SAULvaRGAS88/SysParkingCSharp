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
    public class NotaFiscaisController : Controller
    {
        private readonly SysParkingC_Context _context;

        public NotaFiscaisController(SysParkingC_Context context)
        {
            _context = context;
        }

        // GET: NotaFiscais
        public async Task<IActionResult> Index()
        {
            var sysParkingC_Context = _context.NotaFiscal.Include(n => n.Carro).Include(n => n.Estacionamento);
            return View(await sysParkingC_Context.ToListAsync());
        }

        // GET: NotaFiscais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NotaFiscal == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscal
                .Include(n => n.Carro)
                .Include(n => n.Estacionamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // GET: NotaFiscais/Create
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Id");
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id");
            return View();
        }

        // POST: NotaFiscais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarroId,EstacionamentoId,DataSaida,HoraSaida,Pagamento")] NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notaFiscal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Id", notaFiscal.CarroId);
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id", notaFiscal.EstacionamentoId);
            return View(notaFiscal);
        }

        // GET: NotaFiscais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NotaFiscal == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscal.FindAsync(id);
            if (notaFiscal == null)
            {
                return NotFound();
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Id", notaFiscal.CarroId);
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id", notaFiscal.EstacionamentoId);
            return View(notaFiscal);
        }

        // POST: NotaFiscais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarroId,EstacionamentoId,DataSaida,HoraSaida,Pagamento")] NotaFiscal notaFiscal)
        {
            if (id != notaFiscal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notaFiscal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotaFiscalExists(notaFiscal.Id))
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
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Id", notaFiscal.CarroId);
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id", notaFiscal.EstacionamentoId);
            return View(notaFiscal);
        }

        // GET: NotaFiscais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NotaFiscal == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscal
                .Include(n => n.Carro)
                .Include(n => n.Estacionamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // POST: NotaFiscais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NotaFiscal == null)
            {
                return Problem("Entity set 'SysParkingC_Context.NotaFiscal'  is null.");
            }
            var notaFiscal = await _context.NotaFiscal.FindAsync(id);
            if (notaFiscal != null)
            {
                _context.NotaFiscal.Remove(notaFiscal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotaFiscalExists(int id)
        {
          return (_context.NotaFiscal?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
