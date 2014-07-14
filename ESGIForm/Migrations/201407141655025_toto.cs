namespace ESGIForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Forms", "CloseDate", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Forms", "CloseDate", c => c.DateTime(nullable: false));
        }
    }
}
