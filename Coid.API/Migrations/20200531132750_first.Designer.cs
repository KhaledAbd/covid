﻿// <auto-generated />
using System;
using Coid.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Coid.API.Migrations
{
    [DbContext(typeof(ContextData))]
    [Migration("20200531132750_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("Coid.API.Models.Coron", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Active");

                    b.Property<string>("Confirmed");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Deaths");

                    b.Property<int>("Recovered");

                    b.HasKey("id");

                    b.ToTable("Corons");
                });

            modelBuilder.Entity("Coid.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsCertificated");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("Coid.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("Gender");

                    b.Property<string>("KnownAs");

                    b.Property<string>("Username");

                    b.Property<byte[]>("passwordHash");

                    b.Property<byte[]>("passwordSalt");

                    b.Property<int>("typePerson");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Coid.API.Models.Vidoe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Vidoes");
                });

            modelBuilder.Entity("Coid.API.Models.Photo", b =>
                {
                    b.HasOne("Coid.API.Models.User", "UserNavigation")
                        .WithMany("Photos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Coid.API.Models.Vidoe", b =>
                {
                    b.HasOne("Coid.API.Models.User", "UserNavigation")
                        .WithMany("Vidoes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
