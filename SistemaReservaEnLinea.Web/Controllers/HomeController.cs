using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaReservaEnLinea.Web.Models;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContextLPL _dbContext;
        private readonly IEmailSender _emailSender;


        public HomeController(ILogger<HomeController> logger, DataContextLPL dataContext, IEmailSender emailSender)
        {
            _logger = logger;
            _dbContext = dataContext;
            _emailSender = emailSender;
        }

        public IActionResult Index(string ReturnUrl = null)
        {
            ViewBag.Page = "Home";
            ViewData["ReturnUrl"] = ReturnUrl;
            var blog = _dbContext.Blog.Where( c=>c.Categoria != "configuracion").OrderByDescending(c => c.FechaModificacion).FirstOrDefault() ?? new Blog();
            var config = _dbContext.Configuracion.FirstOrDefault() ?? new Configuracion();
            ViewBag.Config = config;
            EventosTop(config);
            LugaresRemcomendados(config);
            BlogsMasRecientes();
            return View(blog);
        }

        public void BlogsMasRecientes()
        {
            ViewBag.BlogRecientes = _dbContext.Blog.Where(c => c.Activo == true && c.Categoria!="configuracion").OrderByDescending(c=>c.FechaModificacion).Take(5);
        }
        public void LugaresRemcomendados(Configuracion config)
        {
            if (config.Universidad == "Universidad")
            {
                List<SistemaReservaEnLinea.Models.Lugar> lugares = _dbContext.Lugar.Where(c => c.Estatus == true).OrderByDescending(c => c.Calificacion).Take(5).Include(c => c.Evento).ToList();
                foreach (var item in lugares)
                {
                    item.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Where(c => c.Activo == true && c.LugarId == item.Id).ToList();
                    foreach (var itemchild in item.ImagenesAsociadas)
                    {
                        itemchild.Imagenes = _dbContext.Imagenes.Where(c => c.Activo == true && c.Id == itemchild.ImagenId).FirstOrDefault();

                    }
                }
                ViewBag.LugaresRecomendados = lugares;
            }
            else
            {
                List<SistemaReservaEnLinea.Models.Eventos> eventos = _dbContext.Eventos.Where(c => c.Activo == true).OrderByDescending(c => c.Calificacion).Take(5).Include(c => c.ImagenesAsociadas).ToList();
                foreach (var item in eventos)
                {
                    foreach (var itemchild in item.ImagenesAsociadas)
                    {
                        itemchild.Imagenes = _dbContext.Imagenes.Where(c => c.Activo == true && c.Id == itemchild.ImagenId).FirstOrDefault();

                    }
                }
                ViewBag.EventosRecomendados = eventos;
            }
        }

        public void EventosTop(Configuracion config)
        {
            if (config.Universidad == "Universidad")
            {
                List<int> ids = _dbContext.Reservas.GroupBy(c => c.LugarId).Select(c => new { Id = c.Key, Count = c.Count() }).OrderByDescending(c => c.Count).Select(c => c.Id).Take(5).ToList();
                int idsSeleccionados = ids.Count();
                int idsFaltantes = 5 - idsSeleccionados;
                if (idsFaltantes > 0)
                {
                    ids.AddRange(_dbContext.Lugar.Where(c => c.Estatus == true && !ids.Contains(c.Id)).Select(c => c.Id).Take(idsFaltantes));
                }
                List<SistemaReservaEnLinea.Models.Lugar> lugares = _dbContext.Lugar.Where(c => c.Estatus == true && ids.Contains(c.Id)).Take(5).Include(c => c.Evento).ToList();
                foreach (var item in lugares)
                {
                    item.ImagenesAsociadas = _dbContext.ImagenesAsociadas.Where(c => c.Activo == true && c.LugarId == item.Id).ToList();
                    foreach (var itemchild in item.ImagenesAsociadas)
                    {
                        itemchild.Imagenes = _dbContext.Imagenes.Where(c => c.Activo == true && c.Id == itemchild.ImagenId).FirstOrDefault();

                    }
                }
                ViewBag.EventosTop = lugares;
            }
            else
            {
                List<int> ids = _dbContext.ReservaDetalle.GroupBy(c => c.EventoId).Select(c => new { Id = c.Key, Count = c.Count() }).OrderByDescending(c => c.Count).Select(c => c.Id).Take(5).ToList();
                int idsSeleccionados = ids.Count();
                int idsFaltantes = 5 - idsSeleccionados;
                if (idsFaltantes > 0)
                {
                    ids.AddRange(_dbContext.Eventos.Where(c => c.Activo == true && !ids.Contains(c.Id)).Select(c => c.Id).Take(idsFaltantes));
                }
                List<SistemaReservaEnLinea.Models.Eventos> eventos = _dbContext.Eventos.Where(c => c.Activo == true && ids.Contains(c.Id)).Take(5).Include(c => c.ImagenesAsociadas).ToList();
                foreach (var item in eventos)
                {
                    foreach (var itemchild in item.ImagenesAsociadas)
                    {
                        itemchild.Imagenes = _dbContext.Imagenes.Where(c => c.Activo == true && c.Id == itemchild.ImagenId).FirstOrDefault();

                    }
                }
                ViewBag.EventosTop = eventos;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Suscribirme(string txtEmail)
        {
            try
            {
                var objSuscripcion = _dbContext.Suscripciones.Where(c => c.Email == txtEmail).FirstOrDefault();
                if (objSuscripcion != null)
                {
                    objSuscripcion.Activo = true;
                    _dbContext.Update(objSuscripcion);
                }
                else
                {
                    _dbContext.Add(new Suscripciones
                    {
                        Activo = true,
                        Email = txtEmail
                    });
                }
                await _dbContext.SaveChangesAsync();
                return new JsonResult(new Response { IsSuccess = true, Message = "Se suscribió al boletín correctamente." });
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("hkarlos694@gmail.com", "Error  ", ex.Message);
                return new JsonResult(new Response { IsSuccess = false, Message = "No se pudo suscribir por el momento, intentelo más tarde." });
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
