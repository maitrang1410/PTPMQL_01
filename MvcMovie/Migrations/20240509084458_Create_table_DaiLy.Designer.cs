﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcMovie.Data;

#nullable disable

namespace MvcMovie.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240509084458_Create_table_DaiLy")]
    partial class Create_table_DaiLy
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("MvcMovie.Models.Hethongphanphoi", b =>
                {
                    b.Property<string>("MaHTPP")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenHTPP")
                        .HasColumnType("TEXT");

                    b.HasKey("MaHTPP");

                    b.ToTable("Hethongphanphois", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("MvcMovie.Models.Person", b =>
                {
                    b.Property<string>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MvcMovie.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("MvcMovie.Models.DaiLy", b =>
                {
                    b.HasBaseType("MvcMovie.Models.Hethongphanphoi");

                    b.Property<string>("DiaChi")
                        .HasColumnType("TEXT");

                    b.Property<string>("DienThoai")
                        .HasColumnType("TEXT");

                    b.Property<string>("MaDaiLy")
                        .HasColumnType("TEXT");

                    b.Property<string>("NguoiDaiDien")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenDaiLy")
                        .HasColumnType("TEXT");

                    b.ToTable("DaiLy");
                });

            modelBuilder.Entity("MvcMovie.Models.Employee", b =>
                {
                    b.HasBaseType("MvcMovie.Models.Person");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("TEXT");

                    b.ToTable("Persons");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("MvcMovie.Models.DaiLy", b =>
                {
                    b.HasOne("MvcMovie.Models.Hethongphanphoi", null)
                        .WithOne()
                        .HasForeignKey("MvcMovie.Models.DaiLy", "MaHTPP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
