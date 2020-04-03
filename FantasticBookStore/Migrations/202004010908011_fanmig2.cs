namespace FantasticBookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fanmig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.carts",
                c => new
                    {
                        cartId = c.String(nullable: false, maxLength: 128),
                        userId = c.String(),
                        bookId = c.String(),
                        bookName = c.String(),
                        categoryName = c.String(),
                        price = c.Double(nullable: false),
                        authorName = c.String(),
                    })
                .PrimaryKey(t => t.cartId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.carts");
        }
    }
}
