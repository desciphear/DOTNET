using System;
using System.Collections.Generic;
using Loyal.Models;
using Microsoft.EntityFrameworkCore;

namespace Loyal.Data;

public partial class AdminContext : DbContext
{
    public AdminContext()
    {
    }

    public AdminContext(DbContextOptions<AdminContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminCred> AdminCreds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KRJ668D\\SQLEXPRESS;Database=Creddata;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminCred>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AdminCred");

            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
