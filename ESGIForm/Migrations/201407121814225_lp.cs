namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Form_FormId", "dbo.Forms");
            DropIndex("dbo.Questions", new[] { "Form_FormId" });
            RenameColumn(table: "dbo.Questions", name: "Form_FormId", newName: "FormId");
            AlterColumn("dbo.Questions", "FormId", c => c.Guid(nullable: true));
            CreateIndex("dbo.Questions", "FormId");
            AddForeignKey("dbo.Questions", "FormId", "dbo.Forms", "FormId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "FormId", "dbo.Forms");
            DropIndex("dbo.Questions", new[] { "FormId" });
            AlterColumn("dbo.Questions", "FormId", c => c.Guid());
            RenameColumn(table: "dbo.Questions", name: "FormId", newName: "Form_FormId");
            CreateIndex("dbo.Questions", "Form_FormId");
            AddForeignKey("dbo.Questions", "Form_FormId", "dbo.Forms", "FormId");
        }
    }
}
