namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Utensilios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UtensilioCategorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 256, unicode: false),
                        UrlSlug = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Nombre, unique: true, name: "IX_NombreCategoriaUtensilio")
                .Index(t => t.UrlSlug, unique: true, name: "IX_UrlSlug_DeCategoriaUtensilio");
            
            CreateTable(
                "dbo.Utensilio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 256, unicode: false),
                        Imagen_Url = c.String(),
                        Imagen_Alt = c.String(),
                        Link = c.String(nullable: false, maxLength: 256, unicode: false),
                        Categoria_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UtensilioCategorias", t => t.Categoria_Id, cascadeDelete: true)
                .Index(t => t.Nombre, unique: true, name: "IX_NombreUtensilio")
                .Index(t => t.Categoria_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Utensilio", "Categoria_Id", "dbo.UtensilioCategorias");
            DropIndex("dbo.Utensilio", new[] { "Categoria_Id" });
            DropIndex("dbo.Utensilio", "IX_NombreUtensilio");
            DropIndex("dbo.UtensilioCategorias", "IX_UrlSlug_DeCategoriaUtensilio");
            DropIndex("dbo.UtensilioCategorias", "IX_NombreCategoriaUtensilio");
            DropTable("dbo.Utensilio");
            DropTable("dbo.UtensilioCategorias");
        }
    }
}
