namespace WaveFit2.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLocation_Place : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.location",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    state = c.String(),
                    acronym = c.String(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.place",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    idlocation = c.Int(nullable: false),
                    city = c.String(),
                    area = c.String(),
                    address = c.String(),
                    cep = c.String(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.location", t => t.idlocation, cascadeDelete: true)
                .Index(t => t.idlocation);
        }

        public override void Down()
        {
            DropForeignKey("dbo.place", "idlocation", "dbo.location");
            DropIndex("dbo.place", new[] { "idlocation" });
            DropTable("dbo.place");
            DropTable("dbo.location");
        }
    }
}