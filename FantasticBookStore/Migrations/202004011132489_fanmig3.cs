namespace FantasticBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanmig3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.checkouts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        bookId = c.String(),
                        bookName = c.String(),
                        userId = c.String(),
                        categoryName = c.String(),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        authorName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.checkouts");
        }
    }
}
