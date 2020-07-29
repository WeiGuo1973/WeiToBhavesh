namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileaname : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoadFiles", "FileName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoadFiles", "FileName", c => c.String(maxLength: 100));
        }
    }
}
