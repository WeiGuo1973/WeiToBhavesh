namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _enum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "Revenue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "Revenue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
