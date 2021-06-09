using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReservaEnLinea.ViewModels
{
	public class Lugar
	{
		public int Id { get; set; }
		public string Telefono { get; set; }
		public string Website { get; set; }
		public string Email { get; set; }
		public string PalabrasClave { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public string Direccion { get; set; }
		public string Ciudad { get; set; }
		public string CodigoPostal { get; set; }
		public decimal? Latitud { get; set; }
		public decimal? Longitud { get; set; }
		public string Facebook { get; set; }
		public string Twitter { get; set; }
		public string Pinterest { get; set; }
		public string LinkedIn { get; set; }
		public string WhatsApp { get; set; }
		public string Instagram { get; set; }
		public bool? Facturacion { get; set; }
		public int UsuarioId { get; set; }
		public decimal Calificacion { get; set; }
		public bool? Estatus { get; set; }
		public string Departamento { get; set; }
		public DateTime? Fecha { get; set; } 

		public int[]? Caracteristica { get; set; }
	}
}
