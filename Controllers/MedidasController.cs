using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using desafio_mvc.Data;
using desafio_mvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace desafio_mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MedidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MedidasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Administrador/Medidas")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medidas.ToListAsync());
        }

        [Route("Administrador/Medidas/Nova")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("Administrador/Medidas/Nova")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Status")] Medida medida)
        {
            if (ModelState.IsValid)
            {
                medida.Id = Guid.NewGuid();
                _context.Add(medida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medida);
        }

        [Route("Administrador/Medidas/Editar")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medida = await _context.Medidas.FindAsync(id);
            if (medida == null)
            {
                return NotFound();
            }
            return View(medida);
        }

        [Route("Administrador/Medidas/Editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Status")] Medida medida)
        {
            if (id != medida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedidaExists(medida.Id))
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
            return View(medida);
        }

        [Route("Administrador/Medidas/Deletar")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medida = await _context.Medidas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medida == null)
            {
                return NotFound();
            }

            return View(medida);
        }

        [Route("Administrador/Medidas/Deletar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var medida = await _context.Medidas.FindAsync(id);
            _context.Medidas.Remove(medida);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedidaExists(Guid id)
        {
            return _context.Medidas.Any(e => e.Id == id);
        }
    }
}
