namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dzdef : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "QuestionId_QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId_QuestionId" });
            AddColumn("dbo.Answers", "QuestionId", c => c.Guid(nullable: false));
            DropColumn("dbo.Answers", "QuestionId_QuestionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "QuestionId_QuestionId", c => c.Guid());
            DropColumn("dbo.Answers", "QuestionId");
            CreateIndex("dbo.Answers", "QuestionId_QuestionId");
            AddForeignKey("dbo.Answers", "QuestionId_QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}
