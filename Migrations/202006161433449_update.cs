namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "Physicalyear", c => c.Int(nullable: false));
            AddColumn("dbo.Contacts", "Revenue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "Revenue");
            DropColumn("dbo.Contacts", "Physicalyear");
        }
    }
}
