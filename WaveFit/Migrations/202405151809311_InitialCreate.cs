namespace WaveFit2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.audioevaluation",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    leftmeatoscopy = c.String(),
                    rightmeatoscopy = c.String(),
                    leftearlrf = c.String(),
                    rightearlrf = c.String(),
                    leftearlaf = c.String(),
                    rightearlaf = c.String(),
                    leftearintesity = c.String(),
                    rightearintesity = c.String(),
                    wordsmono = c.String(),
                    wordsdi = c.String(),
                    wordstri = c.String(),
                    leftearmono = c.String(),
                    lefteardi = c.String(),
                    lefteartri = c.String(),
                    rightearmono = c.String(),
                    righteardi = c.String(),
                    righteartri = c.String(),
                    leftearvamin = c.String(),
                    leftearvamax = c.String(),
                    leftearvomin = c.String(),
                    leftearvomax = c.String(),
                    leftearlogo = c.String(),
                    rightearvamin = c.String(),
                    rightearvamax = c.String(),
                    rightearvomin = c.String(),
                    rightearvomax = c.String(),
                    rightearlogo = c.String(),
                    obs = c.String(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.session",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    idpatient = c.Int(nullable: false),
                    iduser = c.Int(nullable: false),
                    idaudiogram = c.Int(nullable: false),
                    idaudioevaluation = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.audioevaluation", t => t.idaudioevaluation, cascadeDelete: true)
                .ForeignKey("dbo.audiogram", t => t.idaudiogram, cascadeDelete: true)
                .ForeignKey("dbo.users", t => t.iduser, cascadeDelete: true)
                .ForeignKey("dbo.patients", t => t.idpatient, cascadeDelete: true)
                .Index(t => t.idpatient)
                .Index(t => t.iduser)
                .Index(t => t.idaudiogram)
                .Index(t => t.idaudioevaluation);

            CreateTable(
                "dbo.audiogram",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.frequency",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    idaudiogram = c.Int(nullable: false),
                    type = c.String(nullable: false, maxLength: 3),
                    ear = c.String(nullable: false, maxLength: 1),
                    hz125 = c.String(),
                    hz250 = c.String(),
                    hz500 = c.String(),
                    hz750 = c.String(),
                    hz1000 = c.String(),
                    hz1500 = c.String(),
                    hz2000 = c.String(),
                    hz3000 = c.String(),
                    hz4000 = c.String(),
                    hz6000 = c.String(),
                    hz8000 = c.String(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.audiogram", t => t.idaudiogram, cascadeDelete: true)
                .Index(t => t.idaudiogram);

            CreateTable(
                "dbo.users",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    name = c.String(nullable: false),
                    surname = c.String(nullable: false),
                    username = c.String(nullable: false),
                    password = c.String(nullable: false),
                    crfa = c.String(),
                    permission = c.Int(nullable: false),
                    enabled = c.Boolean(nullable: false),
                    image = c.Binary(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.patients",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    patientcode = c.Long(nullable: false),
                    name = c.String(nullable: false),
                    surname = c.String(),
                    gender = c.String(),
                    birthday = c.DateTime(nullable: false, storeType: "date"),
                    typedoc = c.String(),
                    numdoc = c.String(),
                    status = c.Boolean(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.fitting",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    idpatient = c.Int(nullable: false),
                    idhearingaid = c.Int(nullable: false),
                    channel = c.String(),
                    program = c.Int(nullable: false),
                    paramters = c.String(),
                    configuration = c.String(),
                    date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.hearingaid", t => t.idhearingaid, cascadeDelete: true)
                .ForeignKey("dbo.patients", t => t.idpatient, cascadeDelete: true)
                .Index(t => t.idpatient)
                .Index(t => t.idhearingaid);

            CreateTable(
                "dbo.hearingaid",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    serialnumber = c.Long(nullable: false),
                    device = c.String(),
                })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.gainplot",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    idfitting = c.Int(nullable: false),
                    plot = c.Binary(),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.fitting", t => t.idfitting, cascadeDelete: true)
                .Index(t => t.idfitting);
        }

        public override void Down()
        {
            DropForeignKey("dbo.session", "idpatient", "dbo.patients");
            DropForeignKey("dbo.fitting", "idpatient", "dbo.patients");
            DropForeignKey("dbo.gainplot", "idfitting", "dbo.fitting");
            DropForeignKey("dbo.fitting", "idhearingaid", "dbo.hearingaid");
            DropForeignKey("dbo.session", "iduser", "dbo.users");
            DropForeignKey("dbo.session", "idaudiogram", "dbo.audiogram");
            DropForeignKey("dbo.frequency", "idaudiogram", "dbo.audiogram");
            DropForeignKey("dbo.session", "idaudioevaluation", "dbo.audioevaluation");
            DropIndex("dbo.gainplot", new[] { "idfitting" });
            DropIndex("dbo.fitting", new[] { "idhearingaid" });
            DropIndex("dbo.fitting", new[] { "idpatient" });
            DropIndex("dbo.frequency", new[] { "idaudiogram" });
            DropIndex("dbo.session", new[] { "idaudioevaluation" });
            DropIndex("dbo.session", new[] { "idaudiogram" });
            DropIndex("dbo.session", new[] { "iduser" });
            DropIndex("dbo.session", new[] { "idpatient" });
            DropTable("dbo.gainplot");
            DropTable("dbo.hearingaid");
            DropTable("dbo.fitting");
            DropTable("dbo.patients");
            DropTable("dbo.users");
            DropTable("dbo.frequency");
            DropTable("dbo.audiogram");
            DropTable("dbo.session");
            DropTable("dbo.audioevaluation");
        }
    }
}