namespace WaveFit2.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddHealthCenter_Audiometer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.healthcenter",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    name = c.String(),
                    nickname = c.String(),
                    logo = c.Binary(),
                    cnpj = c.String(),
                    telephone = c.String(),
                    status = c.Boolean(nullable: false),
                    idplace = c.Int(nullable: false),
                    idaudiometer = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.audiometer", t => t.idaudiometer, cascadeDelete: true)
                .ForeignKey("dbo.place", t => t.idplace, cascadeDelete: true)
                .Index(t => t.idplace)
                .Index(t => t.idaudiometer);

            CreateTable(
                "dbo.audiometer",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    code = c.String(),
                    model = c.String(),
                    maintenance = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id);

            AddColumn("dbo.session", "idhealthcenter", c => c.Int());
            CreateIndex("dbo.session", "idhealthcenter");
            AddForeignKey("dbo.session", "idhealthcenter", "dbo.healthcenter", "id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.session", "idhealthcenter", "dbo.healthcenter");
            DropForeignKey("dbo.healthcenter", "idplace", "dbo.place");
            DropForeignKey("dbo.healthcenter", "idaudiometer", "dbo.audiometer");
            DropIndex("dbo.healthcenter", new[] { "idaudiometer" });
            DropIndex("dbo.healthcenter", new[] { "idplace" });
            DropIndex("dbo.session", new[] { "idhealthcenter" });
            DropColumn("dbo.session", "idhealthcenter");
            DropTable("dbo.audiometer");
            DropTable("dbo.healthcenter");
        }
    }
}