namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuitarTiemposDelTipoTimeSql : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Receta", "TiempoCoccion");
            DropColumn("dbo.Receta", "TiempoPreparacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Receta", "TiempoPreparacion", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.Receta", "TiempoCoccion", c => c.Time(nullable: false, precision: 7));
        }
    }
}
