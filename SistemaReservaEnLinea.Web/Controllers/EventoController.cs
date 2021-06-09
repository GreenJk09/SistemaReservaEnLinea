using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class EventoController : Controller
    {
        private readonly DataContextLPL _dbContext;
        private readonly IEmailSender _emailSender;

        public EventoController(DataContextLPL dataContext, IEmailSender emailSender)
        {
            _dbContext = dataContext;
            _emailSender = emailSender;
        }
        public IActionResult Index(int? Id)
        {
            ViewBag.Page = "Evento";
            var evento = _dbContext.Eventos.Include(c=>c.Lugar).Where(c => c.Id == Id && c.Activo == true).FirstOrDefault();
            if (evento == null)
                return RedirectToAction("Index", "Home");
            ViewBag.Url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Evento/?Id=" + Id.ToString();
            evento.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.Activo == true && c.EventoId == evento.Id).ToList();
            evento.Comentarios = _dbContext.Comentarios.Where(c => c.Activo == true && c.EventoId == evento.Id).ToList();
            var config = _dbContext.Configuracion.FirstOrDefault() ?? new Configuracion();
            ViewBag.Config = config;
            evento.Lugar.ImagenesAsociadas= _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.LugarId == evento.Lugar.Id && c.Activo == true).ToList();
            
            return View(evento);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarComentaio(string Nombre, string email, string mensaje, int? PadreId, int HabitacionId)
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mensaje))
                return new JsonResult(new Response { IsSuccess = false, Message = "Todos los campos son requeridos" });
            var coment = new Comentarios
            {
                Activo = true,
                Comentario = mensaje,
                Email = email,
                Fecha = DateTime.Now,
                EventoId = HabitacionId,
                Nombre = Nombre,
                ParentId = PadreId
            };
            await _dbContext.AddAsync(coment);
            await _dbContext.SaveChangesAsync();
            if (coment.Id > 0)
            {
                string strDivId = PadreId == null ? "#blogcomentarios" : "#comentario" + PadreId.ToString();
                return new JsonResult(new Response
                {
                    IsSuccess = true,
                    Message = "Se envió el comentario correctamente",
                    Id = coment.Id,
                    Funcion = "cargacomentario",
                    Html = SistemaReservaEnLinea.Tools.Comentarios.GetComentario(coment.Id, "Lugar", coment, Url.Content("~/images/blog-comment-2.jpg")),
                    DivTabla = strDivId
                });
            }
            return new JsonResult(new Response { IsSuccess = false, Message = "No se pudo enviar el comentario, intentelo más tarde." });
        }
    }
}