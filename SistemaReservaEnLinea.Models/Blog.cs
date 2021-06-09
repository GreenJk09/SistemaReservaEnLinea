using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
	public class Blog
	{
		[Key]
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public string Post { get; set; }
		public DateTime Fecha { get; set; }
		public DateTime FechaModificacion { get; set; }
		public string Tipo { get; set; }
		public string Categoria { get; set; }
		public string Video { get; set; }
		public string Imagen { get; set; }
		public int Consultas { get; set; }
		public string Tags { get; set; }
		public string MetaKeys { get; set; }
		[ForeignKey("Usuarios")]
		public int UsuarioId { get; set; }
		public bool Activo { get; set; }
		[ForeignKey("Lugar")]
		public int? LugarId { get; set; }
		[ForeignKey("ServiciosExtras")]
		public int? ServicioId { get; set; }
		[ForeignKey("Eventos")]
		public int? EventoId { get; set; }
		public int? MeGusta { get; set; }
		public int? Comentarios { get; set; }
		public virtual Usuarios Usuarios { get; set; }
		public virtual Eventos Eventos { get; set; }
		public virtual Lugar Lugar { get; set; }
		public virtual ServiciosExtras ServiciosExtras { get; set; }

	}
}
