using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Ac.Datos.MapeosTablas;
using Ac.Modelo;
using Ac.Modelo.Tags;
using Ac.Modelo.Usuarios;
using Infra;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ac.Datos
{
    public class ContextoBaseDatos : IdentityDbContext<Usuario>
    {
        public ContextoBaseDatos()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static ContextoBaseDatos Create()
        {
            return new ContextoBaseDatos();
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Post> Posts { get; set; }


        
        public async Task GuardarCambios()
        {
            try
            {
                await SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContextoBaseDatos, Migrations.Configuration>());

            modelBuilder.HasDefaultSchema("acapdevila");

            modelBuilder.ComplexType<Imagen>();
            
            modelBuilder.Configurations.Add(new MapeoTag());
            
            modelBuilder.Configurations.Add(new MapeoPost());


            base.OnModelCreating(modelBuilder);
        }

      
    }
}