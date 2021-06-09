using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
    public class Cracteristicas
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
