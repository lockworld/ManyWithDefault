namespace MWD.ArvixeSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "mwd.Emails",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        EmailAddress = c.String(),
                        NickName = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        ForeignKey = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "mwd.People",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("mwd.People");
            DropTable("mwd.Emails");
        }
    }
}
