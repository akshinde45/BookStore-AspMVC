namespace FantasticBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanmig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        username = c.String(nullable: false),
                        type = c.String(),
                        email = c.String(nullable: false),
                        pass = c.String(nullable: false),
                        address = c.String(nullable: false),
                        phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.users");
        }
    }
}
