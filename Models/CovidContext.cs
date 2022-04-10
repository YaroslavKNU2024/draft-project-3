using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoronavirusBase
{
    public partial class CovidContext : DbContext
    {
        public CovidContext()
        {
        }

        public CovidContext(DbContextOptions<CovidContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassVirus> ClassViruses { get; set; } = null!;
        public virtual DbSet<CountriesVariant> CountriesVariants { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Symptom> Symptoms { get; set; } = null!;
        public virtual DbSet<Variant> Variants { get; set; } = null!;
        public virtual DbSet<Virus> Viruses { get; set; } = null!;
        public virtual DbSet<VirusGroup> VirusGroups { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0SH5QUI; Database=Covid; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassVirus>(entity =>
            {
                entity.HasKey(e => e.IdClass);

                entity.Property(e => e.InfoVirusClass).HasMaxLength(100);

                entity.Property(e => e.TypeClass).HasMaxLength(50);
            });

            modelBuilder.Entity<CountriesVariant>(entity =>
            {
                entity.HasKey(e => new { e.IdCountry, e.IdVariant })
                    .HasName("PK_TableCountries");

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.CountriesVariants)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TableCountries_Countries");

                entity.HasOne(d => d.IdVariantNavigation)
                    .WithMany(p => p.CountriesVariants)
                    .HasForeignKey(d => d.IdVariant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TableCountries_Variants");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry);

                entity.Property(e => e.NameCountry).HasMaxLength(50);
            });

            modelBuilder.Entity<Symptom>(entity =>
            {
                entity.HasKey(e => e.IdSymptom);

                entity.Property(e => e.NameSymptom).HasMaxLength(100);
            });

            modelBuilder.Entity<Variant>(entity =>
            {
                entity.HasKey(e => e.IdVariant);

                entity.Property(e => e.DateDiscovered).HasColumnType("date");

                entity.Property(e => e.VariantName).HasMaxLength(50);

                entity.Property(e => e.VariantOrigin).HasMaxLength(50);

                entity.HasOne(d => d.IdVirusNavigation)
                    .WithMany(p => p.Variants)
                    .HasForeignKey(d => d.IdVirus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Variants_Virus1");

                entity.HasMany(d => d.IdSymptoms)
                    .WithMany(p => p.IdVariants)
                    .UsingEntity<Dictionary<string, object>>(
                        "SymptomsVariant",
                        l => l.HasOne<Symptom>().WithMany().HasForeignKey("IdSymptom").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TableSymptoms_Symptoms"),
                        r => r.HasOne<Variant>().WithMany().HasForeignKey("IdVariant").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_TableSymptoms_Variants"),
                        j =>
                        {
                            j.HasKey("IdVariant", "IdSymptom").HasName("PK_TableSymptoms");

                            j.ToTable("SymptomsVariants");
                        });
            });

            modelBuilder.Entity<Virus>(entity =>
            {
                entity.HasKey(e => e.IdVirus)
                    .HasName("PK_Virus");

                entity.Property(e => e.DateDiscovered).HasColumnType("date");

                entity.Property(e => e.VirusName).HasMaxLength(50);

                entity.HasOne(d => d.IdVirusGroupNavigation)
                    .WithMany(p => p.Viruses)
                    .HasForeignKey(d => d.IdVirusGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Virus_VirusGroup1");
            });

            modelBuilder.Entity<VirusGroup>(entity =>
            {
                entity.HasKey(e => e.IdGroup)
                    .HasName("PK_VirusGroup");

                entity.Property(e => e.DateDiscovered).HasColumnType("date");

                entity.Property(e => e.GroupInfo).HasMaxLength(50);

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.HasOne(d => d.IdClassNavigation)
                    .WithMany(p => p.VirusGroups)
                    .HasForeignKey(d => d.IdClass)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VirusGroups_ClassViruses");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
