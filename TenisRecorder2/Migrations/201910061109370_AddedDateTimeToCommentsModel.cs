namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateTimeToCommentsModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentModels", "Date");
        }
    }
}
