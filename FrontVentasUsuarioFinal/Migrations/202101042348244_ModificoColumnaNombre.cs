namespace FrontVentasUsuarioFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificoColumnaNombre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NombreColaborador", c => c.String(maxLength: 100));
            DropColumn("dbo.AspNetUsers", "NumeroColaborador");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "NumeroColaborador", c => c.String(maxLength: 10));
            DropColumn("dbo.AspNetUsers", "NombreColaborador");
        }
    }
}
