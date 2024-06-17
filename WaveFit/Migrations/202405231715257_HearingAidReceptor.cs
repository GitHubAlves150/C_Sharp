namespace WaveFit2.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class HearingAidReceptor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.hearingaid", "receptor", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.hearingaid", "receptor");
        }
    }
}