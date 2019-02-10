namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TiempoCoccionTicks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Receta", "TiempoCoccionEnSegundos", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Receta", "TiempoPreparacionEnSegundos", c => c.Int(nullable: false , defaultValue: 0));
            
            Sql("UPDATE dbo.Receta SET TiempoCoccionEnSegundos = DATEPART(SECOND, TiempoCoccion) + 60 * DATEPART(MINUTE, TiempoCoccion) + 3600 * DATEPART(HOUR, TiempoCoccion)");
            Sql("UPDATE dbo.Receta SET TiempoPreparacionEnSegundos = DATEPART(SECOND, TiempoPreparacion) + 60 * DATEPART(MINUTE, TiempoPreparacion) + 3600 * DATEPART(HOUR, TiempoPreparacion)");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Receta", "TiempoPreparacionEnSegundos");
            DropColumn("dbo.Receta", "TiempoCoccionEnSegundos");
        }
    }
}
