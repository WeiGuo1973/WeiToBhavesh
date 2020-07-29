namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Status");
        }
    }
}
