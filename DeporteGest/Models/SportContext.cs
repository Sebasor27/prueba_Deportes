using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DeporteGest.Models;

public partial class SportContext : DbContext
{
    public SportContext()
    {
    }

    public SportContext(DbContextOptions<SportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=Sport;user=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.EventoId).HasName("PRIMARY");

            entity.Property(e => e.EventoId)
                .HasColumnType("int(11)")
                .HasColumnName("evento_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.Ubicacion)
                .HasMaxLength(255)
                .HasColumnName("ubicacion");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PRIMARY");

            entity.HasIndex(e => e.EventoId, "evento_id");

            entity.HasIndex(e => e.ParticipanteId, "participante_id");

            entity.Property(e => e.InscripcionId)
                .HasColumnType("int(11)")
                .HasColumnName("inscripcion_id");
            entity.Property(e => e.EventoId)
                .HasColumnType("int(11)")
                .HasColumnName("evento_id");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_inscripcion");
            entity.Property(e => e.ParticipanteId)
                .HasColumnType("int(11)")
                .HasColumnName("participante_id");

            entity.HasOne(d => d.Evento).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.EventoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Inscripciones_ibfk_1");

            entity.HasOne(d => d.Participante).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.ParticipanteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Inscripciones_ibfk_2");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.ParticipanteId).HasName("PRIMARY");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.ParticipanteId)
                .HasColumnType("int(11)")
                .HasColumnName("participante_id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
