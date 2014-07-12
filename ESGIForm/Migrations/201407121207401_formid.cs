namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class formid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "FormId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "FormId");
        }
    }
}
