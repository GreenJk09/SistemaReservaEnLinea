using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReservaEnLinea.ViewModels
{
	public class Eventos
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string PalabrasClave { get; set; }
		public string Descripcion { get; set; }
		public decimal Costo { get; set; }
		public int Catedraticos { get; set; }
		public int Estudiantes { get; set; }
		public int Invitados { get; set; }
		public int MaximoCatedraticos { get; set; }
		public int MaximoEstudiantes { get; set; }
		public int MaximoInvitados { get; set; }
		public int CostoAdicionalCatedraticos { get; set; }
		public int CostoAdicionalEstudiantes { get; set; }
		public int CostoAdicionalInvitados { get; set; }
		public int TotalEventos { get; set; }
		public int Seccion1 { get; set; }
		public int Seccion2 { get; set; }
		public int Seccion3 { get; set; }
		public int Seccion4 { get; set; }
		public decimal Calificacion { get; set; }
		public bool Activo { get; set; }
		public int LugarId { get; set; }
		public int[]? Caracteristica { get; set; }
	}
}
