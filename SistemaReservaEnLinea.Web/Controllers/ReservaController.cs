using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaEnLinea.ViewModels;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class ReservaController : Controller
    {
        private readonly DataContextLPL _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReservaController(DataContextLPL dataContext, IEmailSender emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dataContext;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index(int Id)
        {
            ViewBag.Page = "Lugar";
            var evento = _dbContext.Eventos.Include(c => c.Lugar).Where(c => c.Id == Id && c.Activo == true).FirstOrDefault();
            if (evento == null)
                return RedirectToAction("Index", "Home");
            ViewBag.Url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Evento/?Id=" + Id.ToString();
            evento.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.Activo == true && c.LugarId == evento.Id).ToList();
            evento.Comentarios = _dbContext.Comentarios.Where(c => c.Activo == true && c.EventoId == evento.Id).ToList();
            var config = _dbContext.Configuracion.FirstOrDefault() ?? new Configuracion();
            ViewBag.Config = config;
            evento.Lugar.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.LugarId == evento.Lugar.Id && c.Activo == true).ToList();
           
            
            
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];
            //read cookie from Request object  
            string checkinlp = Request.Cookies["checkinlp"];
            string checkoutlp = Request.Cookies["checkoutlp"];
            string catedraticoslp = Request.Cookies["catedraticoslp"];
            string estudianteslp = Request.Cookies["estudianteslp"];

            ViewBag.CheckIn = checkinlp;
            ViewBag.CheckOut = checkoutlp;
            ViewBag.Cantidad = (Int32.Parse(catedraticoslp) + Int32.Parse(estudianteslp)).ToString();
            
            DateTime fecha1 = DateTime.Now;
            DateTime fecha2 = DateTime.Now;

            DateTime.TryParse(checkinlp, out fecha1);
            DateTime.TryParse(checkoutlp, out fecha2);

            ViewBag.TotalReserva = (fecha2 - fecha1).TotalDays;
            ViewBag.TotalEvento = String.Format(CultureInfo.InvariantCulture,
                                 "{0:0,0}", (evento.Costo * decimal.Parse(((fecha2 - fecha1).TotalDays).ToString())));

            return View(evento);
        }

        
    }
}