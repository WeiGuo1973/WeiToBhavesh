namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCon : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoadFiles", "FileName", c => c.String(maxLength: 100));
            DropColumn("dbo.Contacts", "OverheadFile");
            DropColumn("dbo.Contacts", "CertificationFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "CertificationFile", c => c.String(maxLength: 100));
            AddColumn("dbo.Contacts", "OverheadFile", c => c.String(maxLength: 100));
            AlterColumn("dbo.LoadFiles", "FileName", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
