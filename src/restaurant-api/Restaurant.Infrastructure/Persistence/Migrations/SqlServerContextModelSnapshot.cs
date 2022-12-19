﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Restaurant.Infrastructure.Persistence.Context;

#nullable disable

namespace Restaurant.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    partial class SqlServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Core.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Contacts", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Entities.DayOfWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClosesAt")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("OpensAt")
                        .HasColumnType("int");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("DaysOfWork", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Entities.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalTables")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Restaurants", (string)null);
                });

            modelBuilder.Entity("Restaurant.Core.Entities.Contact", b =>
                {
                    b.HasOne("Restaurant.Core.Entities.Restaurant", "Restaurant")
                        .WithMany("Contacts")
                        .HasForeignKey("RestaurantId")
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Restaurant.Core.Entities.DayOfWork", b =>
                {
                    b.HasOne("Restaurant.Core.Entities.Restaurant", "Restaurant")
                        .WithMany("DaysOfWork")
                        .HasForeignKey("RestaurantId")
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Restaurant.Core.Entities.Restaurant", b =>
                {
                    b.OwnsOne("Restaurant.Core.Entities.Restaurant.Address#Restaurant.Core.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("RestaurantId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressCity");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressCountry");

                            b1.Property<string>("FullAddress")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressFullAddress");

                            b1.Property<string>("Neighborhood")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressNeighborhood");

                            b1.Property<int>("Number")
                                .HasColumnType("int")
                                .HasColumnName("AddressNumber");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressPostalCode");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressState");

                            b1.Property<string>("Street")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressStreet");

                            b1.Property<string>("Zone")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("AddressZone");

                            b1.HasKey("RestaurantId");

                            b1.ToTable("Restaurants", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Restaurant.Core.Entities.Restaurant", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("DaysOfWork");
                });
#pragma warning restore 612, 618
        }
    }
}
