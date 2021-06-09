using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools.Services;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class BuscarEventoController : Controller
    {
        private readonly ILogger<BuscarEventoController> _logger;
        private readonly DataContextLPL _dbContext;
        private readonly IEmailSender _emailSender;


        public BuscarEventoController(ILogger<BuscarEventoController> logger, DataContextLPL dataContext, IEmailSender emailSender)
        {
            _logger = logger;
            _dbContext = dataContext;
            _emailSender = emailSender;
        }
        public IActionResult Index(string fecha, string fecha2, int? catedraticos, int? estudiantes)
        {

            DateTime _fecha = DateTime.Now;
            if (!string.IsNullOrEmpty(fecha))
                DateTime.TryParse(fecha, out _fecha);

            DateTime _fecha2 = DateTime.Now.AddDays(2);
            if (!string.IsNullOrEmpty(fecha2))
                DateTime.TryParse(fecha2, out _fecha2);


            _fecha2 = _fecha >= _fecha2 ? DateTime.Now.AddDays(2) : _fecha2;
            catedraticos = catedraticos ?? 1;
            estudiantes = estudiantes ?? 0;
            ViewBag.fecha = _fecha.ToString("dd/MM/yyyy");
            ViewBag.fecha2 = _fecha2.ToString("dd/MM/yyyy");
            ViewBag.catedraticos = catedraticos.ToString();
            ViewBag.estudiantes = estudiantes.ToString();
            ViewBag.Page = "Eventos";
            var blog = _dbContext.Blog.Where(c => c.Categoria != "configuracion").OrderByDescending(c => c.FechaModificacion).FirstOrDefault() ?? new Blog();
            var config = _dbContext.Configuracion.FirstOrDefault() ?? new Configuracion();
            ViewBag.Config = config;
            List<Eventos> lstEventos = new List<Eventos>();
            var idH = _dbContext.ReservaDetalle.Include(c => c.Reservas).Where(c => c.Reservas.Estatus == "Activo" &&
              (c.Reservas.FechaReservaInicia >= _fecha && c.Reservas.FechaReservaFinaliza <= _fecha) ||
              (c.Reservas.FechaReservaInicia >= _fecha2 && c.Reservas.FechaReservaFinaliza <= _fecha2)).Select(c => c.Id);
            Set("checkinlp", _fecha.ToString("dd/MM/yyyy"), 180);
            Set("checkoutlp", _fecha2.ToString("dd/MM/yyyy"), 180);
            Set("adultoslp", catedraticos.ToString(), 180);
            Set("ninolp", estudiantes.ToString(), 180);
            lstEventos = _dbContext.Eventos.Include(c => c.Lugar).Where(c => c.Activo == true && !idH.Contains(c.Id)).ToList();
            foreach (var item in lstEventos)
            {
                item.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.EventoId == item.Id && c.Activo == true).ToList();
            }

            if (!string.IsNullOrEmpty(fecha))
            { }


            return View(lstEventos);
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }
    }
}