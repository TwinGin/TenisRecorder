namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatchModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Result = c.String(),
                        Left_Id = c.String(maxLength: 128),
                        Right_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Left_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Right_Id)
                .Index(t => t.Left_Id)
                .Index(t => t.Right_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchModels", "Right_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.MatchModels", "Left_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MatchModels", new[] { "Right_Id" });
            DropIndex("dbo.MatchModels", new[] { "Left_Id" });
            DropTable("dbo.MatchModels");
        }
    }
}
