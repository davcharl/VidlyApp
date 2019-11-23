namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertDiscountRateDataToMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET DiscountRate=0 WHERE Id=1");
            Sql("UPDATE MembershipTypes SET DiscountRate=10 WHERE Id=2");
            Sql("UPDATE MembershipTypes SET DiscountRate=15 WHERE Id=3");
            Sql("UPDATE MembershipTypes SET DiscountRate=20 WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
