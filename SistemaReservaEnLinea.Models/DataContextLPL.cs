using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReservaEnLinea.Models
{
    public class DataContextLPL: DbContext
    {
        public DataContextLPL(DbContextOptions<DataContextLPL> options): base(options)
        {

        }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Cracteristicas> Cracteristicas { get; set; }
        public DbSet<Eventos> Eventos { get; set; }
        public DbSet<Lugar> Lugar { get; set; }
        public DbSet<LugaresEventosCaracteristicas> LugaresEventosCaracteristicas { get; set; }
        public DbSet<Imagenes> Imagenes { get; set; }
        public DbSet<ReservaDetalle> ReservaDetalle { get; set; }
        public DbSet<Reservas> Reservas { get; set; }
        public DbSet<ServiciosExtras> ServiciosExtras { get; set; }
        public DbSet<Suscripciones> Suscripciones { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<ImagenesAsociadas> ImagenesAsociadas { get; set; }
        public DbSet<Configuracion> Configuracion { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
    }
}
