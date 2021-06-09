using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{

	public class Comentarios
	{
		[Key]
		public int Id { get; set; }
		public string Comentario { get; set; }
		public string Nombre { get; set; }
		public string Email { get; set; }
		public bool Activo { get; set; }
		[ForeignKey("Lugar")]
		public int? LugarId { get; set; }
		[ForeignKey("Eventos")]
		public int? EventoId { get; set; }
		[ForeignKey("Blog")]
		public int? BlogId { get; set; }
		public DateTime Fecha { get; set; }
		public int? ParentId { get; set; }
		public virtual Eventos Eventos { get; set; }
		public virtual Lugar Lugar { get; set; }
		public virtual Blog Blog { get; set; }
	}
}
