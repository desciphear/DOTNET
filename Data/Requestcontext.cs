using System;
using System.Collections.Generic;
using Loyal.Models;
using Microsoft.EntityFrameworkCore;

namespace Loyal.Data;

public partial class Requestcontext : DbContext
{
    public Requestcontext()
    {
    }

    public Requestcontext(DbContextOptions<Requestcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Request> Requests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KRJ668D\\SQLEXPRESS;Database=Creddata;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_UserID");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Code)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Offerdescription).IsUnicode(false);
            entity.Property(e => e.Offername)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Reason).IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
