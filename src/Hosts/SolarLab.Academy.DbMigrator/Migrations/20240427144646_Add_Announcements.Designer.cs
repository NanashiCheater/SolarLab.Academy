﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SolarLab.Academy.DbMigrator.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    [Migration("20240427144646_Add_Announcements")]
    partial class Add_Announcements
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SolarLab.Academy.Domain.Announcements.Entity.Announcement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<int>("Cost")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2550)
                        .HasColumnType("character varying(2550)");

                    b.Property<Guid[]>("Images")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Announcements", (string)null);
                });

            modelBuilder.Entity("SolarLab.Academy.Domain.Categories.Entity.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("10946739-9f9e-4958-81f8-66c52dba6bea"),
                            Name = "Недвижимость"
                        },
                        new
                        {
                            Id = new Guid("ec698cde-0525-42c5-8b10-37179adb3540"),
                            Name = "Дома",
                            ParentId = new Guid("10946739-9f9e-4958-81f8-66c52dba6bea")
                        },
                        new
                        {
                            Id = new Guid("6737fa84-51b2-4e20-8603-02be4dcdc9ac"),
                            Name = "Квартиры",
                            ParentId = new Guid("10946739-9f9e-4958-81f8-66c52dba6bea")
                        },
                        new
                        {
                            Id = new Guid("2e51c519-79c5-4468-a1ee-478ca0c2b8ff"),
                            Name = "Транспорт"
                        },
                        new
                        {
                            Id = new Guid("bb805e10-d6c0-4ce5-b8ae-2d2554b516b9"),
                            Name = "Велосипеды",
                            ParentId = new Guid("2e51c519-79c5-4468-a1ee-478ca0c2b8ff")
                        },
                        new
                        {
                            Id = new Guid("cb66c03d-fcf8-4e9e-9a37-bfe63c07d5fc"),
                            Name = "Автомобили",
                            ParentId = new Guid("2e51c519-79c5-4468-a1ee-478ca0c2b8ff")
                        },
                        new
                        {
                            Id = new Guid("3aad0e0f-c16e-47e7-a223-b638cc5a48f6"),
                            Name = "Одежда"
                        },
                        new
                        {
                            Id = new Guid("9797b747-3432-4f37-8725-d97a3978ab49"),
                            Name = "Одежда для взрослых",
                            ParentId = new Guid("3aad0e0f-c16e-47e7-a223-b638cc5a48f6")
                        },
                        new
                        {
                            Id = new Guid("3b0beb72-e73b-473d-9804-720e656cb243"),
                            Name = "Детская одежда",
                            ParentId = new Guid("3aad0e0f-c16e-47e7-a223-b638cc5a48f6")
                        });
                });

            modelBuilder.Entity("SolarLab.Academy.Domain.Files.Entity.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("SolarLab.Academy.Domain.Users.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SolarLab.Academy.Domain.Announcements.Entity.Announcement", b =>
                {
                    b.HasOne("SolarLab.Academy.Domain.Categories.Entity.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SolarLab.Academy.Domain.Users.Entity.User", "Owner")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("SolarLab.Academy.Domain.Categories.Entity.Category", b =>
                {
                    b.HasOne("SolarLab.Academy.Domain.Categories.Entity.Category", "Parent")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("SolarLab.Academy.Domain.Categories.Entity.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
