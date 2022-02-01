using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HospitatorBackend.Models;

namespace HospitatorBackend.Data
{
    public partial class HospitatorDBContext : DbContext
    {
        public HospitatorDBContext()
        {
        }

        public HospitatorDBContext(DbContextOptions<HospitatorDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Formulazprotokolu> FromularzeProtokolow { get; set; } = null!;
        public virtual DbSet<Grupazajeciowa> GrupyZajeciowe { get; set; } = null!;
        public virtual DbSet<Harmonogram> Harmonogramy { get; set; } = null!;
        public virtual DbSet<Hospitacja> Hospitacje { get; set; } = null!;
        public virtual DbSet<Kurs> Kursy { get; set; } = null!;
        public virtual DbSet<Odwolanie> Odwolania { get; set; } = null!;
        public virtual DbSet<Protokol> Protokoly { get; set; } = null!;
        public virtual DbSet<Prowadzacy> Prowadzacy { get; set; } = null!;
        public virtual DbSet<Zespolhospitujacy> ZespolyHospitujace { get; set; } = null!;
        public virtual DbSet<Prowadzacy_ZespolHospitujacy> Prowadzacy_ZespolHospitujacy { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("datasource=127.0.0.1;port=3306;username=root;database=hospitator", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.22-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Prowadzacy_ZespolHospitujacy>(entity =>
            {
                entity.HasKey(e => new { e.ZespolID, e.ProwadzacyID });
            });

            modelBuilder.Entity<Formulazprotokolu>(entity =>
            {
                entity.HasOne(d => d.Protokol)
                    .WithOne(p => p.Formulazprotokolus)
                    //.HasForeignKey(d => d)
                    .HasConstraintName("formulazprotokolu_ibfk_1");
            });

            modelBuilder.Entity<Grupazajeciowa>(entity =>
            {
                entity.HasKey(e => e.Kod)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.KursKodNavigation)
                    .WithMany(p => p.Grupazajeciowas)
                    .HasForeignKey(d => d.KursKod)
                    .HasConstraintName("grupa_zaj_fk_k");

                entity.HasOne(d => d.Prowadzacy)
                    .WithMany(p => p.Grupazajeciowas)
                    .HasForeignKey(d => d.ProwadzacyId)
                    .HasConstraintName("grupa_zaj_fk_p");
            });

            modelBuilder.Entity<Hospitacja>(entity =>
            {
                entity.HasOne(d => d.Harmonogram)
                    .WithMany(p => p.Hospitacjas)
                    .HasForeignKey(d => d.HarmonogramId)
                    .HasConstraintName("hospitacja_ibfk_1");

                entity.HasOne(d => d.KursKodNavigation)
                    .WithMany(p => p.Hospitacjas)
                    .HasForeignKey(d => d.KursKod)
                    .HasConstraintName("hospitacja_ibfk_4");

                entity.HasOne(d => d.Prowadzacy)
                    .WithMany(p => p.Hospitacjas)
                    .HasForeignKey(d => d.ProwadzacyId)
                    .HasConstraintName("hospitacja_ibfk_3");

                entity.HasOne(d => d.ZespolHospitujacy)
                    .WithMany(p => p.Hospitacjas)
                    .HasForeignKey(d => d.ZespolHospitujacyId)
                    .HasConstraintName("hospitacja_ibfk_2");

                entity.HasOne(h => h.Protokol)
                .WithOne(p => p.Hospitacja);
            });

            modelBuilder.Entity<Kurs>(entity =>
            {
                entity.HasKey(e => e.Kod)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Odwolanie>(entity =>
            {
                entity.HasOne(d => d.Protokol)
                    .WithOne(p => p.Odwolanie)
                    .HasForeignKey<Odwolanie>(d => d.ProtokolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("odwolanie_ibfk_1");

                entity.HasOne(d => d.Prowadzacy)
                    .WithMany(p => p.Odwolanies)
                    .HasForeignKey(d => d.ProwadzacyId)
                    .HasConstraintName("odwolanie_ibfk_2");
            });

            modelBuilder.Entity<Prowadzacy>(entity =>
            {
                entity.HasMany(d => d.Zespols)
                    .WithMany(p => p.Prowadzacies)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProwadzacyZespolhospitujacy",
                        l => l.HasOne<Zespolhospitujacy>().WithMany().HasForeignKey("ZespolId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("prowadzacy_zespolhospitujacy_ibfk_2"),
                        r => r.HasOne<Prowadzacy>().WithMany().HasForeignKey("ProwadzacyId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("prowadzacy_zespolhospitujacy_ibfk_1"),
                        j =>
                        {
                            j.HasKey("ProwadzacyId", "ZespolId").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("prowadzacy_zespolhospitujacy");

                            j.HasIndex(new[] { "ZespolId" }, "zespol_id");

                            j.IndexerProperty<int>("ProwadzacyId").HasColumnType("int(11)").HasColumnName("prowadzacy_id");

                            j.IndexerProperty<int>("ZespolId").HasColumnType("int(11)").HasColumnName("zespol_id");
                        });
            });

            modelBuilder.Entity<Zespolhospitujacy>(entity =>
            {
                entity.HasOne(d => d.Prowadzacy)
                    .WithMany(p => p.Zespolhospitujacies)
                    .HasForeignKey(d => d.ProwadzacyId)
                    .HasConstraintName("zesp_hosp_fk_p");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
