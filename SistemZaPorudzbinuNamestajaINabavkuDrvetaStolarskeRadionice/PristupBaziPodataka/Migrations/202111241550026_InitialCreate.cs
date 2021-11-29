namespace PristupBaziPodataka.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Korisnici",
                c => new
                    {
                        KorisnickoIme = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Lozinka = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.KorisnickoIme);
            
            CreateTable(
                "dbo.Cenovnik",
                c => new
                    {
                        StolarskaRadionicaId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        StolarId = c.Int(),
                    })
                .PrimaryKey(t => t.StolarskaRadionicaId)
                .ForeignKey("dbo.Stolari", t => t.StolarId)
                .ForeignKey("dbo.StolarskaRadionica", t => t.StolarskaRadionicaId, cascadeDelete: true)
                .Index(t => t.StolarskaRadionicaId)
                .Index(t => t.StolarId);
            
            CreateTable(
                "dbo.StavkaCenovnika",
                c => new
                    {
                        CenovnikId = c.Int(nullable: false),
                        RedniBrojStavke = c.Int(nullable: false),
                        Cena = c.Double(nullable: false),
                        VrstaNamestajaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CenovnikId, t.RedniBrojStavke })
                .ForeignKey("dbo.Cenovnik", t => t.CenovnikId, cascadeDelete: true)
                .ForeignKey("dbo.VrstaNamestaja", t => t.VrstaNamestajaId)
                .Index(t => t.CenovnikId)
                .Index(t => t.VrstaNamestajaId);
            
            CreateTable(
                "dbo.StavkaPorudzbine",
                c => new
                    {
                        PorudzbinaId = c.Int(nullable: false),
                        PorudzbinaMusterijaId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RedniBrojStavke = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        StavkaCenovnikaId = c.Int(nullable: false),
                        RedniBrojStavkeCenovnika = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PorudzbinaId, t.PorudzbinaMusterijaId, t.RedniBrojStavke })
                .ForeignKey("dbo.Porudzbina", t => new { t.PorudzbinaMusterijaId, t.PorudzbinaId }, cascadeDelete: true)
                .ForeignKey("dbo.StavkaCenovnika", t => new { t.StavkaCenovnikaId, t.RedniBrojStavkeCenovnika })
                .Index(t => new { t.PorudzbinaMusterijaId, t.PorudzbinaId })
                .Index(t => new { t.StavkaCenovnikaId, t.RedniBrojStavkeCenovnika });
            
            CreateTable(
                "dbo.Porudzbina",
                c => new
                    {
                        MusterijaId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Id = c.Int(nullable: false),
                        DatumPorudzbine = c.DateTime(nullable: false, precision: 0),
                        Ukupno = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MusterijaId, t.Id })
                .ForeignKey("dbo.Musterija", t => t.MusterijaId, cascadeDelete: true)
                .Index(t => t.MusterijaId);
            
            CreateTable(
                "dbo.Musterija",
                c => new
                    {
                        JMBG = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Ime = c.String(unicode: false),
                        Prezime = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.JMBG);
            
            CreateTable(
                "dbo.VrstaNamestaja",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Materijal = c.String(maxLength: 35, storeType: "nvarchar"),
                        Naziv = c.String(maxLength: 35, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Materijal, t.Naziv }, unique: true, name: "IX_FirstAndSecond");
            
            CreateTable(
                "dbo.Radnik",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Ime = c.String(unicode: false),
                        Prezime = c.String(unicode: false),
                        Zanimanje = c.String(unicode: false),
                        StolarskaRadionicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StolarskaRadionica", t => t.StolarskaRadionicaId, cascadeDelete: true)
                .Index(t => t.StolarskaRadionicaId);
            
            CreateTable(
                "dbo.Skladisti",
                c => new
                    {
                        VrstaDrvetaId = c.Int(nullable: false),
                        MagacinId = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                        Radnik_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.VrstaDrvetaId, t.MagacinId })
                .ForeignKey("dbo.Radnik", t => t.Radnik_Id)
                .ForeignKey("dbo.Magacin", t => t.MagacinId, cascadeDelete: true)
                .ForeignKey("dbo.VrstaDrveta", t => t.VrstaDrvetaId, cascadeDelete: true)
                .Index(t => t.VrstaDrvetaId)
                .Index(t => t.MagacinId)
                .Index(t => t.Radnik_Id);
            
            CreateTable(
                "dbo.Magacin",
                c => new
                    {
                        StolarskaRadionicaId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StolarskaRadionicaId)
                .ForeignKey("dbo.StolarskaRadionica", t => t.StolarskaRadionicaId, cascadeDelete: true)
                .Index(t => t.StolarskaRadionicaId);
            
            CreateTable(
                "dbo.StolarskaRadionica",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Naziv = c.String(unicode: false),
                        Adresa = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zahtev",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        DatumNastanka = c.DateTime(nullable: false, precision: 0),
                        MagacionerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Magacioneri", t => t.MagacionerId)
                .Index(t => t.MagacionerId);
            
            CreateTable(
                "dbo.Sadrzi",
                c => new
                    {
                        VrstaDrvetaId = c.Int(nullable: false),
                        ZahtevId = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VrstaDrvetaId, t.ZahtevId })
                .ForeignKey("dbo.VrstaDrveta", t => t.VrstaDrvetaId, cascadeDelete: true)
                .ForeignKey("dbo.Zahtev", t => t.ZahtevId, cascadeDelete: true)
                .Index(t => t.VrstaDrvetaId)
                .Index(t => t.ZahtevId);
            
            CreateTable(
                "dbo.VrstaDrveta",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Naziv = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Nudi",
                c => new
                    {
                        VrstaDrvetaId = c.Int(nullable: false),
                        DobavljacDrvetaId = c.Int(nullable: false),
                        CenaPoKubnomMetru = c.Double(nullable: false),
                        Kolicina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VrstaDrvetaId, t.DobavljacDrvetaId })
                .ForeignKey("dbo.DobavljacDrveta", t => t.DobavljacDrvetaId, cascadeDelete: true)
                .ForeignKey("dbo.VrstaDrveta", t => t.VrstaDrvetaId, cascadeDelete: true)
                .Index(t => t.VrstaDrvetaId)
                .Index(t => t.DobavljacDrvetaId);
            
            CreateTable(
                "dbo.DobavljacDrveta",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Naziv = c.String(unicode: false),
                        Adresa = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SaradjujeSa",
                c => new
                    {
                        DobavljacDrvetaId = c.Int(nullable: false),
                        StolarskaRadionicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DobavljacDrvetaId, t.StolarskaRadionicaId })
                .ForeignKey("dbo.DobavljacDrveta", t => t.DobavljacDrvetaId, cascadeDelete: true)
                .ForeignKey("dbo.StolarskaRadionica", t => t.StolarskaRadionicaId, cascadeDelete: true)
                .Index(t => t.DobavljacDrvetaId)
                .Index(t => t.StolarskaRadionicaId);
            
            CreateTable(
                "dbo.ImaPonudu",
                c => new
                    {
                        DobavljacDrvetaNudiId = c.Int(nullable: false),
                        VrstaDrvetaId = c.Int(nullable: false),
                        DobavljacDrvetaSaradjujeId = c.Int(nullable: false),
                        StolarskaRadionicaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DobavljacDrvetaNudiId, t.VrstaDrvetaId, t.DobavljacDrvetaSaradjujeId, t.StolarskaRadionicaId })
                .ForeignKey("dbo.Nudi", t => new { t.DobavljacDrvetaNudiId, t.VrstaDrvetaId })
                .ForeignKey("dbo.SaradjujeSa", t => new { t.DobavljacDrvetaSaradjujeId, t.StolarskaRadionicaId }, cascadeDelete: true)
                .Index(t => new { t.DobavljacDrvetaNudiId, t.VrstaDrvetaId })
                .Index(t => new { t.DobavljacDrvetaSaradjujeId, t.StolarskaRadionicaId });
            
            CreateTable(
                "dbo.Administratori",
                c => new
                    {
                        KorisnickoIme = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.KorisnickoIme)
                .ForeignKey("dbo.Korisnici", t => t.KorisnickoIme)
                .Index(t => t.KorisnickoIme);
            
            CreateTable(
                "dbo.SegrtiKorisnici",
                c => new
                    {
                        KorisnickoIme = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.KorisnickoIme)
                .ForeignKey("dbo.Korisnici", t => t.KorisnickoIme)
                .Index(t => t.KorisnickoIme);
            
            CreateTable(
                "dbo.StolariKorisnici",
                c => new
                    {
                        KorisnickoIme = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.KorisnickoIme)
                .ForeignKey("dbo.Korisnici", t => t.KorisnickoIme)
                .Index(t => t.KorisnickoIme);
            
            CreateTable(
                "dbo.Segrti",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Ocena = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Radnik", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Magacioneri",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Radnik", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Stolari",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Radnik", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stolari", "Id", "dbo.Radnik");
            DropForeignKey("dbo.Magacioneri", "Id", "dbo.Radnik");
            DropForeignKey("dbo.Segrti", "Id", "dbo.Radnik");
            DropForeignKey("dbo.StolariKorisnici", "KorisnickoIme", "dbo.Korisnici");
            DropForeignKey("dbo.SegrtiKorisnici", "KorisnickoIme", "dbo.Korisnici");
            DropForeignKey("dbo.Administratori", "KorisnickoIme", "dbo.Korisnici");
            DropForeignKey("dbo.Cenovnik", "StolarskaRadionicaId", "dbo.StolarskaRadionica");
            DropForeignKey("dbo.Cenovnik", "StolarId", "dbo.Stolari");
            DropForeignKey("dbo.Skladisti", "VrstaDrvetaId", "dbo.VrstaDrveta");
            DropForeignKey("dbo.Skladisti", "MagacinId", "dbo.Magacin");
            DropForeignKey("dbo.Magacin", "StolarskaRadionicaId", "dbo.StolarskaRadionica");
            DropForeignKey("dbo.Sadrzi", "ZahtevId", "dbo.Zahtev");
            DropForeignKey("dbo.Sadrzi", "VrstaDrvetaId", "dbo.VrstaDrveta");
            DropForeignKey("dbo.Nudi", "VrstaDrvetaId", "dbo.VrstaDrveta");
            DropForeignKey("dbo.SaradjujeSa", "StolarskaRadionicaId", "dbo.StolarskaRadionica");
            DropForeignKey("dbo.ImaPonudu", new[] { "DobavljacDrvetaSaradjujeId", "StolarskaRadionicaId" }, "dbo.SaradjujeSa");
            DropForeignKey("dbo.ImaPonudu", new[] { "DobavljacDrvetaNudiId", "VrstaDrvetaId" }, "dbo.Nudi");
            DropForeignKey("dbo.SaradjujeSa", "DobavljacDrvetaId", "dbo.DobavljacDrveta");
            DropForeignKey("dbo.Nudi", "DobavljacDrvetaId", "dbo.DobavljacDrveta");
            DropForeignKey("dbo.Zahtev", "MagacionerId", "dbo.Magacioneri");
            DropForeignKey("dbo.Radnik", "StolarskaRadionicaId", "dbo.StolarskaRadionica");
            DropForeignKey("dbo.Skladisti", "Radnik_Id", "dbo.Radnik");
            DropForeignKey("dbo.StavkaCenovnika", "VrstaNamestajaId", "dbo.VrstaNamestaja");
            DropForeignKey("dbo.StavkaPorudzbine", new[] { "StavkaCenovnikaId", "RedniBrojStavkeCenovnika" }, "dbo.StavkaCenovnika");
            DropForeignKey("dbo.StavkaPorudzbine", new[] { "PorudzbinaMusterijaId", "PorudzbinaId" }, "dbo.Porudzbina");
            DropForeignKey("dbo.Porudzbina", "MusterijaId", "dbo.Musterija");
            DropForeignKey("dbo.StavkaCenovnika", "CenovnikId", "dbo.Cenovnik");
            DropIndex("dbo.Stolari", new[] { "Id" });
            DropIndex("dbo.Magacioneri", new[] { "Id" });
            DropIndex("dbo.Segrti", new[] { "Id" });
            DropIndex("dbo.StolariKorisnici", new[] { "KorisnickoIme" });
            DropIndex("dbo.SegrtiKorisnici", new[] { "KorisnickoIme" });
            DropIndex("dbo.Administratori", new[] { "KorisnickoIme" });
            DropIndex("dbo.ImaPonudu", new[] { "DobavljacDrvetaSaradjujeId", "StolarskaRadionicaId" });
            DropIndex("dbo.ImaPonudu", new[] { "DobavljacDrvetaNudiId", "VrstaDrvetaId" });
            DropIndex("dbo.SaradjujeSa", new[] { "StolarskaRadionicaId" });
            DropIndex("dbo.SaradjujeSa", new[] { "DobavljacDrvetaId" });
            DropIndex("dbo.Nudi", new[] { "DobavljacDrvetaId" });
            DropIndex("dbo.Nudi", new[] { "VrstaDrvetaId" });
            DropIndex("dbo.Sadrzi", new[] { "ZahtevId" });
            DropIndex("dbo.Sadrzi", new[] { "VrstaDrvetaId" });
            DropIndex("dbo.Zahtev", new[] { "MagacionerId" });
            DropIndex("dbo.Magacin", new[] { "StolarskaRadionicaId" });
            DropIndex("dbo.Skladisti", new[] { "Radnik_Id" });
            DropIndex("dbo.Skladisti", new[] { "MagacinId" });
            DropIndex("dbo.Skladisti", new[] { "VrstaDrvetaId" });
            DropIndex("dbo.Radnik", new[] { "StolarskaRadionicaId" });
            DropIndex("dbo.VrstaNamestaja", "IX_FirstAndSecond");
            DropIndex("dbo.Porudzbina", new[] { "MusterijaId" });
            DropIndex("dbo.StavkaPorudzbine", new[] { "StavkaCenovnikaId", "RedniBrojStavkeCenovnika" });
            DropIndex("dbo.StavkaPorudzbine", new[] { "PorudzbinaMusterijaId", "PorudzbinaId" });
            DropIndex("dbo.StavkaCenovnika", new[] { "VrstaNamestajaId" });
            DropIndex("dbo.StavkaCenovnika", new[] { "CenovnikId" });
            DropIndex("dbo.Cenovnik", new[] { "StolarId" });
            DropIndex("dbo.Cenovnik", new[] { "StolarskaRadionicaId" });
            DropTable("dbo.Stolari");
            DropTable("dbo.Magacioneri");
            DropTable("dbo.Segrti");
            DropTable("dbo.StolariKorisnici");
            DropTable("dbo.SegrtiKorisnici");
            DropTable("dbo.Administratori");
            DropTable("dbo.ImaPonudu");
            DropTable("dbo.SaradjujeSa");
            DropTable("dbo.DobavljacDrveta");
            DropTable("dbo.Nudi");
            DropTable("dbo.VrstaDrveta");
            DropTable("dbo.Sadrzi");
            DropTable("dbo.Zahtev");
            DropTable("dbo.StolarskaRadionica");
            DropTable("dbo.Magacin");
            DropTable("dbo.Skladisti");
            DropTable("dbo.Radnik");
            DropTable("dbo.VrstaNamestaja");
            DropTable("dbo.Musterija");
            DropTable("dbo.Porudzbina");
            DropTable("dbo.StavkaPorudzbine");
            DropTable("dbo.StavkaCenovnika");
            DropTable("dbo.Cenovnik");
            DropTable("dbo.Korisnici");
        }
    }
}
