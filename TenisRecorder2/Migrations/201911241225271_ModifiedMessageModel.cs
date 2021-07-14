namespace TenisRecorder2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedMessageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageModels", "WhoSent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageModels", "WhoSent");
        }
    }
}
