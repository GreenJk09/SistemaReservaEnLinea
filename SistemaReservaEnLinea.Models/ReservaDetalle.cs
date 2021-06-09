using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
	public partial class ReservaDetalle
	{
		[Key]
		public int Id { get; set; }
		public int TotalReserva { get; set; }
		public int Catedraticos { get; set; }
		public int Estudiantes { get; set; }
		public int Invitados { get; set; }
		public decimal CostoEvento { get; set; }
		public decimal Monto { get; set; }
		public string NameEvento { get; set; }
		[ForeignKey("Reservas")]
		public int ReservaId { get; set; }
		[ForeignKey("Eventos")]
		public int EventoId { get; set; }
		public virtual Eventos Eventos { get; set; }
		public virtual Reservas Reservas { get; set; }
	}

	public  class ReservaDetalleViewModel
	{
		public int Id { get; set; }
		public string NameEvento { get; set; }
		public int TotalReserva { get; set; }
		public decimal CostoEvento { get; set; }
		public bool Retirar { get; set; }
		public decimal Monto()
		{
			return TotalReserva * CostoEvento;
		}
	}
}
