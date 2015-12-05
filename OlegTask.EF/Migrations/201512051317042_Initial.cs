namespace OlegTask.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "OlegTask.Car",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 32),
                        Brand = c.String(nullable: false, maxLength: 64),
                        Model = c.String(nullable: false, maxLength: 64),
                        Color = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "OlegTask.Driver",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 128),
                        PassportNumber = c.Int(nullable: false),
                        BirthDay = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.PassportNumber, unique: true, name: "QIX_PassportNumber");
            
            CreateTable(
                "OlegTask.Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Single(nullable: false),
                        DriverId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("OlegTask.Driver", t => t.DriverId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "OlegTask.DriversCars",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.CarId })
                .ForeignKey("OlegTask.Driver", t => t.DriverId)
                .ForeignKey("OlegTask.Car", t => t.CarId)
                .Index(t => t.DriverId)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("OlegTask.Rating", "DriverId", "OlegTask.Driver");
            DropForeignKey("OlegTask.DriversCars", "CarId", "OlegTask.Car");
            DropForeignKey("OlegTask.DriversCars", "DriverId", "OlegTask.Driver");
            DropIndex("OlegTask.DriversCars", new[] { "CarId" });
            DropIndex("OlegTask.DriversCars", new[] { "DriverId" });
            DropIndex("OlegTask.Rating", new[] { "DriverId" });
            DropIndex("OlegTask.Driver", "QIX_PassportNumber");
            DropTable("OlegTask.DriversCars");
            DropTable("OlegTask.Rating");
            DropTable("OlegTask.Driver");
            DropTable("OlegTask.Car");
        }
    }
}
