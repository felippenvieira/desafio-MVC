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
    public class IngredientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Administrador/Ingredientes")]
        public async Task<IActionResult> Index()
        {
            var ingredientes = await _context.Ingredientes.Include(x => x.Medida).ToListAsync();
            return View(ingredientes);
        }

        [Route("Administrador/Ingredientes/Novo")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Medida = await _context.Medidas.Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToListAsync();
            return View();
        }

        [Route("Administrador/Ingredientes/Novo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,MedidaId")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                ingrediente.Id = Guid.NewGuid();
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        [Route("Administrador/Ingredientes/Editar")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Medida = await _context.Medidas.Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToListAsync();

            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            return View(ingrediente);
        }

        [Route("Administrador/Ingredientes/Editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,MedidaId")] Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.Id))
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
            return View(ingrediente);
        }

        [Route("Administrador/Ingredientes/Deletar")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        [Route("Administrador/Ingredientes/Deletar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteExists(Guid id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }
    }
}
