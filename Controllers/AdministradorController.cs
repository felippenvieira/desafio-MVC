using System;
using System.Linq;
using desafio_mvc.Data;
using desafio_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministradorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministradorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // [HttpGet]
        // public IActionResult Receitas()
        // {
        //     var receitas = _context.Receitas.Include(x => x.IngredienteReceitas).ToList();
        //     return View(receitas);
        // }

        public IActionResult NovaReceita()
        {
            ViewBag.Ingredientes = _context.Ingredientes.ToList();
            ViewBag.Medidas = _context.Medidas.ToList();
            return View();
        }

        public IActionResult EditarReceita(Receita receita, Guid id)
        {
            if (!ModelState.IsValid || receita.Id != id) return BadRequest(ModelState);

            _context.Entry(receita).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return View(receita);
        }

        public IActionResult Ingredientes()
        {
            var ingredientes = _context.Ingredientes.ToList();
            return View(ingredientes);
        }

        public IActionResult NovoIngrediente()
        {
            return View();
        }

        public IActionResult EditarIngrediente(Ingrediente ingrediente, Guid id)
        {
            if (!ModelState.IsValid || ingrediente.Id != id) return BadRequest(ModelState);

            _context.Entry(ingrediente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        public IActionResult Medidas()
        {
            var medidas = _context.Medidas.ToList();
            return View(medidas);
        }

        public IActionResult NovaMedida()
        {
            return View();
        }

        public IActionResult EditarMedida(Medida medida, Guid id)
        {
            if (!ModelState.IsValid || medida.Id != id) return BadRequest(ModelState);

            _context.Entry(medida).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return View(medida);
        }
    }
}
