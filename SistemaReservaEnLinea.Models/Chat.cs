using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
	public class Chat
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Usuarios")]
		public int UsuarioId { get; set; }
		public string Nombre { get; set; }
		public string Mensaje { get; set; }
		public DateTime Fecha { get; set; }
		public string De { get; set; }
		public string Para { get; set; }
		public virtual Usuarios Usuarios { get; set; }
	}
}
