namespace ProjectEJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apuestas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Usuario_Id = c.String(),
                        Numero = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        Sorteos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sorteos", t => t.Sorteos_Id)
                .Index(t => t.Sorteos_Id);
            
            CreateTable(
                "dbo.Sorteos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fecha_Expiracion = c.DateTime(nullable: false),
                        Descripcion = c.String(),
                        Is_Active = c.Boolean(nullable: false),
                        Is_Finished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cajas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ganadores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimerNumero = c.Int(nullable: false),
                        SegundoNumero = c.Int(nullable: false),
                        TercerNumero = c.Int(nullable: false),
                        Sorteos_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sorteos", t => t.Sorteos_Id)
                .Index(t => t.Sorteos_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ganadores", "Sorteos_Id", "dbo.Sorteos");
            DropForeignKey("dbo.Apuestas", "Sorteos_Id", "dbo.Sorteos");
            DropIndex("dbo.Ganadores", new[] { "Sorteos_Id" });
            DropIndex("dbo.Apuestas", new[] { "Sorteos_Id" });
            DropTable("dbo.Ganadores");
            DropTable("dbo.Cajas");
            DropTable("dbo.Sorteos");
            DropTable("dbo.Apuestas");
        }
    }
}
