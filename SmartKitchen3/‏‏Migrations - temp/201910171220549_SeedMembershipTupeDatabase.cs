namespace SmartKitchen3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMembershipTupeDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO [dbo].[MembershipTypes](Name,SignUpFee) VALUES('Member',50)");
            Sql("INSERT INTO [dbo].[MembershipTypes](Name,SignUpFee) VALUES('SuperAdmin',0)");
        }
        
        public override void Down()
        {
        }
    }
}
