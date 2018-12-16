namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recetas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imagen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 256, unicode: false),
                        Alt = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 256, unicode: false),
                        Autor = c.String(maxLength: 64, unicode: false),
                        TiempoCoccion = c.Time(nullable: false, precision: 7),
                        FechaPublicacion = c.DateTime(nullable: false),
                        Descripcion = c.String(maxLength: 8000, unicode: false),
                        Keywords = c.String(maxLength: 256, unicode: false),
                        TiempoPreparacion = c.Time(nullable: false, precision: 7),
                        CategoriaReceta = c.String(maxLength: 64, unicode: false),
                        Raciones = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IngredienteReceta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ingrediente_Id = c.Int(nullable: false),
                        Receta_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingrediente", t => t.Ingrediente_Id, cascadeDelete: true)
                .ForeignKey("dbo.Receta", t => t.Receta_Id, cascadeDelete: true)
                .Index(t => t.Ingrediente_Id)
                .Index(t => t.Receta_Id);
            
            CreateTable(
                "dbo.Ingrediente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 512, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Instruccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 512, unicode: false),
                        Receta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Receta", t => t.Receta_Id)
                .Index(t => t.Receta_Id);
            
            CreateTable(
                "dbo.RecetasImagenes",
                c => new
                    {
                        Receta_Id = c.Int(nullable: false),
                        Imagen_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receta_Id, t.Imagen_Id })
                .ForeignKey("dbo.Receta", t => t.Receta_Id, cascadeDelete: true)
                .ForeignKey("dbo.Imagen", t => t.Imagen_Id, cascadeDelete: true)
                .Index(t => t.Receta_Id)
                .Index(t => t.Imagen_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Instruccion", "Receta_Id", "dbo.Receta");
            DropForeignKey("dbo.IngredienteReceta", "Receta_Id", "dbo.Receta");
            DropForeignKey("dbo.IngredienteReceta", "Ingrediente_Id", "dbo.Ingrediente");
            DropForeignKey("dbo.RecetasImagenes", "Imagen_Id", "dbo.Imagen");
            DropForeignKey("dbo.RecetasImagenes", "Receta_Id", "dbo.Receta");
            DropIndex("dbo.RecetasImagenes", new[] { "Imagen_Id" });
            DropIndex("dbo.RecetasImagenes", new[] { "Receta_Id" });
            DropIndex("dbo.Instruccion", new[] { "Receta_Id" });
            DropIndex("dbo.IngredienteReceta", new[] { "Receta_Id" });
            DropIndex("dbo.IngredienteReceta", new[] { "Ingrediente_Id" });
            DropTable("dbo.RecetasImagenes");
            DropTable("dbo.Instruccion");
            DropTable("dbo.Ingrediente");
            DropTable("dbo.IngredienteReceta");
            DropTable("dbo.Receta");
            DropTable("dbo.Imagen");
        }
    }
}
