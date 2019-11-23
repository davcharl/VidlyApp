namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipType_DiscountToDiscountRate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "DiscountRate", c => c.Byte(nullable: false));
            DropColumn("dbo.MembershipTypes", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "Discount", c => c.Byte(nullable: false));
            DropColumn("dbo.MembershipTypes", "DiscountRate");
        }
    }
}
