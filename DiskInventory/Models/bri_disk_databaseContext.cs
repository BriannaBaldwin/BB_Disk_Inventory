using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class bri_disk_databaseContext : DbContext
    {
        public bri_disk_databaseContext()
        {
        }

        public bri_disk_databaseContext(DbContextOptions<bri_disk_databaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<DiskArtist> DiskArtists { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=bri_disk_database;Trusted_Connection=True;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artist");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("artist_name");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__artist__artist_t__32E0915F");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("artist_type");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.BorrowerFname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("borrower_fname")
                    .IsFixedLength(true);

                entity.Property(e => e.BorrowerLname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("borrower_lname")
                    .IsFixedLength(true);

                entity.Property(e => e.BorrowerPhoneNum)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("borrower_phone_num");
            });

            modelBuilder.Entity<DiskArtist>(entity =>
            {
                entity.ToTable("disk_artist");

                entity.Property(e => e.DiskArtistId).HasColumnName("disk_artist_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiskArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_arti__artis__398D8EEE");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.DiskArtists)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__disk_arti__media__3A81B327");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MediaType>(entity =>
            {
                entity.ToTable("media_type");

                entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.HasKey(e => e.MediaId)
                    .HasName("PK__media__D0A840F4A9729904");

                entity.ToTable("media");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.MediaName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("media_name")
                    .IsFixedLength(true);

                entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media__genre_id__2E1BDC42");

                entity.HasOne(d => d.MediaType)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.MediaTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media__media_typ__2C3393D0");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__media__status_id__2D27B809");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("rental");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("date")
                    .HasColumnName("borrowed_date");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.MediaId).HasColumnName("media_id");

                entity.Property(e => e.ReturnedDate)
                    .HasColumnType("date")
                    .HasColumnName("returned_date");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rental__borrower__36B12243");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rental__media_id__35BCFE0A");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.ArtistId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("artist_id");

                entity.Property(e => e.ArtistName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("artist_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
