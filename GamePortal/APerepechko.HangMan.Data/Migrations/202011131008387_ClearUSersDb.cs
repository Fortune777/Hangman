namespace APerepechko.HangMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearUSersDb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserStatisticsDbs", "StatisticsId", "dbo.UserDbs");
            DropIndex("dbo.UserStatisticsDbs", new[] { "StatisticsId" });
            DropPrimaryKey("dbo.UserStatisticsDbs");
            AlterColumn("dbo.UserStatisticsDbs", "StatisticsId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserStatisticsDbs", "StatisticsId");
            DropTable("dbo.UserDbs");
        }
        
        public override void Down()
        {
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
            
            DropPrimaryKey("dbo.UserStatisticsDbs");
            AlterColumn("dbo.UserStatisticsDbs", "StatisticsId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserStatisticsDbs", "StatisticsId");
            CreateIndex("dbo.UserStatisticsDbs", "StatisticsId");
            AddForeignKey("dbo.UserStatisticsDbs", "StatisticsId", "dbo.UserDbs", "UserId");
        }
    }
}
