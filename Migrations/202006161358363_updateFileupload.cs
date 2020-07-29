namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFileupload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoadFiles", "FileCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LoadFiles", "FileCategory");
        }
    }
}
