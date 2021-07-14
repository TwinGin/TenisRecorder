namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedFriendModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendsModels", "Paired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FriendsModels", "Paired");
        }
    }
}
