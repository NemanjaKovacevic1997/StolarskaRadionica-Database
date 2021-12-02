using BazaPodataka.Model;
using BazaPodataka.Model.Authentication;
using BazaPodataka.Model.Vise_ViseEntiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BazaPodataka
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
        public DbSet<StolarskaRadionica> StolarskeRadionice { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<StavkaPorudzbine> StavkePorudzbine { get; set; }
        public DbSet<StavkaCenovnika> StavkeCenovnika { get; set; }
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<Magacin> Magacini { get; set; }
        public DbSet<DobavljacDrveta> DobavljaciDrveta { get; set; }
        public DbSet<Cenovnik> Cenovnici { get; set; }
        public DbSet<ImaPonudu> ImaPonudu { get; set; }
        public DbSet<Nudi> Nudi { get; set; }
        public DbSet<Sadrzi> Sadrzi { get; set; }
        public DbSet<SaradjujeSa> SaradjujeSa { get; set; }
        public DbSet<Skladisti> Skladisti { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<AdministratorKorisnik>  Administratori { get; set; }
        public DbSet<StolarKorisnik> StolariKorisnici { get; set; }
        public DbSet<SegrtKorisnik> SegrtiKorisnici { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //musterija - ogranicenje kljuca
            modelBuilder.Entity<Musterija>().HasKey(x => x.JMBG);

            //musterija (0,N) <-> (1,1) (id) porudzbina
            modelBuilder.Entity<Porudzbina>().HasRequired(x => x.Musterija).WithMany(x => x.Porudzbine).HasForeignKey(x => x.MusterijaId);
            modelBuilder.Entity<Porudzbina>().HasKey(x => new { x.MusterijaId, x.Id });

            // porudzbina (0,N) <-> (1,1) (id) stavka porudzbine
            modelBuilder.Entity<StavkaPorudzbine>().HasRequired(x => x.Porudzbina).WithMany(x => x.StavkePorudzbine).HasForeignKey(x => new { x.PorudzbinaMusterijaId , x.PorudzbinaId });
            modelBuilder.Entity<StavkaPorudzbine>().HasKey(x => new { x.PorudzbinaId, x.PorudzbinaMusterijaId , x.RedniBrojStavke });

            //stolarska radionica (0, 1) <-> (1, 1) (id) cenovnik 
            modelBuilder.Entity<Cenovnik>().HasRequired(x => x.StolarskaRadionica);
            modelBuilder.Entity<Cenovnik>().HasKey(x => x.StolarskaRadionicaId);    //da li je ispravno ?

            //cenovnik (0, N) <-> (1,1 (id)) stavka cenovnika
            modelBuilder.Entity<StavkaCenovnika>().HasRequired(x => x.Cenovnik).WithMany(x => x.StavkeCenovnika).HasForeignKey(x => new { x.CenovnikId });
            modelBuilder.Entity<StavkaCenovnika>().HasKey(x => new { x.CenovnikId, x.RedniBrojStavke });

            //stavka cenovnika (1,1) <-> (0,N) vrsta namestaja
            modelBuilder.Entity<StavkaCenovnika>().HasRequired(x => x.VrstaNamestaja).WithMany(x => x.StavkeCenovnika).HasForeignKey(x => x.VrstaNamestajaId).WillCascadeOnDelete(false);
            modelBuilder.Entity<VrstaNamestaja>().Property(x => x.Materijal).HasMaxLength(35);
            modelBuilder.Entity<VrstaNamestaja>().Property(x => x.Naziv).HasMaxLength(35);
            //vrsta namestaja jedinstvenost (stavljeno u anotacije)

            //stavka cenovnika (0,N) <-> (1,1) stavka poruzdbine
            modelBuilder.Entity<StavkaPorudzbine>().HasRequired(x => x.StavkaCenovnika).WithMany(x => x.StavkePorudzbine).HasForeignKey(x => new { x.StavkaCenovnikaId, x.RedniBrojStavkeCenovnika }).WillCascadeOnDelete(false);

            //stolarska radionica (0, N) <-> (1, 1) radnik 
            modelBuilder.Entity<Radnik>().HasRequired(x => x.StolarskaRadionica);

            //isa : radnik (0,1) <-> (1,1) magacioner, segrt, stolar (proveriti kako pravi)

            modelBuilder.Entity<Segrt>().Property(x => x.Ocena).IsOptional();

            //zahtev (1,1) <-> (0,N) magacioner
            modelBuilder.Entity<Zahtev>().HasRequired(x => x.Magacioner).WithMany(x => x.Zahtevi).HasForeignKey(x => x.MagacionerId).WillCascadeOnDelete(false);

            //stolar (0,N) <-> (0,1) cenovnik (trebalo bi da je podeseno po default-u)
            modelBuilder.Entity<Cenovnik>().HasOptional(x => x.Stolar).WithMany(x => x.Cenovnici).HasForeignKey(x => x.StolarId).WillCascadeOnDelete(false);

            //cenovnik (id) (1,1) <-> stolarska radionica (0,1)
            modelBuilder.Entity<Cenovnik>().HasRequired(x => x.StolarskaRadionica).WithOptional(x => x.Cenovnik).WillCascadeOnDelete(true);

            //stolarska radionica (0,1) <-> (1,1) (id) magacin 
            modelBuilder.Entity<Magacin>().HasRequired(x => x.StolarskaRadionica).WithOptional(x => x.Magacin).WillCascadeOnDelete(true);
            modelBuilder.Entity<Magacin>().HasKey(x => x.StolarskaRadionicaId);    //da li je ispravno ?

            //ogranicenja stranog kljuca za sve meny-to-meny asocijativne entitete

            //popravka za Skladisti
            modelBuilder.Entity<Skladisti>().HasRequired(x => x.Magacin).WithMany(x => x.Skladisti).HasForeignKey(x => x.MagacinId).WillCascadeOnDelete(true);
            modelBuilder.Entity<Skladisti>().HasRequired(x => x.VrstaDrveta).WithMany(x => x.Skladisti).HasForeignKey(x => x.VrstaDrvetaId).WillCascadeOnDelete(true);

            //definisanje kljuca za sve meny-to-meny asocijativne entitete
            modelBuilder.Entity<Skladisti>().HasKey(x => new { x.VrstaDrvetaId, x.MagacinId });
            modelBuilder.Entity<Sadrzi>().HasKey(x => new { x.VrstaDrvetaId, x.ZahtevId });
            modelBuilder.Entity<Nudi>().HasKey(x => new { x.VrstaDrvetaId, x.DobavljacDrvetaId });
            modelBuilder.Entity<SaradjujeSa>().HasKey(x => new { x.DobavljacDrvetaId, x.StolarskaRadionicaId });

            modelBuilder.Entity<ImaPonudu>().HasRequired(x => x.Nudi).WithMany(x => x.ImaPonude).HasForeignKey(x => new { x.DobavljacDrvetaNudiId, x.VrstaDrvetaId }).WillCascadeOnDelete(false);
            modelBuilder.Entity<ImaPonudu>().HasRequired(x => x.SaradjujeSa).WithMany(x => x.ImaPonude).HasForeignKey(x => new { x.DobavljacDrvetaSaradjujeId, x.StolarskaRadionicaId });
            modelBuilder.Entity<ImaPonudu>().HasKey(x => new { x.DobavljacDrvetaNudiId, x.VrstaDrvetaId, x.DobavljacDrvetaSaradjujeId, x.StolarskaRadionicaId });


            //autoincrement off za svaki entitet
            modelBuilder.Entity<StolarskaRadionica>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Musterija>().Property(x => x.JMBG).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Porudzbina>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<StavkaPorudzbine>().Property(x => x.RedniBrojStavke).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<StavkaCenovnika>().Property(x => x.RedniBrojStavke).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<VrstaNamestaja>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<Cenovnik>().Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
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
            modelBuilder.Entity<AdministratorKorisnik>().ToTable("Administratori");
            modelBuilder.Entity<StolarKorisnik>().ToTable("StolariKorisnici");
            modelBuilder.Entity<SegrtKorisnik>().ToTable("SegrtiKorisnici");
        }
    }
}
