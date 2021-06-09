using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SistemaReservaEnLinea.Models
{

	public partial class Reservas
	{
		public Reservas()
		{
			ReservaDetalle = new List<ReservaDetalle>();

		}
		[Key]
		public int Id { get; set; }
		public string Folio { get; set; }
		public string NoEvento { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaReservaInicia { get; set; }
		public DateTime FechaReservaFinaliza { get; set; }
		public decimal CostoTotal { get; set; }
		public string Estatus { get; set; }
		public string Email { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public string Telefono { get; set; }
		public string Pais { get; set; }
		public string Estado { get; set; }
		public string Municipio { get; set; }
		public string Direccion { get; set; }
		public string CodigoPostal { get; set; }
		public string RequerimientosEspeciales { get; set; }
		public string ReferenciaPago { get; set; }
		public int CantidadReservacion { get; set; }
		[ForeignKey("Lugar")]
		public int LugarId { get; set; }
		public virtual Lugar Lugar { get; set; }

		public virtual List<ReservaDetalle> ReservaDetalle { get; set; }
		
	}

	public class ReservaViewModel
    {
		public string Nombres { get; set; }
		public int Id { get; set; }
		public string NoEvento { get; set; }
		public int CantidadReservacion { get; set; }
		public decimal CostoTotal { get; set; }

		public List<ReservaDetalleViewModel> ReservaDetalle { get; set; }

		public decimal Total()
		{
			return ReservaDetalle.Sum(x => x.Monto());
		}

		public DateTime FechaCreacion { get; set; }

		public ReservaViewModel()
        {
			ReservaDetalle = new List<ReservaDetalleViewModel>();
			Refrescar();

		}
		public void Refrescar()
        {
			Id = 0;
			NoEvento = null;
			CantidadReservacion = 1;
			CostoTotal = 0;
        }
		public bool AddValidEvent()
        {
			return !(Id == 0 || string.IsNullOrEmpty(NoEvento) || CantidadReservacion == 0 || CostoTotal == 0);

		}

		public bool ExisteEnDetalle(int Id)
		{
			return ReservaDetalle.Any(x => x.Id == Id);
		}

		public void RetirarItemDeDetalle()
		{
			if (ReservaDetalle.Count > 0)
			{
				var detalleARetirar = ReservaDetalle.Where(x => x.Retirar)
														.SingleOrDefault();

				ReservaDetalle.Remove(detalleARetirar);
			}
		}

		public void AgregarItemADetalle()
		{
			ReservaDetalle.Add(new ReservaDetalleViewModel
			{
				Id = Id,
				NameEvento = NoEvento,
				CostoEvento = CostoTotal,
				TotalReserva = CantidadReservacion,
			});

			Refrescar();
		}

		public Reservas ToModel()
		{
			var reservas = new Reservas();
			reservas.Nombres = this.Nombres;
			reservas.FechaCreacion = DateTime.Now;
			reservas.CostoTotal = this.Total();

			foreach (var d in ReservaDetalle)
			{
				reservas.ReservaDetalle.Add(new ReservaDetalle
				{
					Id = d.Id,
					Monto = d.Monto(),
					CostoEvento = d.CostoEvento,
					TotalReserva = d.TotalReserva
				});
			}

			return reservas;
		}
	}
}

