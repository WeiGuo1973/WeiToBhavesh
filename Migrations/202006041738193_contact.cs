namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contact : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoadFiles", "Contact_ID", "dbo.Contacts");
            DropIndex("dbo.LoadFiles", new[] { "Contact_ID" });
            AddColumn("dbo.Contacts", "OverheadFile", c => c.String(maxLength: 100));
            AddColumn("dbo.Contacts", "CertificationFile", c => c.String(maxLength: 100));
            DropColumn("dbo.LoadFiles", "Contact_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoadFiles", "Contact_ID", c => c.Int());
            DropColumn("dbo.Contacts", "CertificationFile");
            DropColumn("dbo.Contacts", "OverheadFile");
            CreateIndex("dbo.LoadFiles", "Contact_ID");
            AddForeignKey("dbo.LoadFiles", "Contact_ID", "dbo.Contacts", "ID");
        }
    }
}
