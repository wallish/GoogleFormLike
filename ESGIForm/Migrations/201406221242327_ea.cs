namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Form_FormId", c => c.Guid());
            CreateIndex("dbo.Questions", "Form_FormId");
            AddForeignKey("dbo.Questions", "Form_FormId", "dbo.Forms", "FormId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Form_FormId", "dbo.Forms");
            DropIndex("dbo.Questions", new[] { "Form_FormId" });
            DropColumn("dbo.Questions", "Form_FormId");
        }
    }
}
