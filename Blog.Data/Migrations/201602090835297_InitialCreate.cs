namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        category_id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 255),
                        category_slug = c.String(nullable: false, maxLength: 255),
                        category_description = c.String(),
                        category_parent = c.Int(nullable: false),
                        category_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.category_id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        user_username = c.String(nullable: false, maxLength: 50),
                        user_password = c.String(nullable: false, maxLength: 64),
                        user_email = c.String(nullable: false, maxLength: 256),
                        user_firstname = c.String(maxLength: 50),
                        user_lastname = c.String(maxLength: 50),
                        user_avatar = c.String(),
                        user_displayname = c.String(),
                        user_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
            DropTable("dbo.Category");
        }
    }
}
