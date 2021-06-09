using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaReservaEnLinea.ViewModels;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaReservaEnLinea.Tools;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class EventosController : Controller
    {
        private readonly DataContextLPL _dbContext;
        private readonly IEmailSender _emailSender;


        public EventosController(DataContextLPL dataContext, IEmailSender emailSender)
        {
            _dbContext = dataContext;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index(int? Id)
        {
            ViewBag.PaginaActual = "Eventos";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = true, Nombre = "Eventos", Url = Url.Content("~/Eventos") });
            ViewBag.Paginas = p;
            ViewBag.LugarId = Id;
            return View();
        }

        public async Task<IActionResult> ListaEventos(int? IdLugar)
        {
            ViewBag.LugarId = IdLugar;
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            if (IdLugar != null)
            {
                var habitaciones = _dbContext.Lugar.Where(c => c.Id == IdLugar && c.UsuarioId == usuario.Id).Include(c => c.Evento);
                return PartialView(habitaciones);
            }
            else
            {
                var habitaciones = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id).Include(c => c.Evento);
                return PartialView(habitaciones);
            }
        }
        public async Task<IActionResult> AddEvento(int? id)
        {
            ViewBag.PaginaActual = "Eventos";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            SistemaReservaEnLinea.Models.Eventos habitacion = new SistemaReservaEnLinea.Models.Eventos();
            var lugar = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id && c.Estatus==true);
            ViewBag.Lugares = lugar;
            if (id != null)
            {
                var ids = lugar.Select(c => c.Id);
                habitacion = _dbContext.Eventos.Where(c => ids.Contains(c.LugarId) && c.Id == id).FirstOrDefault();
                if (habitacion == null)
                    habitacion = new SistemaReservaEnLinea.Models.Eventos();
            }
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = false, Nombre = "Eventos", Url = Url.Content("~/Eventos") });
            p.Add(new Pagina { Actual = true, Nombre = "Agregar o modificar eventos", Url = Url.Content("~/Eventos/AddEvento") });
            ViewBag.Paginas = p;

            var instalaciones = _dbContext.Cracteristicas.ToList().OrderBy(c => c.Nombre);
            ViewBag.Instalaciones = instalaciones;
            ViewBag.InstalacionesDivs = instalaciones.Count() / 3;

            return PartialView(habitacion);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvento(SistemaReservaEnLinea.ViewModels.Eventos hab)
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                SistemaReservaEnLinea.Models.Lugar lugardb = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id && c.Id == hab.LugarId).Include(c => c.Evento).FirstOrDefault();
                SistemaReservaEnLinea.Models.Eventos evento = new SistemaReservaEnLinea.Models.Eventos();
                if (lugardb != null)
                {
                    evento = lugardb.Evento.Where(c => c.Id == hab.Id).FirstOrDefault();
                    if (evento != null)
                    {
                        evento.Catedraticos = hab.Catedraticos;
                        evento.Seccion1 = hab.Seccion1;
                        evento.Seccion2 = hab.Seccion2;
                        evento.Seccion3 = hab.Seccion3;
                        evento.Seccion4 = hab.Seccion4;
                        evento.Costo = hab.Costo;
                        evento.CostoAdicionalCatedraticos = hab.CostoAdicionalCatedraticos;
                        evento.CostoAdicionalEstudiantes = hab.CostoAdicionalEstudiantes;
                        evento.CostoAdicionalInvitados = hab.CostoAdicionalInvitados;
                        evento.Descripcion = hab.Descripcion ?? "";
                        evento.Invitados = hab.Invitados;
                        evento.MaximoCatedraticos = hab.MaximoCatedraticos;
                        evento.MaximoEstudiantes = hab.MaximoEstudiantes;
                        evento.MaximoInvitados = hab.MaximoInvitados;
                        evento.Invitados = hab.Invitados;
                        evento.Titulo = hab.Titulo;
                        evento.TotalEventos = hab.TotalEventos;
                        _dbContext.Update(evento);
                    }
                    else
                    {
                        evento = new SistemaReservaEnLinea.Models.Eventos
                        {
                            Catedraticos = hab.Catedraticos,
                            Seccion1 = hab.Seccion1,
                            Seccion2 = hab.Seccion2,
                            Seccion3 = hab.Seccion3,
                            Seccion4 = hab.Seccion4,
                            Costo = hab.Costo,
                            CostoAdicionalCatedraticos = hab.CostoAdicionalCatedraticos,
                            CostoAdicionalEstudiantes = hab.CostoAdicionalEstudiantes,
                            CostoAdicionalInvitados = hab.CostoAdicionalInvitados,
                            Descripcion = hab.Descripcion ?? "",
                            Estudiantes = hab.Estudiantes,
                            MaximoCatedraticos = hab.MaximoCatedraticos,
                            MaximoEstudiantes = hab.MaximoEstudiantes,
                            MaximoInvitados = hab.MaximoInvitados,
                            Invitados = hab.Invitados,
                            Titulo = hab.Titulo,
                            TotalEventos = hab.TotalEventos,
                            Activo = true,
                            Calificacion = 0,
                            LugarId = hab.LugarId,
                            PalabrasClave = ""
                        };
                        _dbContext.Add(evento);
                    }
                    await _dbContext.SaveChangesAsync();
                    if (evento.Id > 0 && hab.Caracteristica != null)
                    {
                        var cars = _dbContext.LugaresEventosCaracteristicas.Where(c => c.EventosId == evento.Id && !hab.Caracteristica.Contains(c.CaracteristicaId));
                        foreach (var item in cars)
                        {
                            item.Activo = false;
                            _dbContext.Update(item);
                        }
                        foreach (var item in hab.Caracteristica)
                        {
                            if (item > 0)
                            {
                                LugaresEventosCaracteristicas car = _dbContext.LugaresEventosCaracteristicas.Where(c => c.EventosId == evento.Id && c.CaracteristicaId == item).FirstOrDefault();
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
                                        EventosId = evento.Id
                                    };
                                    _dbContext.Add(car);
                                }
                            }
                        }
                        await _dbContext.SaveChangesAsync();
                    }
                    return new JsonResult(new Response { IsSuccess = true, Message = "Se guardaron los datos correctamente", Id = evento.Id });
                }
                return new JsonResult(new Response { IsSuccess = false, Message = "No se encontro el evento, intentelo más tarde." });
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error ", ex.ToString());
                return new JsonResult(new Response { IsSuccess = false, Message = "No se pudieron guardar los datos, intentelo más tarde." });
            }

        }

        public async Task<IActionResult> ImagenesEvento(int id)
        {
            ViewBag.PaginaActual = "Eventos";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            var imagenes = await _dbContext.ImagenesAsociadas.Where(c => c.EventoId == id && c.Activo == true).Include(c => c.Imagenes).OrderByDescending(c => c.Id).ToListAsync();
            return PartialView(imagenes);
        }

        [HttpPost]
        public async Task<IActionResult> DesactivarActivar(int HabitacionId, string MovimientoActDesact)
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                var hoteldb = _dbContext.Lugar.Where(c => c.UsuarioId == usuario.Id).Select(c=>c.Id);
                var habita = _dbContext.Eventos.Where(c => hoteldb.Contains(c.LugarId) && c.Id == HabitacionId).FirstOrDefault();
                if (habita != null)
                {
                    
                    Boolean movimiento = MovimientoActDesact == "A" ? true : false;
                    habita.Activo = movimiento;
                    _dbContext.Update(habita);
                    await _dbContext.SaveChangesAsync();
                    string strmensaje = MovimientoActDesact == "A" ? "activo" : "desactivo";
                    return new JsonResult(new Response { IsSuccess = true, Message = "Se " + strmensaje + " el evento correctamente", DivTabla = "#tblHabitaciones", Url = @Url.Content("~/Eventos/ListaEventos") });
                }
                else
                {
                    return new JsonResult(new Response { IsSuccess = false, Message = "No se encuentra el evento que quiere desactivar" });
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