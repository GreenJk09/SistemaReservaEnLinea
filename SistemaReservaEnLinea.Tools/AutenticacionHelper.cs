using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SistemaReservaEnLinea.Tools.Services;
using SistemaReservaEnLinea.ViewModels;

namespace SistemaReservaEnLinea.Tools
{
    public class AutenticacionHelper
    {
        public static async Task<Usuario> GetUsuario(HttpContext currentUser, IEmailSender _emailSender)
        {
            Usuario usuario = new Usuario();
            int UsuarioId = 0;
            try
            {
                int.TryParse(currentUser.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value, out UsuarioId);
                usuario.Id = UsuarioId;
                usuario.Nombre = currentUser.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                usuario.Email= currentUser.User.Claims.FirstOrDefault(c => c.Type == "Correo").Value;
                usuario.Foto = currentUser.User.Claims.FirstOrDefault(c => c.Type == "Avatar").Value;
                usuario.Puesto = currentUser.User.Claims.FirstOrDefault(c => c.Type == "Puesto").Value;
                usuario.ColorTema = currentUser.User.Claims.FirstOrDefault(c => c.Type == "Tema").Value;
                bool recordar = false;
                bool.TryParse(currentUser.User.Claims.FirstOrDefault(c => c.Type == "Recordar").Value, out recordar);
                usuario.Recordar = recordar;
            }
            catch(Exception ex)
            {
              await  _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error", "GetUsuario: " + ex.Message);
            }
            return usuario;
        }

        public static async Task<List<Claim>> UpdateClaims(HttpContext currentUser, IEmailSender _emailSender, Usuario usuario)
        {
            List<Claim> claims = new List<Claim>();
            try
            {
                claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Nombre),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim("Correo", usuario.Email),
                    new Claim("Avatar", usuario.Foto ?? "user-image.png"),
                    new Claim("Puesto", usuario.Puesto.ToString()),
                    new Claim("Tema", usuario.ColorTema),
                    new Claim("Recordar", usuario.Recordar.ToString())
                };
            }
            catch (Exception ex)
            {
                await _emailSender.SendEmailAsync("carlos.hernandez191137@liveusam.edu.sv", "Error", "GetUsuario: " + ex.Message);
            }
            return claims;
        }
    }
}
