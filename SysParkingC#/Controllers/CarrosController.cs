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
    public class CarrosController : Controller
    {
        private readonly SysParkingC_Context _context;

        public CarrosController(SysParkingC_Context context)
        {
            _context = context;
        }

        // GET: Carros
        public async Task<IActionResult> Index()
        {
            var sysParkingC_Context = _context.Carro.Include(c => c.Estacionamento);
            return View(await sysParkingC_Context.ToListAsync());
        }

        // GET: Carros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carro == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro
                .Include(c => c.Estacionamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // GET: Carros/Create
        public IActionResult Create()
        {
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id");
            return View();
        }

        // POST: Carros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marca,Modelo,Cor,Placa,DataEntrada,HoraEntrada,EstacionamentoId")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id", carro.EstacionamentoId);
            return View(carro);
        }

        // GET: Carros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carro == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro.FindAsync(id);
            if (carro == null)
            {
                return NotFound();
            }
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id", carro.EstacionamentoId);
            return View(carro);
        }

        // POST: Carros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Modelo,Cor,Placa,DataEntrada,HoraEntrada,EstacionamentoId")] Carro carro)
        {
            if (id != carro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarroExists(carro.Id))
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
            ViewData["EstacionamentoId"] = new SelectList(_context.Estacionamento, "Id", "Id", carro.EstacionamentoId);
            return View(carro);
        }

        // GET: Carros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carro == null)
            {
                return NotFound();
            }

            var carro = await _context.Carro
                .Include(c => c.Estacionamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carro == null)
            {
                return NotFound();
            }

            return View(carro);
        }

        // POST: Carros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carro == null)
            {
                return Problem("Entity set 'SysParkingC_Context.Carro'  is null.");
            }
            var carro = await _context.Carro.FindAsync(id);
            if (carro != null)
            {
                _context.Carro.Remove(carro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarroExists(int id)
        {
          return (_context.Carro?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> EntrarNoEstacionamento(int id)
        {
            var carro = await _context.Carro.FirstOrDefaultAsync(c => c.Id == id);

            if (carro == null)
            {
                return NotFound();
            }

            // Atualizar HoraEntrada no momento correto
            carro.HoraEntrada = DateTime.Now;

            _context.Update(carro);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Redirecionando para uma lista ou página relevante
        }

        public async Task<double> CalculaCustoPermanencia(int id)
        {
            var Estacionamento = await _context.Estacionamento.FirstOrDefaultAsync(c => c.Id == id);

            if (Estacionamento == null)
            {
                Console.WriteLine("Estacionamento não está carregado.");
                return 1;
            }

            
            //double tempoEmMinutos = TempoPermanencia;
var carro = await _context.Carro.FirstOrDefaultAsync(c => c.Id == id);
var tempoEmMinutos = (DateTime.Now - carro.HoraEntrada).TotalMinutes;
            if (tempoEmMinutos <= 15) return Estacionamento.Preco15Min;
            if (tempoEmMinutos <= 30) return Estacionamento.Preco30Min;
            if (tempoEmMinutos <= 60) return Estacionamento.Preco1Hora;
            if (tempoEmMinutos > 60 && tempoEmMinutos <= 24 * 60) return Estacionamento.PrecoDiaria;
            if (tempoEmMinutos > 24 * 60) return Estacionamento.PrecoPernoite;

            return 0;
        }
    }

}
