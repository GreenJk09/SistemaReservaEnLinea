using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
	public class ImagenesAsociadas
    {
		[Key]
		public int Id { get; set; }
		[ForeignKey("Imagenes")]
		public int ImagenId { get; set; }
		[ForeignKey("Lugar")]
		public int? LugarId { get; set; }
		[ForeignKey("Eventos")]
		public int? EventoId { get; set; }
		[ForeignKey("ServiciosExtras")]
		public int? ServicioId { get; set; }
		[ForeignKey("Blog")]
		public int? BlogId { get; set; }
		public bool Activo { get; set; }		
		public virtual Imagenes Imagenes { get; set; }
		public virtual Blog Blog { get; set; }
		public virtual Eventos Eventos { get; set; }
		public virtual Lugar Lugar { get; set; }
		public virtual ServiciosExtras ServiciosExtras { get; set; }
	}
}
