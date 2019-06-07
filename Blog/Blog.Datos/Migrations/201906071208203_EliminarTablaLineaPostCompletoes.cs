namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarTablaLineaPostCompletoes : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LineaPostCompletoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LineaPostCompletoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrlSlug = c.String(),
                        Titulo = c.String(),
                        Subtitulo = c.String(),
                        FechaPost = c.DateTime(nullable: false),
                        Autor = c.String(),
                        ContenidoHtml = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
