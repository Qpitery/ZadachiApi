using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ZadachiApi.DB;

public partial class User07Context : DbContext
{
    public User07Context()
    {
    }

    public User07Context(DbContextOptions<User07Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Zadachi> Zadachis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;database=user07;user=user07;password=24367;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Idstatus);

            entity.ToTable("Status");

            entity.Property(e => e.Idstatus).HasColumnName("IDStatus");
            entity.Property(e => e.NameStatus)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Zadachi>(entity =>
        {
            entity.HasKey(e => e.Idzadachi);

            entity.ToTable("Zadachi");

            entity.Property(e => e.Idzadachi).HasColumnName("IDZadachi");
            entity.Property(e => e.Idstatus).HasColumnName("IDStatus");
            entity.Property(e => e.NameZadachi)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.OpisanieZadachi)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.Zadachis)
                .HasForeignKey(d => d.Idstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zadachi_Status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
