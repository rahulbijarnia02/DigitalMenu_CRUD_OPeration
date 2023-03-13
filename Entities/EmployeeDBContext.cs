using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DIgitalMenu.Entities;

public partial class EmployeeDBContext : DbContext
{
    public EmployeeDBContext()
    {
    }

    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddMenuDetail> AddMenuDetails { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<UserOffice> UserOffices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-8LV5BMKE;Database=70-461;Trusted_Connection=True;Trustservercertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddMenuDetail>(entity =>
        {
            entity.ToTable("AddMenuDetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.DishName).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Quantity).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.ToTable("Employe");

            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__office__3213E83FBB8B045C");

            entity.ToTable("office");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OfficeAddress)
                .HasMaxLength(100)
                .HasColumnName("office_Address");
            entity.Property(e => e.OfficeCountry)
                .HasMaxLength(30)
                .HasColumnName("office_Country");
            entity.Property(e => e.OfficeName)
                .HasMaxLength(50)
                .HasColumnName("office_Name");
            entity.Property(e => e.OfficePinCode)
                .HasMaxLength(20)
                .HasColumnName("office_PinCode");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Offices)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__office__CreatedB__6A30C649");
        });

        modelBuilder.Entity<UserOffice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__userOffi__3213E83F2054706F");

            entity.ToTable("userOffice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OfficeId).HasColumnName("office_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Office).WithMany(p => p.UserOffices)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("FK__userOffic__offic__6D0D32F4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
