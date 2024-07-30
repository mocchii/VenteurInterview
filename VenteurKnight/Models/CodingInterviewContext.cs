using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VenteurKnight.Models;

public partial class CodingInterviewContext : DbContext
{
    public CodingInterviewContext()
    {
    }

    public CodingInterviewContext(DbContextOptions<CodingInterviewContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Knight> Knights { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=dustintsui.database.windows.net;Initial Catalog=CodingInterview;User ID=dustinctsui@gmail.com;Trust Server Certificate=True;Authentication=ActiveDirectoryInteractive");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Knight>(entity =>
        {
            entity.HasIndex(e => e.OperationId, "CK_UNIQUE_GUID").IsUnique();

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Ending)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.OperationId).HasMaxLength(128);
            entity.Property(e => e.Starting)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
