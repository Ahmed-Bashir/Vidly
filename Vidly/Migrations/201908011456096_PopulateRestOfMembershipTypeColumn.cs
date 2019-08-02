namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateRestOfMembershipTypeColumn : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET MembershipTypes = 'Monthly' WHERE Id in (3, 4)");
        }
        
        public override void Down()
        {
        }
    }
}
