namespace FantasticBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanmig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        bookId = c.String(nullable: false, maxLength: 128),
                        bookName = c.String(),
                        publishYear = c.String(),
                        price = c.Double(nullable: false),
                        quantity = c.Int(nullable: false),
                        authorName = c.String(),
                        catId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.bookId)
                .ForeignKey("dbo.categories", t => t.catId)
                .Index(t => t.catId);
            
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        catId = c.String(nullable: false, maxLength: 128),
                        catName = c.String(),
                    })
                .PrimaryKey(t => t.catId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "catId", "dbo.categories");
            DropIndex("dbo.Books", new[] { "catId" });
            DropTable("dbo.categories");
            DropTable("dbo.Books");
        }
    }
}
