namespace Blog.Datos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InstruccionSinLongitud : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Instruccion", "Nombre", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Instruccion", "Nombre", c => c.String(maxLength: 512, unicode: false));
        }
    }
}
