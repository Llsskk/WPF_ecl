using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ecl
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<OrgLegForm> OrgLegForms { get; set; }
        public virtual DbSet<OrgRegistration> OrgRegistrations { get; set; }
        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=entityclients;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.DataReg).HasColumnType("datetime");

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameShort).HasMaxLength(50);

                entity.Property(e => e.OrgLegFormId).HasColumnName("OrgLegFormID");

                entity.Property(e => e.OrgRegistrationId).HasColumnName("OrgRegistrationID");

                entity.Property(e => e.PersonsId).HasColumnName("PersonsID");

                entity.HasOne(d => d.OrgLegForm)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.OrgLegFormId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_OrgLegForms");

                entity.HasOne(d => d.OrgRegistration)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.OrgRegistrationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_OrgRegistration");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.PersonsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Companies_Persons");
            });

            modelBuilder.Entity<OrgLegForm>(entity =>
            {
                entity.ToTable("OrgLegForm");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameShort)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrgRegistration>(entity =>
            {
                entity.ToTable("OrgRegistration");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.NameFull)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NameShort)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
