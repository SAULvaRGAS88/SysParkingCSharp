using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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


        private int ConverterTempoParaMinutos(string tempo)
        {
            Console.WriteLine($"Convertendo tempo: {tempo}");

            if (string.IsNullOrEmpty(tempo))
            {
                Console.WriteLine("Tempo inválido ou vazio.");
                return 0;
            }

            var regex = new Regex(@"(?:(\d+)h)?\s*(?:(\d+)m)?");
            var match = regex.Match(tempo);

            if (match.Success)
            {
                var horas = int.TryParse(match.Groups[1].Value, out var h) ? h : 0;
                var minutos = int.TryParse(match.Groups[2].Value, out var m) ? m : 0;

                Console.WriteLine($"Horas: {horas}, Minutos: {minutos}");

                return horas * 60 + minutos;
            }

            Console.WriteLine("Regex não teve sucesso.");
            return 0;
        }

        public async Task<IActionResult> RelatorioGeral()
        {
            try
            {
                Console.WriteLine("Consultando total de veículos...");
                var totalVeiculosEstacionados = await _context.NotaFiscal.CountAsync();
                Console.WriteLine($"Total de Veículos Estacionados: {totalVeiculosEstacionados}");

                Console.WriteLine("Consultando total arrecadado...");
                var totalArrecadado = await _context.NotaFiscal
                    .SumAsync(t => t.ValorTotal) ?? 0;
                Console.WriteLine($"Total Arrecadado: {totalArrecadado}");

                Console.WriteLine("Consultando tempo total de permanência...");
                // Força a execução da consulta no cliente
                var totalMinutos = _context.NotaFiscal
                    .AsEnumerable() // Carrega os dados para memória
                    .Select(t => ConverterTempoParaMinutos(t.TempoPermanencia)) // Aplica a função
                    .Sum(); // Soma os resultados
                Console.WriteLine($"Tempo Total em minutos: {totalMinutos}");

                var relatorio = new Relatorio
                {
                    Id = 1,
                    TotalVeiculosEstacionados = totalVeiculosEstacionados,
                    TotalArrecadado = totalArrecadado,
                    TempoTotalPermanenciaHoras = totalMinutos 
                };

                Console.WriteLine($"Dados preparados: TotalVeiculosEstacionados={relatorio.TotalVeiculosEstacionados}, TotalArrecadado={relatorio.TotalArrecadado}, TempoTotalPermanenciaHoras={relatorio.TempoTotalPermanenciaHoras}");
                return View(new List<Relatorio> { relatorio });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return View(new List<Relatorio>());
            }
        }

        public static string FormatarTempo(int totalMinutos)
        {
            int horas = totalMinutos / 60;
            int minutos = totalMinutos % 60;
            return $"{horas}h {minutos}min";
        }
    }
}
