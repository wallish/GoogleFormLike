namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Form_FormId", "dbo.Forms");
            DropIndex("dbo.Questions", new[] { "Form_FormId" });
            DropColumn("dbo.Questions", "Form_FormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Form_FormId", c => c.Guid());
            CreateIndex("dbo.Questions", "Form_FormId");
            AddForeignKey("dbo.Questions", "Form_FormId", "dbo.Forms", "FormId");
        }
    }
}
