using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Blog.Datos.MapeosTablas;
using Blog.Modelo.Categorias;
using Blog.Modelo.Imagenes;
using Blog.Modelo.Posts;
using Blog.Modelo.Recetas;
using Blog.Modelo.Tags;
using Blog.Modelo.Usuarios;
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
        public DbSet<Post> Posts { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<BlogEntidad> Blogs { get; set; }

   
        public DbSet<Ingrediente> Ingredientes { get; set; }

      

        public DbSet<Imagen> Imagenes { get; set; }

        public DbSet<Receta> Recetas { get; set; }

        public DbSet<Instruccion> InstruccionesDeRecetas { get; set; }


        public static ContextoBaseDatos Create()
        {
            return new ContextoBaseDatos();
        }

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

            modelBuilder.Configurations.Add(new MapeoTag());

            modelBuilder.Configurations.Add(new MapeoCategoria());

            modelBuilder.Configurations.Add(new MapeoPost());

            modelBuilder.Configurations.Add(new MapeoBlogEntidad());


            modelBuilder.Configurations.Add(new MapeoIngrediente());

            modelBuilder.Configurations.Add(new MapeoImagen());

            modelBuilder.Configurations.Add(new MapeoInstruccion());

            modelBuilder.Configurations.Add(new MapeoReceta());


            modelBuilder.Configurations.Add(new MapeoIngredienteReceta());


            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Blog.Modelo.Posts.LineaPostCompleto> LineaPostCompletoes { get; set; }
    }
}