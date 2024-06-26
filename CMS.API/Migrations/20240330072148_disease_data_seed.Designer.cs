﻿// <auto-generated />
using System;
using CMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMS.API.Migrations
{
    [DbContext(typeof(CMSDbContext))]
    [Migration("20240330072148_disease_data_seed")]
    partial class disease_data_seed
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CMS.API.Model.Domain.Consultation", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ConsultationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ConsultationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId", "DoctorId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Diseases");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DiseaseName = "Flu"
                        },
                        new
                        {
                            Id = 2,
                            DiseaseName = "Cold"
                        },
                        new
                        {
                            Id = 3,
                            DiseaseName = "Fever"
                        },
                        new
                        {
                            Id = 4,
                            DiseaseName = "Headache"
                        },
                        new
                        {
                            Id = 5,
                            DiseaseName = "Sore throat"
                        },
                        new
                        {
                            Id = 6,
                            DiseaseName = "Stomach flu"
                        },
                        new
                        {
                            Id = 7,
                            DiseaseName = "Pneumonia"
                        },
                        new
                        {
                            Id = 8,
                            DiseaseName = "Sinusitis"
                        },
                        new
                        {
                            Id = 9,
                            DiseaseName = "Bronchitis"
                        },
                        new
                        {
                            Id = 10,
                            DiseaseName = "Allergies"
                        });
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VisitDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VisitTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("DiseasePatient", b =>
                {
                    b.Property<int>("DiseasesId")
                        .HasColumnType("int");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("DiseasesId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("DiseasePatient");
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Consultation", b =>
                {
                    b.HasOne("CMS.API.Model.Domain.Doctor", "Doctor")
                        .WithMany("Consultations")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CMS.API.Model.Domain.Patient", "Patient")
                        .WithMany("Consultations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Visit", b =>
                {
                    b.HasOne("CMS.API.Model.Domain.Patient", "Patient")
                        .WithMany("Visits")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DiseasePatient", b =>
                {
                    b.HasOne("CMS.API.Model.Domain.Disease", null)
                        .WithMany()
                        .HasForeignKey("DiseasesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CMS.API.Model.Domain.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Doctor", b =>
                {
                    b.Navigation("Consultations");
                });

            modelBuilder.Entity("CMS.API.Model.Domain.Patient", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Visits");
                });
#pragma warning restore 612, 618
        }
    }
}
