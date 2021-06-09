using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
	public class Imagenes
	{
		[Key]
		public int Id { get; set; }
		public string Imagen { get; set; }
		public string Nombre { get; set; }
		[ForeignKey("Lugar")]
		public int? LugarId { get; set; }
		[ForeignKey("Eventos")]
		public int? EventoId { get; set; }
		[ForeignKey("ServiciosExtras")]
		public int? ServicioId { get; set; }
		[ForeignKey("Blog")]
		public int? BlogId { get; set; }
		public bool Activo { get; set; }
		[ForeignKey("Usuarios")]
		public int UsuarioId { get; set; }
		public virtual Blog Blog { get; set; }
		public virtual Eventos Eventos { get; set; }
		public virtual Lugar Lugar { get; set; }
		public virtual ServiciosExtras ServiciosExtras { get; set; }
		public virtual Usuarios Usuarios { get; set; }		
	}
}
