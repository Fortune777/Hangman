namespace APerepechko.HangMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemesDbs",
                c => new
                    {
                        ThemeId = c.Int(nullable: false, identity: true),
                        Theme = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.ThemeId);
            
            CreateTable(
                "dbo.WordsDbs",
                c => new
                    {
                        WordId = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false, maxLength: 20),
                        ThemeId = c.Int(),
                    })
                .PrimaryKey(t => t.WordId)
                .ForeignKey("dbo.ThemesDbs", t => t.ThemeId)
                .Index(t => t.ThemeId);
            
            CreateTable(
                "dbo.UserDbs",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Avatar = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.Int(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserStatisticsDbs",
                c => new
                    {
                        StatisticsId = c.Int(nullable: false),
                        WinCount = c.Int(nullable: false),
                        LossCount = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatisticsId)
                .ForeignKey("dbo.UserDbs", t => t.StatisticsId)
                .Index(t => t.StatisticsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserStatisticsDbs", "StatisticsId", "dbo.UserDbs");
            DropForeignKey("dbo.WordsDbs", "ThemeId", "dbo.ThemesDbs");
            DropIndex("dbo.UserStatisticsDbs", new[] { "StatisticsId" });
            DropIndex("dbo.WordsDbs", new[] { "ThemeId" });
            DropTable("dbo.UserStatisticsDbs");
            DropTable("dbo.UserDbs");
            DropTable("dbo.WordsDbs");
            DropTable("dbo.ThemesDbs");
        }
    }
}
