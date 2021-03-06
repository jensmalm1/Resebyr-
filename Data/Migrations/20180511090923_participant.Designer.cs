﻿// <auto-generated />
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Data.Migrations
{
    [DbContext(typeof(TravelAgencyContext))]
    [Migration("20180511090923_participant")]
    partial class registration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfDebts");

                    b.Property<double>("TotalDebt");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Participant", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("TravelId");

                    b.HasKey("CustomerId", "TravelId");

                    b.HasIndex("TravelId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("Domain.Travel", b =>
                {
                    b.Property<int>("TravelId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Destination");

                    b.Property<double>("Price");

                    b.HasKey("TravelId");

                    b.ToTable("Travels");
                });

            modelBuilder.Entity("Domain.Participant", b =>
                {
                    b.HasOne("Domain.Customer", "Customer")
                        .WithMany("Participants")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.Travel", "Travel")
                        .WithMany("Participants")
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
