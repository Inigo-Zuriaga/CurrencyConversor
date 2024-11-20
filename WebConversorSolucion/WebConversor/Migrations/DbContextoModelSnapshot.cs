﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebConversor.Models;

#nullable disable

namespace WebConversor.Migrations
{
    [DbContext(typeof(DbContexto))]
    partial class DbContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<double>("FromAmount")
                        .HasColumnType("float");

                    b.Property<string>("FromCoin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ToAmount")
                        .HasColumnType("float");

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
                            FromAmount = 76.0,
                            FromCoin = "EUR",
                            ToAmount = 2.0,
                            ToCoin = "USD",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2024, 11, 20, 17, 57, 33, 120, DateTimeKind.Local).AddTicks(7329),
                            FromAmount = 20.0,
                            FromCoin = "USD",
                            ToAmount = 16.0,
                            ToCoin = "EUR",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2007, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FromAmount = 45.0,
                            FromCoin = "USD",
                            ToAmount = 120.0,
                            ToCoin = "PLN",
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

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Img")
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
                            LastName = "Gomez",
                            Name = "Julian",
                            Password = "ddd"
                        },
                        new
                        {
                            Id = 2,
                            Email = "ggrg2@gmail.com",
                            Img = "ff",
                            LastName = "Garcia",
                            Name = "Manuel",
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
