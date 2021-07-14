namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConvMessCommentModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Match_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MatchModels", t => t.Match_Id)
                .Index(t => t.Match_Id);
            
            CreateTable(
                "dbo.ConversationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeftUser_Id = c.String(maxLength: 128),
                        RigthUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.LeftUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RigthUser_Id)
                .Index(t => t.LeftUser_Id)
                .Index(t => t.RigthUser_Id);
            
            CreateTable(
                "dbo.MessageModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Conversation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConversationModels", t => t.Conversation_Id)
                .Index(t => t.Conversation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageModels", "Conversation_Id", "dbo.ConversationModels");
            DropForeignKey("dbo.ConversationModels", "RigthUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConversationModels", "LeftUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CommentModels", "Match_Id", "dbo.MatchModels");
            DropIndex("dbo.MessageModels", new[] { "Conversation_Id" });
            DropIndex("dbo.ConversationModels", new[] { "RigthUser_Id" });
            DropIndex("dbo.ConversationModels", new[] { "LeftUser_Id" });
            DropIndex("dbo.CommentModels", new[] { "Match_Id" });
            DropTable("dbo.MessageModels");
            DropTable("dbo.ConversationModels");
            DropTable("dbo.CommentModels");
        }
    }
}
