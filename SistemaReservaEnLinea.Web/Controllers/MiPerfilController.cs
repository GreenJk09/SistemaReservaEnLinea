using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaReservaEnLinea.Web.Controllers
{
    [Authorize]
    public class MiPerfilController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly DataContextLPL _dbContext;
        public MiPerfilController(IEmailSender emailSender, DataContextLPL dbContext)
        {
            _emailSender = emailSender;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PaginaActual = "Mi cuenta";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            var usu = _dbContext.Usuarios.Where(c => c.Id == usuario.Id).FirstOrDefault() ?? new Usuarios();
            usu.Foto = string.IsNullOrEmpty(usu.Foto) ? "200x200.jpg" : usu.Foto;

            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = true, Nombre = "Mi cuenta", Url = Url.Content("~/MiPerfil") });
            ViewBag.Paginas = p;

            return View(usu);
        }

        public async Task<IActionResult> GuardarUsuario(IFormFile Foto, string Nombre, string Puesto, string Telefono, string Password)
        {
            Response response = new Response { IsSuccess = true, Message = "Se actualizó correctamente tu perfil." };
            try
            {
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                var usu = _dbContext.Usuarios.Where(c => c.Id == usuario.Id).FirstOrDefault();
                if (usu != null)
                {
                    usu.Nombre = Nombre;
                    usu.Puesto = Puesto;
                    usu.Telefono = Telefono;
                    usu.Password = string.IsNullOrEmpty(Password) ? usu.Password : Tools.SecurityManager.Encrypt(Password);
                    if (Foto!=null && Foto.Length > 0)
                    {
                        string strFile = Tools.Generics.NameFile() + "_Id_User_" + usu.Id.ToString() + Path.GetExtension(Foto.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FileUpload", strFile);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await Foto.CopyToAsync(stream);
                            usu.Foto = strFile;
                        }
                    }
                    _dbContext.Update(usu);
                    await _dbContext.SaveChangesAsync();
                    return new JsonResult(response);
                }                
            }
            catch(Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error ", ex.ToString());
            }
            response.IsSuccess = false;
            response.Message = "No se pudo actualizar tu perfil";
            return new JsonResult(response);
        }
    }
}