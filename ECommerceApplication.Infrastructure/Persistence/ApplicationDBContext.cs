using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApplication.Infrastructure.Persistence;

public partial class ApplicationDBContext : DbContext
{
    public ApplicationDBContext()
    {
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appuser> Appusers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Productdetail> Productdetails { get; set; }

    public virtual DbSet<Reviewdetail> Reviewdetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=::1;Port=5432;Database=Application;Username=postgres;Password=your_new_password;Pooling=True;SslMode=Prefer;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appuser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("appuser");

            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Emailaddress)
                .HasColumnType("character varying")
                .HasColumnName("emailaddress");
            entity.Property(e => e.Passwordhash)
                .HasColumnType("character varying")
                .HasColumnName("passwordhash");
            entity.Property(e => e.Role)
                .HasColumnType("character varying")
                .HasColumnName("role");
            entity.Property(e => e.Userid)
                .HasColumnType("character varying")
                .HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasColumnType("character varying")
                .HasColumnName("username");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderdetails_pkey");

            entity.ToTable("orderdetails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Expecteddelivery)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expecteddelivery");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Ordereddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ordereddate");
            entity.Property(e => e.Paymentstatus)
                .HasColumnType("character varying")
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Shippingaddress)
                .HasColumnType("character varying")
                .HasColumnName("shippingaddress");
            entity.Property(e => e.Userid)
                .HasColumnType("character varying")
                .HasColumnName("userid");
        });

        modelBuilder.Entity<Productdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productdetails_pkey");

            entity.ToTable("productdetails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Averagerating).HasColumnName("averagerating");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Description)
                .HasColumnType("character varying")
                .HasColumnName("description");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantityavailable).HasColumnName("quantityavailable");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.Productdetails)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("productdetails_categoryid_fkey");
        });

        modelBuilder.Entity<Reviewdetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reviewdetails_pkey");

            entity.ToTable("reviewdetails");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnType("character varying")
                .HasColumnName("comment");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Postedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("postedat");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Ratings).HasColumnName("ratings");
            entity.Property(e => e.Userid)
                .HasColumnType("character varying")
                .HasColumnName("userid");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviewdetails)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviewdetails_productid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
