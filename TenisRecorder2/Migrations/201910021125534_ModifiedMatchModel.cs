namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedMatchModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MatchModels", "ScoreLeft", c => c.Int(nullable: false));
            AddColumn("dbo.MatchModels", "ScoreRight", c => c.Int(nullable: false));
            DropColumn("dbo.MatchModels", "Result");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MatchModels", "Result", c => c.String(nullable: false));
            DropColumn("dbo.MatchModels", "ScoreRight");
            DropColumn("dbo.MatchModels", "ScoreLeft");
        }
    }
}
