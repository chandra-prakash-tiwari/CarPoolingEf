﻿// <auto-generated />
using System;
using CarPoolingEf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarPoolingEf.Migrations
{
    [DbContext(typeof(CarPoolingEfContext))]
    partial class CarPoolingEfContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarPoolingEf.Models.Booking", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BookerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RideId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TravelDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("TravellingDistance")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BookerId");

                    b.HasIndex("RideId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CarPoolingEf.Models.Car", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoofSeat")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("CarPoolingEf.Models.Ride", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AvailableSeats")
                        .HasColumnType("int");

                    b.Property<string>("CarId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("From")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("RatePerKM")
                        .HasColumnType("real");

                    b.Property<DateTime>("RideDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TotalDistance")
                        .HasColumnType("real");

                    b.Property<DateTime>("TravelDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ViaPoints")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("CarPoolingEf.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrivingLicence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarPoolingEf.Models.Booking", b =>
                {
                    b.HasOne("CarPoolingEf.Models.User", "Booker")
                        .WithMany("Bookings")
                        .HasForeignKey("BookerId");

                    b.HasOne("CarPoolingEf.Models.Ride", "Ride")
                        .WithMany("Bookings")
                        .HasForeignKey("RideId");
                });

            modelBuilder.Entity("CarPoolingEf.Models.Car", b =>
                {
                    b.HasOne("CarPoolingEf.Models.User", "Owner")
                        .WithMany("Cars")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("CarPoolingEf.Models.Ride", b =>
                {
                    b.HasOne("CarPoolingEf.Models.Car", "Car")
                        .WithMany("Rides")
                        .HasForeignKey("CarId");

                    b.HasOne("CarPoolingEf.Models.User", "Owner")
                        .WithMany("Rides")
                        .HasForeignKey("OwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
