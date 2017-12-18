using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1
{
    public partial class recipeContext : DbContext
    {
        public virtual DbSet<Equipments> Equipments { get; set; }
        public virtual DbSet<Experiments> Experiments { get; set; }
        public virtual DbSet<Laboratories> Laboratories { get; set; }
        public virtual DbSet<Materials> Materials { get; set; }
        public virtual DbSet<Providers> Providers { get; set; }
        public virtual DbSet<Samples> Samples { get; set; }
        public virtual DbSet<Supplies> Supplies { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }

        public recipeContext(DbContextOptions<recipeContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-PNIQIOG\SQLEXPRESS;Database=recipe;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipments>(entity =>
            {
                entity.HasKey(e => e.IdEquipment);

                entity.Property(e => e.IdEquipment).HasColumnName("id_equipment");

                entity.Property(e => e.DateRelease)
                    .HasColumnName("date_release")
                    .HasColumnType("date");

                entity.Property(e => e.NameEquipment)
                    .HasColumnName("name_equipment")
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<Experiments>(entity =>
            {
                entity.HasKey(e => e.IdExperiment);

                entity.Property(e => e.IdExperiment).HasColumnName("id_experiment");

                entity.Property(e => e.Dates)
                    .HasColumnName("dates")
                    .HasColumnType("date");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.IdLaboratory).HasColumnName("id_laboratory");

                entity.Property(e => e.IdSample).HasColumnName("id_sample");

                entity.Property(e => e.IdWorker).HasColumnName("id_worker");

                entity.Property(e => e.ReceiveMass).HasColumnName("receive_mass");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.SupposeMass).HasColumnName("suppose_mass");

                entity.HasOne(d => d.IdLaboratoryNavigation)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.IdLaboratory)
                    .HasConstraintName("FK_Experiments_Laboratories");

                entity.HasOne(d => d.IdSampleNavigation)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.IdSample)
                    .HasConstraintName("FK_Experiments_Samples");

                entity.HasOne(d => d.IdWorkerNavigation)
                    .WithMany(p => p.Experiments)
                    .HasForeignKey(d => d.IdWorker)
                    .HasConstraintName("FK_Experiments_Workers");
            });

            modelBuilder.Entity<Laboratories>(entity =>
            {
                entity.HasKey(e => e.IdLaboratory);

                entity.Property(e => e.IdLaboratory).HasColumnName("id_laboratory");

                entity.Property(e => e.IdEquipment).HasColumnName("id_equipment");

                entity.Property(e => e.NumberLaboratory).HasColumnName("number_laboratory");

                entity.HasOne(d => d.IdEquipmentNavigation)
                    .WithMany(p => p.Laboratories)
                    .HasForeignKey(d => d.IdEquipment)
                    .HasConstraintName("FK_Laboratories_Equipments");
            });

            modelBuilder.Entity<Materials>(entity =>
            {
                entity.HasKey(e => e.IdMaterial);

                entity.Property(e => e.IdMaterial).HasColumnName("id_material");

                entity.Property(e => e.MassMaterial).HasColumnName("mass_material");

                entity.Property(e => e.NameMaterial)
                    .HasColumnName("name_material")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Property)
                    .HasColumnName("property")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Units)
                    .HasColumnName("units")
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<Providers>(entity =>
            {
                entity.HasKey(e => e.IdProvider);

                entity.Property(e => e.IdProvider).HasColumnName("id_provider");

                entity.Property(e => e.Adress)
                    .HasColumnName("adress")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<Samples>(entity =>
            {
                entity.HasKey(e => e.IdSample);

                entity.Property(e => e.IdSample).HasColumnName("id_sample");

                entity.Property(e => e.IdMaterial).HasColumnName("id_material");

                entity.Property(e => e.MassSample).HasColumnName("mass_sample");

                entity.Property(e => e.NameSample)
                    .HasColumnName("name_sample")
                    .HasColumnType("nchar(10)");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.Samples)
                    .HasForeignKey(d => d.IdMaterial)
                    .HasConstraintName("FK_Samples_Materials");
            });

            modelBuilder.Entity<Supplies>(entity =>
            {
                entity.HasKey(e => e.IdSupple);

                entity.Property(e => e.IdSupple).HasColumnName("id_supple");

                entity.Property(e => e.DateSupple)
                    .HasColumnName("date_supple")
                    .HasColumnType("date");

                entity.Property(e => e.IdMaterial).HasColumnName("id_material");

                entity.Property(e => e.IdProvider).HasColumnName("id_provider");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.IdMaterial)
                    .HasConstraintName("FK_Supplies_Materials");

                entity.HasOne(d => d.IdProviderNavigation)
                    .WithMany(p => p.Supplies)
                    .HasForeignKey(d => d.IdProvider)
                    .HasConstraintName("FK_Supplies_Providers");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("nchar(30)");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nchar(15)");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasColumnType("nchar(25)");
            });

            modelBuilder.Entity<Workers>(entity =>
            {
                entity.HasKey(e => e.IdWorker);

                entity.Property(e => e.IdWorker).HasColumnName("id_worker");

                entity.Property(e => e.Adress)
                    .HasColumnName("adress")
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasColumnType("nchar(50)");
            });
        }
    }
}
