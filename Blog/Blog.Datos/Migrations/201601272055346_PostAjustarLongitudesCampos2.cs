namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostAjustarLongitudesCampos2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Subtitulo", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Subtitulo", c => c.String(maxLength: 400, unicode: false));
        }
    }
}
