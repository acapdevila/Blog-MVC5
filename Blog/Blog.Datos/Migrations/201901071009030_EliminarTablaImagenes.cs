namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EliminarTablaImagenes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecetasImagenes", "Receta_Id", "dbo.Receta");
            DropForeignKey("dbo.RecetasImagenes", "Imagen_Id", "dbo.Imagen");
            DropIndex("dbo.RecetasImagenes", new[] { "Receta_Id" });
            DropIndex("dbo.RecetasImagenes", new[] { "Imagen_Id" });
            AddColumn("dbo.Receta", "Imagen_Url", c => c.String());
            AddColumn("dbo.Receta", "Imagen_Alt", c => c.String());
            DropTable("dbo.Imagen");
            DropTable("dbo.RecetasImagenes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecetasImagenes",
                c => new
                    {
                        Receta_Id = c.Int(nullable: false),
                        Imagen_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Receta_Id, t.Imagen_Id });
            
            CreateTable(
                "dbo.Imagen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(maxLength: 256, unicode: false),
                        Alt = c.String(maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Receta", "Imagen_Alt");
            DropColumn("dbo.Receta", "Imagen_Url");
            CreateIndex("dbo.RecetasImagenes", "Imagen_Id");
            CreateIndex("dbo.RecetasImagenes", "Receta_Id");
            AddForeignKey("dbo.RecetasImagenes", "Imagen_Id", "dbo.Imagen", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecetasImagenes", "Receta_Id", "dbo.Receta", "Id", cascadeDelete: true);
        }
    }
}
