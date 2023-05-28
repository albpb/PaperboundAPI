using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaperboundAPI.Models;

public partial class PaperboundContext : DbContext
{
    public PaperboundContext()
    {
    }

    public PaperboundContext(DbContextOptions<PaperboundContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comanda> Comandes { get; set; }

    public virtual DbSet<Genere> Generes { get; set; }

    public virtual DbSet<Llibre> Llibres { get; set; }

    public virtual DbSet<PuntRecollida> PuntsRecollida { get; set; }

    public virtual DbSet<Qr> Qrs { get; set; }

    public virtual DbSet<Qrcode> Qrcodes { get; set; }

    public virtual DbSet<Usuari> Usuaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = 51.83.58.11, 1433; Initial Catalog = Paperbound;User ID = sa; Password = 123456aA; Encrypt = False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comanda>(entity =>
        {
            entity.HasKey(e => e.Idcomanda).HasName("PK__Comandes__62DF6CBE7E825E07");

            entity.Property(e => e.Idcomanda).HasColumnName("idcomanda");
            entity.Property(e => e.IdLlibre).HasColumnName("idLlibre");
            entity.Property(e => e.Idrecollida).HasColumnName("idrecollida");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.IdrecollidaNavigation).WithMany(p => p.Comandes)
                .HasForeignKey(d => d.Idrecollida)
                .HasConstraintName("FK__Comandes__idreco__5EBF139D");

            entity.HasOne(d => d.User).WithMany(p => p.Comandes)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("FK__Comandes__userid__60A75C0F");
        });

        modelBuilder.Entity<Genere>(entity =>
        {
            entity.HasKey(e => e.IdGenere).HasName("PK__Generes__85223DD5F7B8DA95");

            entity.Property(e => e.IdGenere).HasColumnName("idGenere");
            entity.Property(e => e.ImgGenere)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasColumnName("imgGenere");
            entity.Property(e => e.NomGenere)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nomGenere");
        });

        modelBuilder.Entity<Llibre>(entity =>
        {
            entity.HasKey(e => e.IdLlibre).HasName("PK__Llibres__3606AB7AFF5B849F");

            entity.Property(e => e.IdLlibre).HasColumnName("idLlibre");
            entity.Property(e => e.Descompte).HasColumnName("descompte");
            entity.Property(e => e.IdGenere).HasColumnName("id_genere");
            entity.Property(e => e.NomAutor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nom_autor");
            entity.Property(e => e.PreuTotal)
                .HasColumnType("decimal(20, 2)")
                .HasColumnName("preuTotal");
            entity.Property(e => e.Sinopsi)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("sinopsi");
            entity.Property(e => e.Titol)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titol");
            entity.Property(e => e.UrlImatge)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlImatge");

            entity.HasOne(d => d.IdGenereNavigation).WithMany(p => p.Llibres)
                .HasForeignKey(d => d.IdGenere)
                .HasConstraintName("FK__Llibres__id_gene__619B8048");

            entity.HasMany(d => d.IdComandes).WithMany(p => p.IdLlibres)
                .UsingEntity<Dictionary<string, object>>(
                    "LlibresUsuari",
                    r => r.HasOne<Comanda>().WithMany()
                        .HasForeignKey("IdComandes")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__LlibresUs__idCom__693CA210"),
                    l => l.HasOne<Llibre>().WithMany()
                        .HasForeignKey("IdLlibres")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__LlibresUs__idLLi__68487DD7"),
                    j =>
                    {
                        j.HasKey("IdLlibres", "IdComandes").HasName("llibresusuaris_pk");
                        j.ToTable("LlibresUsuaris");
                        j.IndexerProperty<int>("IdLlibres").HasColumnName("idLLibres");
                        j.IndexerProperty<int>("IdComandes").HasColumnName("idComandes");
                    });
        });

        modelBuilder.Entity<PuntRecollida>(entity =>
        {
            entity.HasKey(e => e.IdPuntRecollida).HasName("PK__PuntsRec__B3D3F0D63BD13C29");

            entity.Property(e => e.IdPuntRecollida).HasColumnName("idPuntRecollida");
            entity.Property(e => e.Latitud)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Qr>(entity =>
        {
            entity.HasKey(e => e.IdQr).HasName("pk_qr");

            entity.ToTable("qr");

            entity.Property(e => e.IdQr).HasColumnName("idQr");
            entity.Property(e => e.Code)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.IdLlibre).HasColumnName("idLlibre");
            entity.Property(e => e.IdPuntRecollida).HasColumnName("idPuntRecollida");

            entity.HasOne(d => d.IdLlibreNavigation).WithMany(p => p.Qrs)
                .HasForeignKey(d => d.IdLlibre)
                .HasConstraintName("fk_qrbook");

            entity.HasOne(d => d.IdPuntRecollidaNavigation).WithMany(p => p.Qrs)
                .HasForeignKey(d => d.IdPuntRecollida)
                .HasConstraintName("fk_qrpointstore");
        });

        modelBuilder.Entity<Qrcode>(entity =>
        {
            entity.HasKey(e => e.IdQr).HasName("PK__QRCode__9DB82086FB4B8970");

            entity.ToTable("QRCode");

            entity.HasIndex(e => e.IdComanda, "UQ_comandes_idcom").IsUnique();

            entity.Property(e => e.IdQr)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("idQR");
            entity.Property(e => e.IdComanda).HasColumnName("idComanda");

            entity.HasOne(d => d.IdComandaNavigation).WithOne(p => p.Qrcode)
                .HasForeignKey<Qrcode>(d => d.IdComanda)
                .HasConstraintName("FK_ComandesIdCom");
        });

        modelBuilder.Entity<Usuari>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Usuaris__3717C982BFB34F66");

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("salt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
