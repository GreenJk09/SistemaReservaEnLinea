using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
    public class LugaresEventosCaracteristicas
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Lugar")]
        public int LugarId { get; set; }
        [ForeignKey("Eventos")]
        public int? EventosId { get; set; }
        [ForeignKey("Cracteristicas")]
        public int CaracteristicaId { get; set; }
        public bool Activo { get; set; }
        public virtual Lugar Lugar { get; set; }
        public virtual Eventos Eventos { get; set; }
        public virtual Cracteristicas Cracteristicas { get; set; }
    }
}
