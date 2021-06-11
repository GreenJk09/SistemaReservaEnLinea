using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SistemaReservaEnLinea.Models;
using SistemaReservaEnLinea.Tools;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly DataContextLPL _dbContext;
        public AccountController(ILogger<AccountController> logger, IEmailSender emailSender, DataContextLPL dbContext)
        {
            _logger = logger;
            _emailSender = emailSender;
            _dbContext = dbContext;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string ReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home",  new { ReturnUrl = ReturnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Usuario model, string ReturnUrl = null)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    string strPassword = SistemaReservaEnLinea.Tools.SecurityManager.Encrypt(model.Password);
                    var objUsr = _dbContext.Usuarios.Where(c => c.Email == model.Email && c.Password == strPassword && c.Estatus == true).FirstOrDefault();

                    if (objUsr == null)
                    {
                        return new JsonResult(new Response { IsSuccess = false, Message = "Usuario y/o contraseña son incorrectos" });
                    }
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, objUsr.Nombre),
                    new Claim(ClaimTypes.NameIdentifier, objUsr.Id.ToString()),
                    new Claim("Correo", objUsr.Email),
                    new Claim("Avatar", objUsr.Foto ?? "user-image.png"),
                    new Claim("Puesto", objUsr.Puesto.ToString()),
                    new Claim("Tema", objUsr.ColorTema),
                    new Claim("Recordar", model.Recordar.ToString())
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = model.Recordar
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);
                    _logger.LogInformation("User {Email} logged in at {Time}.", objUsr.Email, DateTime.UtcNow);
                    return new JsonResult(new Response { IsSuccess = true, Message = "Usuario / contraseña incorrectos", Url = Url.GetLocalUrl(ReturnUrl) });
                }
                catch(Exception ex)
                {
                    await _emailSender.SendEmailAsync(model.Email, "Error", ex.Message);
                }
               

            }
            return new JsonResult(new Response { IsSuccess = false, Message = "Usuario y/o contraseña incorrectos" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecuperarPass(Usuario model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var objUsr = _dbContext.Usuarios.Where(c => c.Email == model.Email).FirstOrDefault();

                    if (objUsr == null)
                    {
                        return new JsonResult(new Response { IsSuccess = false, Message = "No se encuentra registrado." });
                    }
                    string strMensaje = "";
                    string urlAddress = UrlHelper.GetPath(Request) + "Account/Index/?usuario=" + model.Email + "&pass=" + SistemaReservaEnLinea.Tools.SecurityManager.Decrypt(objUsr.Password);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        strMensaje = readStream.ReadToEnd();
                        response.Close();
                        readStream.Close();
                    }
                    await _emailSender.SendEmailAsync(model.Email, "Recuperación de cuenta", strMensaje);
                    return new JsonResult(new Response { IsSuccess = true, Message = "Se envío la información de su cuenta a su correo.", Url =""});
                }
                catch (Exception ex)
                {
                    await _emailSender.SendEmailAsync("hkarlos694@gmail.com", "Error", ex.Message);
                }


            }
            return new JsonResult(new Response { IsSuccess = false, Message = "Usuario y/o contraseña incorrectos" });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario model)
        {
            if (ModelState.IsValid)
            {
                string strPassword = SistemaReservaEnLinea.Tools.SecurityManager.Encrypt(model.Password);
                var objUsr = _dbContext.Usuarios.Where(c => c.Email == model.Email).FirstOrDefault();
                if (objUsr != null)
                {
                    return new JsonResult(new Response { IsSuccess = false, Message = "El usuario ya se encuentra registrado" });
                }
                var newUser = new Usuarios
                {
                    Email = model.Email,
                    Estatus = true,
                    Foto = "",
                    Nombre = model.Nombre,
                    Password = strPassword,
                    Puesto = "",
                    Telefono = "",
                    UsuarioIdPadre = null,
                    Fecha = DateTime.Now, 
                    ColorTema="w"
                };
                await _dbContext.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();
                if (newUser.Id > 0)
                {
                    string strMensaje = "";
                    string urlAddress = UrlHelper.GetPath(Request) + "Account/Index/?usuario="+ model.Email + "&pass="+ model.Password;

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();
                        StreamReader readStream = null;
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        strMensaje = readStream.ReadToEnd();
                        response.Close();
                        readStream.Close();
                    }
                    await _emailSender.SendEmailAsync(model.Email, "Bienvenido a Reserva en Línea", strMensaje);
                    await _emailSender.SendEmailAsync("karlosdh98@outlook.es", "Bienvenido a Reserva en Línea", strMensaje);
                    await _emailSender.SendEmailAsync("hkarlos694@gmail.com", "Bienvenido a Reserva en Línea", strMensaje);
                    return new JsonResult(new Response { IsSuccess = true, Message = "Se creo el usuario correctamente." , Url=""});
                }
            }
            return new JsonResult(new Response { IsSuccess = false, Message = "Usuario y/o contraseña incorrectos" });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User {Name} logged out at {Time}.",
                User.Identity.Name, DateTime.UtcNow);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult Index(string usuario, string pass)
        {
            ViewBag.Logo= UrlHelper.GetPath(Request) + "/images/footer-logo.png";
            ViewBag.Usuario = usuario;
            ViewBag.Password = pass;
            return View();
        }
    }
}