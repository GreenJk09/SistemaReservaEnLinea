using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class LugarController : Controller
    {
        private readonly DataContextLPL _dbContext;
        private readonly IEmailSender _emailSender;

        public LugarController( DataContextLPL dataContext, IEmailSender emailSender)
        {
            _dbContext = dataContext;
            _emailSender = emailSender;
        }


        public IActionResult Index(int? Id)
        {
            ViewBag.Page = "Lugar";
            var lugar = _dbContext.Lugar.Where(c => c.Id == Id && c.Estatus == true).FirstOrDefault();
            if (lugar == null)
                return RedirectToAction("Index", "Home");
            ViewBag.Url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/Lugar/?Id=" + Id.ToString();
            lugar.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.Activo == true && c.LugarId == lugar.Id && c.EventoId == null).ToList();
            lugar.Evento = _dbContext.Eventos.Where(C => C.Activo == true && C.LugarId == lugar.Id).ToList();
            lugar.Comentarios = _dbContext.Comentarios.Where(c => c.Activo == true && c.LugarId == lugar.Id).ToList();
            var config = _dbContext.Configuracion.FirstOrDefault() ?? new Configuracion();
            ViewBag.Config = config;
            foreach (var item in lugar.Evento)
            {
                item.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Include(c => c.Imagenes).Where(c => c.EventoId == item.Id && c.Activo == true).ToList();
            }



            return View(lugar);
        }

        [HttpPost]
        public async Task<IActionResult> EnviarComentaio(string Nombre, string email, string mensaje, int? PadreId, int HotelId)
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mensaje))
                return new JsonResult(new Response { IsSuccess = false, Message = "Todos los campos son requeridos" });
            var coment = new Comentarios
            {
                Activo = true,
                Comentario = mensaje,
                Email = email,
                Fecha = DateTime.Now,
                LugarId = HotelId,
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
                    Message = "Se reservo correctamente",
                    Id = coment.Id,
                    Funcion = "cargacomentario",
                    Html = Tools.Comentarios.GetComentario(coment.Id, "Hotel", coment, Url.Content("~/images/blog-comment-2.jpg")),
                    DivTabla = strDivId
                });
            }
            return new JsonResult(new Response { IsSuccess = false, Message = "No se pudo enviar el comentario, intentelo más tarde." });
        }


    }
}