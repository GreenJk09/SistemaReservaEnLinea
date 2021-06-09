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
    public class BlogController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly DataContextLPL _dbContext;
        public BlogController(IEmailSender emailSender, DataContextLPL dbContext)
        {
            _emailSender = emailSender;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.PaginaActual = "Blog";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = true, Nombre = "Blog", Url = Url.Content("~/Blog") });
            ViewBag.Paginas = p;
            return View();
        }

        public async Task<IActionResult> ListaArticulos()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            var resultado = _dbContext.Blog.Where(c => c.UsuarioId == usuario.Id);
            return PartialView(resultado);
        }
        public async Task<IActionResult> AddArticulo(int? id)
        {
            ViewBag.PaginaActual = "Blog";
            if (!HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
            ViewBag.Color = usuario.ColorTema == "w" ? Url.Content("~/admin/white") : Url.Content("~/admin/black");
            List<Pagina> p = new List<Pagina>();
            p.Add(new Pagina { Actual = false, Nombre = "Blog", Url = Url.Content("~/Blog") });
            p.Add(new Pagina { Actual = true, Nombre = "Agregar o modificar artículo", Url = Url.Content("~/Blog/AddArticulo") });
            ViewBag.Paginas = p;
            SistemaReservaEnLinea.Models.Blog blog = new SistemaReservaEnLinea.Models.Blog();
            if (id != null)
            {
                blog = _dbContext.Blog.Where(c => c.UsuarioId == usuario.Id && c.Id == id).FirstOrDefault();
                if (blog == null)
                    blog = new SistemaReservaEnLinea.Models.Blog();
            }
            return View(blog);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticulo(int? Id, string Titulo, string Descripcion, string Tags, string MetaKeys, string Categoria,
            string Video, IFormFile Imagen, string Post, string Tipo)
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                Blog blogdb = null;
                string ImagenPrincipal = "";
                if (Id != null)
                    blogdb = _dbContext.Blog.Where(c => c.UsuarioId == usuario.Id && c.Id == Id).FirstOrDefault();
                if (Imagen != null && Imagen.Length > 0)
                {
                    string strFile = Tools.Generics.NameFile() + "_ImgBlog_" + usuario.Id.ToString() + Path.GetExtension(Imagen.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "FileUpload", strFile);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await Imagen.CopyToAsync(stream);
                        ImagenPrincipal = strFile;
                    }
                }
                if (blogdb != null)
                {
                    blogdb.Activo = true;
                    blogdb.Categoria = Categoria;
                    blogdb.Descripcion = Descripcion;
                    blogdb.FechaModificacion = DateTime.Now;
                    blogdb.Imagen = string.IsNullOrEmpty( ImagenPrincipal)? blogdb.Imagen : ImagenPrincipal;
                    blogdb.MetaKeys = MetaKeys;
                    blogdb.Post = Post;
                    blogdb.Tags = Tags;
                    blogdb.Tipo = Tipo;
                    blogdb.Titulo = Titulo;
                    blogdb.UsuarioId = usuario.Id;
                    blogdb.Video = Video;
                    _dbContext.Update(blogdb);
                }
                else
                {
                    blogdb = new Blog
                    {
                        Activo = true,
                        Categoria = Categoria,
                        Descripcion = Descripcion,
                        FechaModificacion = DateTime.Now,
                        Imagen = ImagenPrincipal,
                        MetaKeys = MetaKeys,
                        Post = Post,
                        Tags = Tags,
                        Tipo = Tipo,
                        Titulo = Titulo,
                        UsuarioId = usuario.Id,
                        Video = Video,
                        Consultas = 0,
                        Fecha = DateTime.Now,
                        MeGusta=5,
                        Comentarios=0
                    };
                    _dbContext.Add(blogdb);
                }
                await _dbContext.SaveChangesAsync();
                return new JsonResult(new Response { IsSuccess = true, Message = "Se guardaron los datos correctamente", Id = blogdb.Id, Url=Url.Content("~/Blog"), Funcion= "guardadoyredirige" });
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error", ex.ToString());
                return new JsonResult(new Response { IsSuccess = false, Message = "No se pudieron guardar los datos, intentelo más tarde." });
            }

        }

        [HttpPost]
        public async Task<IActionResult> DesactivarActivar(int BlogId, string MovimientoActDesact)
        {
            try
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
                var usuario = await AutenticacionHelper.GetUsuario(HttpContext, _emailSender);
                Blog blogdb = _dbContext.Blog.Where(c => c.UsuarioId == usuario.Id && c.Id == BlogId).FirstOrDefault();
                if (blogdb != null)
                {
                    Boolean movimiento = MovimientoActDesact == "A" ? true : false;
                    blogdb.Activo = movimiento;
                    _dbContext.Update(blogdb);
                    await _dbContext.SaveChangesAsync();
                    string strmensaje = MovimientoActDesact == "A" ? "activo" : "desactivo";
                    return new JsonResult(new Response { IsSuccess = true, Message = "Se " + strmensaje + " el artículo correctamente", DivTabla = "#tblBlog", Url = @Url.Content("~/Blog/ListaArticulos") });
                }
                else
                {
                    return new JsonResult(new Response { IsSuccess = false, Message = "No se encuentra el artículo que quiere desactivar" });
                }


            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error", ex.ToString());
                return new JsonResult(new Response { IsSuccess = false, Message = "No se pudieron guardar los datos, intentelo más tarde." });
            }

        }

    }
}