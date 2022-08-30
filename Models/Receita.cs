using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_mvc.Models
{
    public class Receita
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UrlImagem { get; set; }
        public string TempoPreparo { get; set; }
        public string ModoPreparo { get; set; }
        public DateTime DataCriacao { get; set; }
        public IList<Ingrediente> Ingredientes { get; set; }
    }
}