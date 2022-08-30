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
    public class ReceitasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReceitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Administrador/Receitas")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var receitas = await _context.Receitas.Include(x => x.Ingredientes).ToListAsync();
            return View(receitas);
        }

        [Route("Administrador/Receitas/Detalhes")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        [Route("Administrador/Receitas/Nova")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ingredientes = await _context.Ingredientes.Select(x => new SelectListItem
            {
                Text = x.Nome,
                Value = x.Id.ToString()
            }).ToListAsync();

            return View();
        }

        [Route("Administrador/Receitas/Nova")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,UrlImagem,TempoPreparo,ModoPreparo,DataCriacao,IngredienteReceita,IngredienteId")] Receita receita)
        {
            if (ModelState.IsValid)
            {
                receita.Id = Guid.NewGuid();
                _context.Add(receita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receita);
        }

        [Route("Administrador/Receitas/Editar")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null)
            {
                return NotFound();
            }
            return View(receita);
        }

        [Route("Administrador/Receitas/Editar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,TempoPreparo,ModoPreparo,Status")] Receita receita)
        {
            if (id != receita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceitaExists(receita.Id))
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
            return View(receita);
        }

        [Route("Administrador/Receitas/Deletar")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receita = await _context.Receitas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receita == null)
            {
                return NotFound();
            }

            return View(receita);
        }

        [Route("Administrador/Receitas/Deletar")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var receita = await _context.Receitas.FindAsync(id);
            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceitaExists(Guid id)
        {
            return _context.Receitas.Any(e => e.Id == id);
        }
    }
}
