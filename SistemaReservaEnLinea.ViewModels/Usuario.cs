using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReservaEnLinea.ViewModels
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Foto { get; set; }
        public string Puesto { get; set; }
        public bool Recordar { get; set; }
        public string ColorTema { get; set; }
    }
}
