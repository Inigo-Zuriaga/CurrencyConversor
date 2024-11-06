﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebConversor.Models;

#nullable disable

namespace WebConversor.Migrations
{
    [DbContext(typeof(DbContexto))]
    [Migration("20241106173518_migracion-historial")]
    partial class migracionhistorial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebConversor.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Coins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "D�lar Estadounidense",
                            ShortName = "USD",
                            Symbol = "USD"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Euro",
                            ShortName = "EUR",
                            Symbol = "EUR"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Yen Japon�s",
                            ShortName = "YEN",
                            Symbol = "JPY"
                        });
                });

            modelBuilder.Entity("WebConversor.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromCoin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToCoin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExchangeHistory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2004, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FromCoin = "EUR",
                            ToCoin = "USD",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("WebConversor.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "asda@gmail.com",
                            Img = "dd",
                            LastName = "aaa",
                            Name = "D�lar Estadounidense",
                            Password = "ddd"
                        },
                        new
                        {
                            Id = 2,
                            Email = "ggrg2@gmail.com",
                            Img = "ff",
                            LastName = "aaa",
                            Name = "Euro",
                            Password = "fff"
                        });
                });

            modelBuilder.Entity("WebConversor.Models.History", b =>
                {
                    b.HasOne("WebConversor.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
