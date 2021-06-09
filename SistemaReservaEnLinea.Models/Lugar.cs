
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
    public class Lugar
    {
		[Key]
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
		[ForeignKey("Usuarios")]
		public int UsuarioId { get; set; }
		public decimal? Calificacion { get; set; }
		public bool? Estatus { get; set; }
		public string Departamento { get; set; }
		public DateTime? Fecha { get; set; }
		public virtual Usuarios Usuarios { get; set; }
		public virtual ICollection<ImagenesAsociadas> ImagenesAsociadas { get; set; }
		public virtual ICollection<Eventos> Evento { get; set; }
		public virtual ICollection<Comentarios> Comentarios { get; set; }
	}
}
