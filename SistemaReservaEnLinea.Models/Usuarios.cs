using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
    public class Usuarios
    {
		[Key]
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool Estatus { get; set; }
		public int? UsuarioIdPadre { get; set; }
		public string Foto { get; set; }
		public string Puesto { get; set; }
		public DateTime Fecha { get; set; }

		public string ColorTema { get; set; }
	}
}
