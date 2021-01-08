namespace FrontVentasUsuarioFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregoColumnasPendientes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NumeroUsuario", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NumeroColaborador", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "NumeroColaborador");
            DropColumn("dbo.AspNetUsers", "NumeroUsuario");
        }
    }
}
