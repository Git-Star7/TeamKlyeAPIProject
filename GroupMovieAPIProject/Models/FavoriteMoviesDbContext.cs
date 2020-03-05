using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GroupMovieAPIProject.Models
{
    public partial class FavoriteMoviesDbContext : DbContext
    {
        public FavoriteMoviesDbContext()
        {
        }

        public FavoriteMoviesDbContext(DbContextOptions<FavoriteMoviesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Movies> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FavoriteMoviesDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movies>(entity =>
            {
                entity.Property(e => e.Actors).HasMaxLength(150);

                entity.Property(e => e.Director).HasMaxLength(30);

                entity.Property(e => e.Rated).HasMaxLength(15);

                entity.Property(e => e.Released).HasMaxLength(30);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Year).HasMaxLength(4);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
