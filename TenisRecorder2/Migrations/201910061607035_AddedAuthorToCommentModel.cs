namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAuthorToCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "Author_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.CommentModels", "Author_Id");
            AddForeignKey("dbo.CommentModels", "Author_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentModels", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "Author_Id" });
            DropColumn("dbo.CommentModels", "Author_Id");
        }
    }
}
