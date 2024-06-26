﻿// <auto-generated />
using System;
using LogisticsSQLInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LogisticsSQLInfrastructure.Migrations
{
    [DbContext(typeof(LogisticsDbContext))]
    [Migration("20240626095317_UpteTrackig")]
    partial class UpteTrackig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.ItemRef", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("ItemRef");
                });

            modelBuilder.Entity("Domain.Models.LogisticsCompany", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PricePerKm")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LogisticsCompanies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2e3ae9f4-6739-48aa-92ec-abfe918d59d6"),
                            Location = "Breda",
                            Name = "MailNL",
                            PricePerKm = 0.5m,
                            Website = "https://tracking.postnl.nl/track-and-trace/"
                        },
                        new
                        {
                            Id = new Guid("b2e5fd60-ce5c-4829-9810-0cdd4ad8ba8e"),
                            Location = "Tilburg",
                            Name = "BHL",
                            PricePerKm = 0.4m,
                            Website = "https://www.dhlexpress.nl/nl/consument/track-trace"
                        },
                        new
                        {
                            Id = new Guid("c90c8343-1c7d-4b4f-9617-9f295d04feb0"),
                            Location = "Eindhoven",
                            Name = "FredEx",
                            PricePerKm = 0.6m,
                            Website = "https://www.fedex.com/nl-nl/tracking.html"
                        });
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("LogisticsCompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LogisticsCompanyId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Domain.Models.Tracking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Trackings");
                });

            modelBuilder.Entity("Domain.Models.ItemRef", b =>
                {
                    b.HasOne("Domain.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.HasOne("Domain.Models.LogisticsCompany", "LogisticsCompany")
                        .WithMany()
                        .HasForeignKey("LogisticsCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogisticsCompany");
                });

            modelBuilder.Entity("Domain.Models.Tracking", b =>
                {
                    b.HasOne("Domain.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}