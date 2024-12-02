using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OTPService.Example.Database.AppDbContextModels;

namespace OTPService.Example.Api.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Otp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OTP__3214EC0793CE7326");

            entity.ToTable("OTP");

            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.ExpireTime).HasColumnType("datetime");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(8)
                .HasColumnName("OTPCode");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Otps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTP_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC072612EDD9");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D10534122F363F").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
