namespace Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueFile : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LoadFiles", new[] { "FileName" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.LoadFiles", "FileName", unique: true);
        }
    }
}
