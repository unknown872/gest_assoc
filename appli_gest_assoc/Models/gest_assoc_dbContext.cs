using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace appli_gest_assoc.Models
{
    public partial class gest_assoc_dbContext : DbContext
    {
        public gest_assoc_dbContext()
        {
        }

        public gest_assoc_dbContext(DbContextOptions<gest_assoc_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activite> Activites { get; set; }
        public virtual DbSet<Association> Associations { get; set; }
        public virtual DbSet<Bureau> Bureaus { get; set; }
        public virtual DbSet<Depense> Depenses { get; set; }
        public virtual DbSet<Membre> Membres { get; set; }
        public virtual DbSet<MembreBureau> MembreBureaus { get; set; }
        public virtual DbSet<Recette> Recettes { get; set; }
        public virtual DbSet<TypeDepense> TypeDepenses { get; set; }
        public virtual DbSet<TypeRecette> TypeRecettes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=AppliGestAssocContextConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Activite>(entity =>
            {
                entity.HasKey(e => e.IdActivite);

                entity.ToTable("activites");

                entity.Property(e => e.IdActivite).HasColumnName("id_activite");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("libelle");
            });

            modelBuilder.Entity<Association>(entity =>
            {
                entity.HasKey(e => e.IdAssociation);

                entity.ToTable("associations");

                entity.Property(e => e.IdAssociation).HasColumnName("id_association");

                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("adresse");

                entity.Property(e => e.AnneeCreation)
                    .HasColumnType("datetime")
                    .HasColumnName("annee_creation");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.NomAssociation)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nom_association");

                entity.Property(e => e.Tel1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("tel1");

                entity.Property(e => e.Tel2)
                    .HasMaxLength(50)
                    .HasColumnName("tel2");
            });

            modelBuilder.Entity<Bureau>(entity =>
            {
                entity.HasKey(e => e.IdBureau);

                entity.ToTable("bureau");

                entity.HasIndex(e => e.NomBureau, "Bureau_unique")
                    .IsUnique();

                entity.Property(e => e.IdBureau).HasColumnName("id_bureau");

                entity.Property(e => e.ActiviteId).HasColumnName("activite_id");

                entity.Property(e => e.AssociationId).HasColumnName("association_id");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasColumnName("date_creation");

                entity.Property(e => e.NomBureau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nom_bureau");

                entity.HasOne(d => d.Activite)
                    .WithMany(p => p.Bureaus)
                    .HasForeignKey(d => d.ActiviteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bureau_activites");

                entity.HasOne(d => d.Association)
                    .WithMany(p => p.Bureaus)
                    .HasForeignKey(d => d.AssociationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bureau_associations");
            });

            modelBuilder.Entity<Depense>(entity =>
            {
                entity.HasKey(e => e.IdDepense);

                entity.ToTable("depenses");

                entity.Property(e => e.IdDepense).HasColumnName("id_depense");

                entity.Property(e => e.BureauId).HasColumnName("bureau_id");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("libelle");

                entity.Property(e => e.Montant)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("montant");

                entity.Property(e => e.TypeDepenseId).HasColumnName("type_depense_id");

                entity.HasOne(d => d.Bureau)
                    .WithMany(p => p.Depenses)
                    .HasForeignKey(d => d.BureauId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_depenses_bureau");

                entity.HasOne(d => d.TypeDepense)
                    .WithMany(p => p.Depenses)
                    .HasForeignKey(d => d.TypeDepenseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_depenses_type_depense");
            });

            modelBuilder.Entity<Membre>(entity =>
            {
                entity.HasKey(e => e.IdMembre);

                entity.ToTable("membres");

                entity.Property(e => e.IdMembre).HasColumnName("id_membre");

                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("adresse");

                entity.Property(e => e.DateNaiss)
                    .HasColumnType("datetime")
                    .HasColumnName("date_naiss");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.LieuNaiss)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lieu_naiss");

                entity.Property(e => e.NomMembre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nom_membre");

                entity.Property(e => e.PrenomMembre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("prenom_membre");

                entity.Property(e => e.Sexe)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sexe");

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("tel");
            });

            modelBuilder.Entity<MembreBureau>(entity =>
            {
                entity.HasKey(e => e.IdMembreBureau);

                entity.ToTable("membre_bureau");

                entity.Property(e => e.IdMembreBureau).HasColumnName("id_membre_bureau");

                entity.Property(e => e.BureauId).HasColumnName("bureau_id");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("datetime")
                    .HasColumnName("date_creation");

                entity.Property(e => e.MembreId).HasColumnName("membre_id");

                entity.Property(e => e.Poste)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("poste");

                entity.HasOne(d => d.Bureau)
                    .WithMany(p => p.MembreBureaus)
                    .HasForeignKey(d => d.BureauId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_membre_bureau_bureau");

                entity.HasOne(d => d.Membre)
                    .WithMany(p => p.MembreBureaus)
                    .HasForeignKey(d => d.MembreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_membre_bureau_membre");
            });

            modelBuilder.Entity<Recette>(entity =>
            {
                entity.HasKey(e => e.IdRecette);

                entity.ToTable("recettes");

                entity.Property(e => e.IdRecette).HasColumnName("id_recette");

                entity.Property(e => e.BureauId).HasColumnName("bureau_id");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("libelle");

                entity.Property(e => e.Montant)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("montant");

                entity.Property(e => e.TypeRecetteId).HasColumnName("type_recette_id");

                entity.HasOne(d => d.Bureau)
                    .WithMany(p => p.Recettes)
                    .HasForeignKey(d => d.BureauId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_recettes_bureau");

                entity.HasOne(d => d.TypeRecette)
                    .WithMany(p => p.Recettes)
                    .HasForeignKey(d => d.TypeRecetteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_recettes_type_recette");
            });

            modelBuilder.Entity<TypeDepense>(entity =>
            {
                entity.HasKey(e => e.IdTypeDepense);

                entity.ToTable("type_depense");

                entity.Property(e => e.IdTypeDepense).HasColumnName("id_type_depense");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("libelle");
            });

            modelBuilder.Entity<TypeRecette>(entity =>
            {
                entity.HasKey(e => e.IdTypeRecette);

                entity.ToTable("type_recette");

                entity.Property(e => e.IdTypeRecette).HasColumnName("id_type_recette");

                entity.Property(e => e.Libelle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("libelle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
