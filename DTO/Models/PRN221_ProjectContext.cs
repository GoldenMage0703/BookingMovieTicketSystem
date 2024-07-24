using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DTO.Models
{
    public partial class PRN221_ProjectContext : DbContext
    {
        public PRN221_ProjectContext()
        {
        }

        public PRN221_ProjectContext(DbContextOptions<PRN221_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieGenre> MovieGenres { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Seat> Seats { get; set; } = null!;
        public virtual DbSet<Showtime> Showtimes { get; set; } = null!;
        public virtual DbSet<Theater> Theaters { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=TUANNM\\SQLEXPRESS; database=PRN221_Project;user=sa;password=123456;Integrated Security=true;TrustServerCertificate=Yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.BookingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("booking_date");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");

                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("customer_phone");

                entity.Property(e => e.PaymentId).HasColumnName("paymentID");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_Booking_Payment");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("FK__Booking__seat_id__03F0984C");

                entity.HasOne(d => d.Showtime)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ShowtimeId)
                    .HasConstraintName("FK__Booking__showtim__02FC7413");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.DirectorName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("director_name");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Poster)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("poster");

                entity.Property(e => e.Rating)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("rating");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StoryLine)
                    .HasColumnType("text")
                    .HasColumnName("story_line");

                entity.Property(e => e.Time)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<MovieGenre>(entity =>
            {
                entity.HasKey(e => new { e.MovieId, e.GenreId });

                entity.ToTable("movie_genre");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_genre_genre");

                entity.HasOne(d => d.MovieNavigation)
                    .WithMany(p => p.MovieGenres)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movie_genre_Movie");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("payment_method");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_amount");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_Payment_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("Seat");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.Property(e => e.Available)
                    .HasColumnName("available")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.Property(e => e.RowNum)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("row_num");

                entity.Property(e => e.SeatNum).HasColumnName("seat_num");

                entity.Property(e => e.TheaterId).HasColumnName("theater_id");

                entity.HasOne(d => d.Theater)
                    .WithMany(p => p.Seats)
                    .HasForeignKey(d => d.TheaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Seat__theater_id__00200768");
            });

            modelBuilder.Entity<Showtime>(entity =>
            {
                entity.ToTable("Showtime");

                entity.Property(e => e.ShowtimeId).HasColumnName("showtime_id");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.TheaterId).HasColumnName("theater_id");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Showtime__movie___7B5B524B");

                entity.HasOne(d => d.Theater)
                    .WithMany(p => p.Showtimes)
                    .HasForeignKey(d => d.TheaterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Showtime__theate__7C4F7684");
            });

            modelBuilder.Entity<Theater>(entity =>
            {
                entity.HasKey(e => e.TheatreId)
                    .HasName("PK__Theater__0D49C9B4D210505F");

                entity.ToTable("Theater");

                entity.Property(e => e.TheatreId).HasColumnName("theatre_id");

                entity.Property(e => e.Capacity).HasColumnName("capacity");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registration_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_User_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
