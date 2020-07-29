namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnavation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoadFiles", "Contact_ID", c => c.Int());
            CreateIndex("dbo.LoadFiles", "Contact_ID");
            AddForeignKey("dbo.LoadFiles", "Contact_ID", "dbo.Contacts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoadFiles", "Contact_ID", "dbo.Contacts");
            DropIndex("dbo.LoadFiles", new[] { "Contact_ID" });
            DropColumn("dbo.LoadFiles", "Contact_ID");
        }
    }
}
