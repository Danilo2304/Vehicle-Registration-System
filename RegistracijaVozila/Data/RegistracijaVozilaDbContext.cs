using Microsoft.EntityFrameworkCore;
using RegistracijaVozila.Models.Domain;

namespace RegistracijaVozila.Data
{
    public class RegistracijaVozilaDbContext : DbContext
    {
        public RegistracijaVozilaDbContext(DbContextOptions<RegistracijaVozilaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Klijent> Klijenti { get; set; }

        public DbSet<Vozilo> Vozila { get; set; }

        public DbSet<Osiguranje> Osiguranja { get; set; }

        public DbSet<Registracija> Registracije { get; set; }

        public DbSet<TipVozila> TipoviVozila { get; set; }

        public DbSet<MarkaVozila> MarkeVozila { get; set; }

        public DbSet<ModelVozila> ModeliVozila { get; set; }

        public DbSet<OsiguranjeCijene> OsiguranjeCijene { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vozilo>()
                .HasOne(v => v.ModelVozila)
                .WithMany()
                .HasForeignKey(v => v.ModelVozilaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vozilo>()
                .HasOne(v => v.MarkaVozila)
                .WithMany()
                .HasForeignKey(v => v.MarkaVozilaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vozilo>()
                .HasOne(v => v.TipVozila)
                .WithMany()
                .HasForeignKey(v => v.TipVozilaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MarkaVozila>()
                .HasOne(m => m.TipVozila)
                .WithMany(mv => mv.Marke)
                .HasForeignKey(m => m.TipVozilaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ModelVozila>()
                .HasOne(m => m.MarkaVozila)
                .WithMany(mv => mv.Modeli)
                .HasForeignKey(m => m.MarkaVozilaId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<OsiguranjeRegistracija>()
            //    .HasKey(or => new { or.RegistracijaId, or.OsiguranjeVozilaId });

            //modelBuilder.Entity<OsiguranjeRegistracija>()
            //    .HasOne(or => or.Registracija)
            //    .WithMany(r => r.OsiguranjeRegistracija)
            //    .HasForeignKey(or => or.RegistracijaId);

            //modelBuilder.Entity<OsiguranjeRegistracija>()
            //    .HasOne(or => or.Osiguranje)
            //    .WithMany(o => o.OsiguranjeRegistracija)
            //    .HasForeignKey(or => or.OsiguranjeVozilaId);
        }


    }
}
