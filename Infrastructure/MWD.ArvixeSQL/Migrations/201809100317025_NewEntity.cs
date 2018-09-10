namespace MWD.ArvixeSQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "mwd.NewEntityPrimeAlternates",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "mwd.NewEntitySubs",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ForeignKey_ID = c.Guid(nullable: false),
                        RelatedInfo = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("mwd.NewEntityPrimeAlternates", t => t.ForeignKey_ID, cascadeDelete: true)
                .ForeignKey("mwd.NewEntityPrimes", t => t.ForeignKey_ID, cascadeDelete: true)
                .Index(t => t.ForeignKey_ID);
            
            CreateTable(
                "mwd.NewEntityPrimes",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("mwd.NewEntitySubs", "ForeignKey_ID", "mwd.NewEntityPrimes");
            DropForeignKey("mwd.NewEntitySubs", "ForeignKey_ID", "mwd.NewEntityPrimeAlternates");
            DropIndex("mwd.NewEntitySubs", new[] { "ForeignKey_ID" });
            DropTable("mwd.NewEntityPrimes");
            DropTable("mwd.NewEntitySubs");
            DropTable("mwd.NewEntityPrimeAlternates");
        }
    }
}
