using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_mvc.Models
{
    public class Ingrediente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Medida Medida { get; set; }
        public Receita Receita { get; set; }
    }
}