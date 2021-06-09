using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SistemaReservaEnLinea.Web.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index(int Id)
        {
            return View();
        }
    }
}