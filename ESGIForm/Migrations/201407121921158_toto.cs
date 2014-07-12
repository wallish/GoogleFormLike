namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Guid(nullable: false),
                        QuestionId = c.Guid(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.AnswerId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Answers");
        }
    }
}
