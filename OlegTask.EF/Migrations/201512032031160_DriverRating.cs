namespace OlegTask.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverRating : DbMigration
    {
        public override void Up()
        {
            Sql(@"CREATE FUNCTION [OlegTask].getRating
                (	
	                @id int
                )
                RETURNS float
                AS
                BEGIN
	                RETURN (SELECT ISNULL(AVG([Value]), 0) FROM [OlegTask].[Rating] WHERE [DriverId]= @id);
                END");

            Sql(@"ALTER TABLE [OlegTask].[Driver] ADD [Rating] AS [OlegTask].getRating([Id])");
        }

        public override void Down()
        {
            DropColumn("OlegTask.Driver", "Rating");

            Sql(@"DROP FUNCTION [OlegTask].getRating");
        }
    }
}
