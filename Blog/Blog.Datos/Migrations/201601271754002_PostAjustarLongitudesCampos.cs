namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAjustarLongitudesCampos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Subtitulo", c => c.String(maxLength: 400, unicode: false));
            AlterColumn("dbo.Post", "Titulo", c => c.String(nullable: false, maxLength: 128, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Titulo", c => c.String(nullable: false, maxLength: 64, unicode: false));
            AlterColumn("dbo.Post", "Subtitulo", c => c.String(maxLength: 128, unicode: false));
        }
    }
}
