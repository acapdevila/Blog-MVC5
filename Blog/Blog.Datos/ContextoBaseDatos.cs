using System.Data.Entity;
using Blog.Datos.MapeosTablas;
using Blog.Modelo;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Blog.Datos
{
    public class ContextoBaseDatos : IdentityDbContext<Usuario>
    {
        public ContextoBaseDatos()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Tag> Tags { get; set; }

        public static ContextoBaseDatos Create()
        {
            return new ContextoBaseDatos();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MapeoTag());

            modelBuilder.Configurations.Add(new MapeoPost());

            base.OnModelCreating(modelBuilder);
        }
    }
}