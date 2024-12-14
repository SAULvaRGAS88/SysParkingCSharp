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
    public class NotasFiscaisController : Controller
    {
        private readonly SysParkingC_Context _context;

        public NotasFiscaisController(SysParkingC_Context context)
        {
            _context = context;
        }

        // GET: NotasFiscais
        public async Task<IActionResult> Index()
        {
            var sysParkingC_Context = _context.NotaFiscal.Include(n => n.Carro);
            return View(await sysParkingC_Context.ToListAsync());
        }

        // GET: NotasFiscais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NotaFiscal == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscal
                .Include(n => n.Carro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // GET: NotasFiscais/Create
        public IActionResult Create()
        {
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Id");
            return View();
        }

        // POST: NotasFiscais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarroId,DataSaida,HoraSaida,Pagamento")] NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notaFiscal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroId"] = new SelectList(_context.Carro, "Id", "Id", notaFiscal.CarroId);
            return View(notaFiscal);
        }

        // GET: NotasFiscais/Edit/5
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
            return View(notaFiscal);
        }

        // POST: NotasFiscais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarroId,DataSaida,HoraSaida,Pagamento")] NotaFiscal notaFiscal)
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
            return View(notaFiscal);
        }

        // GET: NotasFiscais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NotaFiscal == null)
            {
                return NotFound();
            }

            var notaFiscal = await _context.NotaFiscal
                .Include(n => n.Carro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notaFiscal == null)
            {
                return NotFound();
            }

            return View(notaFiscal);
        }

        // POST: NotasFiscais/Delete/5
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

        public async Task<IActionResult> RemoverCarroEstacionado(int id)
        {
            if (_context.Carro == null)
            {
                return NotFound("A entidade 'Carro' não foi encontrada no contexto.");
            }

            var carro = await _context.Carro.FindAsync(id);

            if (carro == null)
            {
                return NotFound("O carro com o ID especificado não foi encontrado.");
            }

            _context.Carro.Remove(carro);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Carro removido com sucesso!";
            return RedirectToAction("Index"); 
        }


        private bool NotaFiscalExists(int id)
        {
          return (_context.NotaFiscal?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> GerarNota(int id, NotaFiscal model)
        {
            // Busca o carro no banco de dados pelo ID
            var carro = await _context.Carro.FirstOrDefaultAsync(c => c.Id == id);

            if (carro == null)
            {
                return NotFound("Carro não encontrado.");
            }


            // Calcula o tempo de permanência
            var tempoDePermanencia = DateTime.Now - carro.HoraEntrada;

            // Gera a nota fiscal
            var notaFiscal = new NotaFiscal
            {
                CarroId = carro.Id,
                DataSaida = DateTime.Now.Date,
                HoraSaida = DateTime.Now,
                TempoPermanencia = $"{(int)tempoDePermanencia.TotalHours}h {tempoDePermanencia.Minutes}m",
                ValorTotal = await CalcularValor(tempoDePermanencia, id), // Retorna o valor calculado
                Carro = carro,
                Pagamento = model.Pagamento
            };

            // Adiciona a nota fiscal ao banco de dados
            _context.NotaFiscal.Add(notaFiscal);
            await _context.SaveChangesAsync();

            // Retorna a View com os dados da nota fiscal
            return View(notaFiscal);
        }



        // Função para calcular o valor com base no tempo de permanência
        private async Task<double> CalcularValor(TimeSpan tempoDePermanencia, int id)
        {
            // Obtém o carro e o estacionamento associado
            var carro = await _context.Carro.FirstOrDefaultAsync(c => c.Id == id);
            if (carro == null)
            {
                throw new ArgumentException("Carro não encontrado.");
            }

            var estacionamento = await _context.Estacionamento.FirstOrDefaultAsync(e => e.Id == carro.EstacionamentoId);
            if (estacionamento == null)
            {
                throw new ArgumentException("Estacionamento não encontrado.");
            }

            // Cálculo baseado no tempo de permanência
            double totalHoras = tempoDePermanencia.TotalHours;

            // Exemplo: calcula tarifa considerando intervalos diferentes
            if (totalHoras <= 0.25) // Até 15 minutos
                return estacionamento.Preco15Min;
            else if (totalHoras <= 0.5) // Até 30 minutos
                return estacionamento.Preco30Min;
            else if (totalHoras <= 1) // Até 1 hora
                return estacionamento.Preco1Hora;
            else if (totalHoras <= 12) // Até 12 horas (meia diária)
                return estacionamento.PrecoDiaria / 2;
            else if (totalHoras <= 24) // Até 24 horas (diária completa)
                return estacionamento.PrecoDiaria;
            else if (totalHoras <= 72) // Até 3 dias (pernoite)
                return estacionamento.PrecoPernoite;
            else // Acima de 3 dias, calcula baseado no preço mensal
                return (totalHoras / (24 * 30)) * estacionamento.PrecoMensal;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GerarNotaFiscalConfirmada(int id, TipoPagamento pagamento)
        {
            // Buscar a nota fiscal vinculada ao carro
            var notaFiscal = await _context.NotaFiscal.FirstOrDefaultAsync(n => n.CarroId == id);

            if (notaFiscal == null)
            {
                return NotFound("Nota fiscal não encontrada.");
            }

            // Atualizar o campo de pagamento
            notaFiscal.Pagamento = pagamento;
            await _context.SaveChangesAsync();

            // Buscar o carro pelo ID e removê-lo
            var carro = await _context.Carro.FindAsync(id);
            if (carro != null)
            {
                _context.Carro.Remove(carro);
                await _context.SaveChangesAsync();
            }

            // Redirecionar para a página principal ou outra ação relevante
            return RedirectToAction("Index", "NotasFiscais");
        }



    }
}
