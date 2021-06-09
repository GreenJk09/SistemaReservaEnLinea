using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
	public class ServiciosExtras
	{
		[Key]
		public int Id { get; set; }
		public string Servicio { get; set; }
		public decimal Costo { get; set; }
		[ForeignKey("Lugar")]
		public int? LugarId { get; set; }
		[ForeignKey("Habitaciones")]
		public int? HabitacionId { get; set; }
		public string Descripcion { get; set; }
		[ForeignKey("Usuarios")]
		public int? UsuarioId { get; set; }
		public bool Activio { get; set; }
		public virtual Lugar Lugar { get; set; }
		public virtual Eventos Eventos { get; set; }
		public virtual Usuarios Usuarios { get; set; }
	}
}
