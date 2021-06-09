using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaReservaEnLinea.Web.Controllers
{
    [Authorize]
    public class LugaresController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly DataContextLPL _dbContext;
        public LugaresController(IEmailSender emailSender, DataContextLPL dbContext)
        {
            _emailSender = emailSender;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PaginaActual = "Lugares";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = true, Nombre = "Lugares", Url = Url.Content("~/Lugares") });
            ViewBag.Paginas = p;
            return View();
        }

        public async Task<IActionResult> ListaLugares()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            var lugares = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id);
            return PartialView(lugares);
        }

        public async Task<IActionResult> AddLugar(int? id)
        {
            ViewBag.PaginaActual = "Lugares";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            SistemaReservaEnLinea.Models.Lugar lugar = new SistemaReservaEnLinea.Models.Lugar();
            if (id != null)
            {
                lugar = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id && c.Id == id).FirstOrDefault();
                if (lugar == null)
                    lugar = new SistemaReservaEnLinea.Models.Lugar();
            }
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = false, Nombre = "Lugares de Eventos", Url = Url.Content("~/Lugares") });
            p.Add(new Pagina { Actual = true, Nombre = "Agregar o modificar Lugar", Url = Url.Content("~/Lugares/AddLugar") });
            ViewBag.Paginas = p;

            var instalaciones = _dbContext.Cracteristicas.ToList().OrderBy(c => c.Nombre);
            ViewBag.Instalaciones = instalaciones;
            ViewBag.InstalacionesDivs = instalaciones.Count() / 3;

            return PartialView(lugar);
        }

        public async Task<IActionResult> ImagenesLugar(int id)
        {
            ViewBag.PaginaActual = "Lugares";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            var imagenes = await _dbContext.ImagenesAsociadas.Where(c => c.LugarId == id && c.EventoId==null && c.Activo == true).Include(c => c.Imagenes).OrderByDescending(c=>c.Id).ToListAsync();
            return PartialView(imagenes);
        }

        [HttpPost]
        public async Task<IActionResult> AddLugar(ViewModels.Lugar lugar)
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                SistemaReservaEnLinea.Models.Lugar lugardb = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id && c.Id == lugar.Id).FirstOrDefault();
                if (lugardb != null)
                {
                    lugardb.Ciudad = lugar.Ciudad;
                    lugardb.CodigoPostal = lugar.CodigoPostal;
                    lugardb.Descripcion = lugar.Descripcion;
                    lugardb.Direccion = lugar.Direccion;
                    lugardb.Email = lugar.Email;
                    lugardb.Departamento = lugar.Departamento;
                    lugardb.Facebook = lugar.Facebook;
                    lugardb.Facturacion = lugar.Facturacion;
                    lugardb.Instagram = lugar.Instagram;
                    lugardb.Latitud = lugar.Latitud;
                    lugardb.LinkedIn = lugar.LinkedIn;
                    lugardb.Longitud = lugar.Longitud;
                    lugardb.Nombre = lugar.Nombre;
                    lugardb.PalabrasClave = lugar.PalabrasClave;
                    lugardb.Pinterest = lugar.Pinterest;
                    lugardb.Telefono = lugar.Telefono;
                    lugardb.Twitter = lugar.Twitter;
                    lugardb.Website = lugar.Website;
                    lugardb.WhatsApp = lugar.WhatsApp;
                    _dbContext.Update(lugardb);
                }
                else
                {
                    lugardb = new SistemaReservaEnLinea.Models.Lugar
                    {
                        Ciudad = lugar.Ciudad,
                        CodigoPostal = lugar.CodigoPostal,
                        Descripcion = lugar.Descripcion,
                        Direccion = lugar.Direccion,
                        Email = lugar.Email,
                        Departamento = lugar.Departamento,
                        Facebook = lugar.Facebook,
                        Facturacion = lugar.Facturacion,
                        Instagram = lugar.Instagram,
                        Latitud = lugar.Latitud,
                        LinkedIn = lugar.LinkedIn,
                        Longitud = lugar.Longitud,
                        Nombre = lugar.Nombre,
                        PalabrasClave = lugar.PalabrasClave,
                        Pinterest = lugar.Pinterest,
                        Telefono = lugar.Telefono,
                        Twitter = lugar.Twitter,
                        Website = lugar.Website,
                        WhatsApp = lugar.WhatsApp,
                        Calificacion = 5,
                        Estatus = true,
                        Fecha = DateTime.Now,
                        UsuarioId = usuario.Id
                    };
                    _dbContext.Add(lugardb);
                }
                await _dbContext.SaveChangesAsync();
                if (lugardb.Id > 0 && lugar.Caracteristica != null)
                {
                    var cars = _dbContext.LugaresEventosCaracteristicas.Where(c => c.LugarId == lugardb.Id && !lugar.Caracteristica.Contains(c.CaracteristicaId));
                    foreach (var item in cars)
                    {
                        item.Activo = false;
                        _dbContext.Update(item);
                    }
                    foreach (var item in lugar.Caracteristica)
                    {
                        if (item > 0)
                        {
                            LugaresEventosCaracteristicas car = _dbContext.LugaresEventosCaracteristicas.Where(c => c.LugarId == lugardb.Id && c.CaracteristicaId == item).FirstOrDefault();
                            if (car != null)
                            {
                                car.Activo = true;
                                _dbContext.Update(car);
                            }
                            else
                            {
                                car = new LugaresEventosCaracteristicas
                                {
                                    Activo = true,
                                    CaracteristicaId = item,
                                    LugarId = lugardb.Id
                                };
                                _dbContext.Add(car);
                            }
                        }
                    }
                    await _dbContext.SaveChangesAsync();
                }
                return new JsonResult(new Response { IsSuccess = true, Message = "Se guardaron los datos correctamente", Id = lugardb.Id });
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error ", ex.ToString());
                return new JsonResult(new Response { IsSuccess = false, Message = "No se pudieron guardar los datos, intentelo más tarde." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> DesactivarActivar(int LugarId, string MovimientoActDesact)
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                SistemaReservaEnLinea.Models.Lugar lugardb = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id && c.Id == LugarId).FirstOrDefault();
                if (lugardb != null)
                {
                    Boolean movimiento = MovimientoActDesact == "A" ? true : false;
                    lugardb.Estatus = movimiento;
                    _dbContext.Update(lugardb);
                    await _dbContext.SaveChangesAsync();
                    string strmensaje = MovimientoActDesact == "A" ? "activo" : "desactivo";
                    return new JsonResult(new Response { IsSuccess = true, Message = "Se " + strmensaje + " el lugar correctamente", DivTabla= "#tblHoteles", Url= @Url.Content("~/Lugares/ListaLugares") });
                }
                else
                {
                    return new JsonResult(new Response { IsSuccess = false, Message = "No se encuentra el escenario que quiere desactivar" });
                }               
                
                
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error ", ex.ToString());
                return new JsonResult(new Response { IsSuccess = false, Message = "No se pudieron guardar los datos, intentelo más tarde." });
            }

        }
    }
}