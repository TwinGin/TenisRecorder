namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMatches : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MatchModels", "Right_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MatchModels", new[] { "Right_Id" });
            AlterColumn("dbo.MatchModels", "Result", c => c.String(nullable: false));
            AlterColumn("dbo.MatchModels", "Right_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.MatchModels", "Right_Id");
            AddForeignKey("dbo.MatchModels", "Right_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchModels", "Right_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MatchModels", new[] { "Right_Id" });
            AlterColumn("dbo.MatchModels", "Right_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.MatchModels", "Result", c => c.String());
            CreateIndex("dbo.MatchModels", "Right_Id");
            AddForeignKey("dbo.MatchModels", "Right_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
