using System.Data.Entity;
using Blog.Datos.MapeosTablas;
using Blog.Modelo.Categorias;
using Blog.Modelo.Posts;
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

        public static ContextoBaseDatos Create()
        {
            return new ContextoBaseDatos();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContextoBaseDatos, Migrations.Configuration>());

            modelBuilder.Configurations.Add(new MapeoTag());

            modelBuilder.Configurations.Add(new MapeoCategoria());

            modelBuilder.Configurations.Add(new MapeoPost());

            modelBuilder.Configurations.Add(new MapeoBlogEntidad());

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Blog.Modelo.Posts.LineaPostCompleto> LineaPostCompletoes { get; set; }
    }
}