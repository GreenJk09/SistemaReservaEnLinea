using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaReservaEnLinea.Web.Controllers
{
    [Authorize]
    public class PanelController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly DataContextLPL _dbContext;
        public PanelController(IEmailSender emailSender, DataContextLPL dbContext)
        {
            _emailSender = emailSender;
            _dbContext=dbContext;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PaginaActual = "Inicio";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);

            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeTheme(string Tema)
        {
           Response response = new Response { IsSuccess = true, Message = "Se cambió tu panel de contol correctamente." };
            try
            {
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                usuario.ColorTema = Tema;
                var usu = _dbContext.Usuarios.Where(c => c.Id == usuario.Id).FirstOrDefault();
                if (usu != null)
                {
                    usu.ColorTema = Tema;
                    _dbContext.Update(usu);
                    await _dbContext.SaveChangesAsync();
                    var claimsIdentity = new ClaimsIdentity(await AutenticacionHelper.UpdateClaims(HttpContext, _emailSender, usuario),
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = usuario.Recordar
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
                    response.Funcion = "cambio de tema";
                }
            }
            catch(Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error ", ex.Message);
                response.IsSuccess = false;
                response.Message = "No se pudo actualizar en el servidor la información solicitada.";
            }
            return new JsonResult(response);
        }
    }
}