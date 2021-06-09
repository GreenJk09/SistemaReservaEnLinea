using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
    public class Suscripciones
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }
    }
}
