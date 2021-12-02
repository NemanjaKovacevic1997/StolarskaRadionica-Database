using BazaPodataka.Model;
using BazaPodataka.Model.Authentication;
using BazaPodataka.Model.Vise_ViseEntiteti;
using PristupBaziPodataka.Model.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PristupBaziPodataka
{
    public class DatabaseContext : DbContext 
    {
        public DatabaseContext() : base()
        {
            this.Database.CreateIfNotExists();
        }

        public DbSet<Musterija> Musterije { get; set; }
        public DbSet<Zahtev> Zahtevi { get; set; }
        public DbSet<VrstaNamestaja> VrsteNamestaja { get; set; }
        public DbSet<VrstaDrveta> VrsteDrveta { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<StavkaPorudzbine> StavkePorudzbine { get; set; }
        public DbSet<StavkaCenovnika> StavkeCenovnika { get; set; }
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<Magacin> Magacini { get; set; }
        public DbSet<DobavljacDrveta> DobavljaciDrveta { get; set; }
        public DbSet<Nudi> Nudi { get; set; }
        public DbSet<Sadrzi> Sadrzi { get; set; }
        public DbSet<Skladisti> Skladisti { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<AdministratorKorisnik>  Administratori { get; set; }
        public DbSet<StolarKorisnik> StolariKorisnici { get; set; }
        public DbSet<SegrtKorisnik> SegrtiKorisnici { get; set; }
        public DbSet<MagacionerKorisnik> MagacioneriKorisnici { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //musterija - ogranicenje kljuca
            modelBuilder.Entity<Musterija>().HasKey(x => x.JMBG);

            //musterija (0,N) <-> (1,1) (id) porudzbina
            modelBuilder.Entity<Porudzbina>().HasRequired(x => x.Musterija).WithMany(x => x.Porudzbine).HasForeignKey(x => x.MusterijaId);
            modelBuilder.Entity<Porudzbina>().HasKey(x => new { x.MusterijaId, x.Id });

            // porudzbina (0,N) <-> (1,1) (id) stavka porudzbine
            modelBuilder.Entity<StavkaPorudzbine>().HasRequired(x => x.Porudzbina).WithMany(x => x.StavkePorudzbine).HasForeignKey(x => new { x.PorudzbinaMusterijaId , x.PorudzbinaId });
            modelBuilder.Entity<StavkaPorudzbine>().HasKey(x => new { x.PorudzbinaId, x.PorudzbinaMusterijaId , x.RedniBrojStavke });
            

            modelBuilder.Entity<StavkaCenovnika>().HasKey(x => x.RedniBrojStavke);

            //stavka cenovnika (1,1) <-> (0,N) vrsta namestaja
            modelBuilder.Entity<StavkaCenovnika>().HasRequired(x => x.VrstaNamestaja).WithMany(x => x.StavkeCenovnika).HasForeignKey(x => x.VrstaNamestajaId).WillCascadeOnDelete(false);
            modelBuilder.Entity<VrstaNamestaja>().Property(x => x.Materijal).HasMaxLength(35);
            modelBuilder.Entity<VrstaNamestaja>().Property(x => x.Naziv).HasMaxLength(35);
            //vrsta namestaja jedinstvenost (stavljeno u anotacije)

            //stavka cenovnika (0,N) <-> (1,1) stavka poruzdbine
            modelBuilder.Entity<StavkaPorudzbine>().HasRequired(x => x.StavkaCenovnika).WithMany(x => x.StavkePorudzbine).HasForeignKey(x => x.RedniBrojStavkeCenovnika).WillCascadeOnDelete(false);

            //isa : radnik (0,1) <-> (1,1) magacioner, segrt, stolar (proveriti kako pravi)

            modelBuilder.Entity<Segrt>().Property(x => x.Ocena).IsOptional();

            //zahtev (1,1) <-> (0,N) magacioner
            modelBuilder.Entity<Zahtev>().HasRequired(x => x.Magacioner).WithMany(x => x.Zahtevi).HasForeignKey(x => x.MagacionerId).WillCascadeOnDelete(false);

            //ogranicenja stranog kljuca za sve meny-to-meny asocijativne entitete

            //popravka za Skladisti
            modelBuilder.Entity<Skladisti>().HasRequired(x => x.Magacin).WithMany(x => x.Skladisti).HasForeignKey(x => x.MagacinId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Skladisti>().HasRequired(x => x.VrstaDrveta).WithMany(x => x.Skladisti).HasForeignKey(x => x.VrstaDrvetaId).WillCascadeOnDelete(true);

            //definisanje kljuca za sve meny-to-meny asocijativne entitete
            modelBuilder.Entity<Skladisti>().HasKey(x => new { x.VrstaDrvetaId, x.MagacinId });
            modelBuilder.Entity<Sadrzi>().HasKey(x => new { x.VrstaDrvetaId, x.ZahtevId });
            modelBuilder.Entity<Nudi>().HasKey(x => new { x.VrstaDrvetaId, x.DobavljacDrvetaId });

            //autoincrement off za svaki entitet
            modelBuilder.Entity<Magacin>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Musterija>().Property(x => x.JMBG).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Porudzbina>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<StavkaPorudzbine>().Property(x => x.RedniBrojStavke).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<StavkaCenovnika>().Property(x => x.RedniBrojStavke).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<VrstaNamestaja>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Radnik>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Zahtev>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<VrstaDrveta>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<DobavljacDrveta>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //TPT resenje
            modelBuilder.Entity<Magacioner>().ToTable("Magacioneri");
            modelBuilder.Entity<Segrt>().ToTable("Segrti");
            modelBuilder.Entity<Stolar>().ToTable("Stolari");


            // Korisnici : 
            modelBuilder.Entity<Korisnik>().HasKey(x => new { x.KorisnickoIme });
            modelBuilder.Entity<Korisnik>().Property(x => x.KorisnickoIme).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            // TPT resenje
            modelBuilder.Entity<Korisnik>().ToTable("Korisnici");
            modelBuilder.Entity<MagacionerKorisnik>().ToTable("MagacioneriKorisnici");
            modelBuilder.Entity<AdministratorKorisnik>().ToTable("Administratori");
            modelBuilder.Entity<StolarKorisnik>().ToTable("StolariKorisnici");
            modelBuilder.Entity<SegrtKorisnik>().ToTable("SegrtiKorisnici");
        }
    }
}
