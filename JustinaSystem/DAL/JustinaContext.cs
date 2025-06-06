﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using JustinaSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace JustinaSystem.DAL;

internal partial class JustinaContext : DbContext
{
    public JustinaContext(DbContextOptions<JustinaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dog> Dogs { get; set; }

    public virtual DbSet<Groomer> Groomers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<vaccination> vaccinations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.appointment_id).HasName("PK__Appointm__A50828FC429832AD");

            entity.HasOne(d => d.customer).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__custo__1D114BD1");

            entity.HasOne(d => d.dog).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__dog_i__1E05700A");

            entity.HasOne(d => d.groomer).WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__groom__1EF99443");

            entity.HasMany(d => d.services).WithMany(p => p.appointments)
                .UsingEntity<Dictionary<string, object>>(
                    "AppointmentService",
                    r => r.HasOne<Service>().WithMany()
                        .HasForeignKey("service_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Appointme__servi__24B26D99"),
                    l => l.HasOne<Appointment>().WithMany()
                        .HasForeignKey("appointment_id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Appointme__appoi__23BE4960"),
                    j =>
                    {
                        j.HasKey("appointment_id", "service_id").HasName("PK__Appointm__46E8F376650E89DE");
                        j.ToTable("AppointmentServices");
                    });
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.customer_id).HasName("PK__Customer__CD65CB85EF24DFE5");

            entity.Property(e => e.enrollment_date).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Dog>(entity =>
        {
            entity.HasKey(e => e.dog_id).HasName("PK__Dogs__6804107306D6F539");

            entity.HasOne(d => d.customer).WithMany(p => p.Dogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dogs__customer_i__1758727B");
        });

        modelBuilder.Entity<Groomer>(entity =>
        {
            entity.HasKey(e => e.groomer_id).HasName("PK__Groomers__7BCAEDA8D314120C");

            entity.Property(e => e.enrollment_date).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.payment_id).HasName("PK__Payments__ED1FC9EA40FA2236");

            entity.HasOne(d => d.service).WithMany(p => p.Payments).HasConstraintName("FK__Payments__servic__278EDA44");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.service_id).HasName("PK__Services__3E0DB8AFD3416A69");
        });

        modelBuilder.Entity<vaccination>(entity =>
        {
            entity.HasKey(e => e.vaccine_id).HasName("PK__vaccinat__B593EFB3CDF22183");

            entity.HasOne(d => d.dog).WithMany(p => p.vaccinations).HasConstraintName("FK__vaccinati__dog_i__1A34DF26");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}