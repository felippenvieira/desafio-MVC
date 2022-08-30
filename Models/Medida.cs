using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_mvc.Models
{
    public class Medida
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}