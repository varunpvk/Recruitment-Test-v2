﻿// <auto-generated />
using JG.FinTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JG.FinTech.Models.Migrations
{
    [DbContext(typeof(DonorContext))]
    [Migration("20210201014856_GiftAidMigration")]
    partial class GiftAidMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113");

            modelBuilder.Entity("JG.FinTech.Models.DonorDetails", b =>
                {
                    b.Property<string>("DonorID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("DonationAmount");

                    b.Property<double>("GiftAid");

                    b.Property<string>("Name");

                    b.Property<string>("PostCode");

                    b.HasKey("DonorID");

                    b.ToTable("DonorDetails");
                });
#pragma warning restore 612, 618
        }
    }
}