using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Project_PRN212_FALL24.Models;

public partial class ProjectPrn212Fall24Context : DbContext
{
    public ProjectPrn212Fall24Context()
    {
    }

    public ProjectPrn212Fall24Context(DbContextOptions<ProjectPrn212Fall24Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<SettingName> SettingNames { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC27BE527F29");

            entity.ToTable("Account");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(256);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC27FE3F5BBB");

            entity.ToTable("Book");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author).HasMaxLength(64).HasDefaultValue("Unknown");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Image).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(512);

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.CreateBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_CreateBy");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Type");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC27F6CDADFE");

            entity.ToTable("History");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idaccount).HasColumnName("IDAccount");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");

            entity.HasOne(d => d.IdaccountNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.Idaccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_IDAccount");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_IDBook");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Setting__3214EC278AA36B6F");

            entity.ToTable("Setting");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Settings)
                .HasForeignKey(d => d.Type)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Setting_Type");
        });

        modelBuilder.Entity<SettingName>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SettingN__3214EC276441FB5D");

            entity.ToTable("SettingName");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
