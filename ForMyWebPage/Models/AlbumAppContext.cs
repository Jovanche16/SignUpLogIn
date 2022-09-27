using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ForMyWebPage.Models
{
    public partial class AlbumAppContext : DbContext
    {
        public AlbumAppContext()
        {
        }

        public AlbumAppContext(DbContextOptions<AlbumAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RefreshTokenDto> RefreshTokenDtos { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server = DESKTOP-ACPEQDM\\SQLEXPRESS;Database=AlbumApp; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<RefreshTokenDto>(entity =>
            {
                entity.ToTable("RefreshTokenDTO");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Token).IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<User>()
        .HasKey(c => c.Id);
            modelBuilder.Entity<User>(entity =>
            {

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
