using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class ConfiguracionController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly DataContextLPL _dbContext;
        public ConfiguracionController(IEmailSender emailSender, DataContextLPL dbContext)
        {
            _emailSender = emailSender;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PaginaActual = "Configuración";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = true, Nombre = "Configuración", Url = Url.Content("~/Configuracion") });
            ViewBag.Paginas = p;

            var config = _dbContext.Configuracion.FirstOrDefault() ?? new SistemaReservaEnLinea.Models.Configuracion();
            config.LogoPrincipal = string.IsNullOrEmpty(config.LogoPrincipal) ? "200x200.jpg" : config.LogoPrincipal;
            config.LogoFooter = string.IsNullOrEmpty(config.LogoFooter) ? "200x200.jpg" : config.LogoFooter;
            return View(config);
        }

        public async Task<IActionResult> GuardarInformacion(IFormFile LogoPrincipal, IFormFile LogoFooter, string Direccion, string AgenciaHotel,
            string Telefono, string Twitter, string Facebook, string PaginaWeb, string Instagram, string Linkedin)
        {
            Response response = new Response { IsSuccess = true, Message = "Se actualizó correctamente la configuración." };
            try
            {
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                string logoPrincipal = "";
                string logoFootoer = "";
                if (LogoPrincipal != null && LogoPrincipal.Length > 0)
                {
                    string strFile = SistemaReservaEnLinea.Tools.Generics.NameFile() + "_Id_Logo_" + usuario.Id.ToString() + Path.GetExtension(LogoPrincipal.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FileUpload", strFile);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await LogoPrincipal.CopyToAsync(stream);
                        logoPrincipal = strFile;
                    }
                }
                if (LogoFooter != null && LogoFooter.Length > 0)
                {
                    string strFile = SistemaReservaEnLinea.Tools.Generics.NameFile() + "_Id_Logo_" + usuario.Id.ToString() + Path.GetExtension(LogoFooter.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FileUpload", strFile);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await LogoFooter.CopyToAsync(stream);
                        logoFootoer = strFile;
                    }
                }
                Configuracion config = _dbContext.Configuracion.FirstOrDefault();
                if (config != null)
                {
                    config.Universidad = AgenciaHotel;
                    config.Direccion = Direccion;
                    config.Facebook = Facebook;
                    config.Linkedin = Linkedin;
                    config.PaginaWeb = PaginaWeb;
                    config.Telefono = Telefono;
                    config.Instagram = Instagram;
                    config.Twitter = Twitter;
                    config.LogoPrincipal = string.IsNullOrEmpty(logoPrincipal) ? config.LogoPrincipal : logoPrincipal;
                    config.LogoFooter = string.IsNullOrEmpty(logoFootoer) ? config.LogoFooter : logoFootoer;


                    _dbContext.Update(config);
                    await _dbContext.SaveChangesAsync();
                    return new JsonResult(response);
                }
                else
                {
                    config = new Configuracion
                    {
                        Universidad = AgenciaHotel,
                        Direccion = Direccion,
                        Facebook = Facebook,
                        Linkedin = Linkedin,
                        PaginaWeb = PaginaWeb,
                        Telefono = Telefono,
                        Instagram = Instagram,
                        Twitter = Twitter,
                        LogoPrincipal = logoPrincipal,
                        LogoFooter = logoFootoer
                    };
                    _dbContext.Add(config);
                    await _dbContext.SaveChangesAsync();
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error ", ex.ToString());
            }
            response.IsSuccess = false;
            response.Message = "No se pudo actualizar la configuración";
            return new JsonResult(response);
        }
    }
}