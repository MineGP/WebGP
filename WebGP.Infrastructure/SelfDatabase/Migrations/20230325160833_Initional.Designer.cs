﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebGP.Infrastructure.SelfDatabase;

#nullable disable

namespace WebGP.Infrastructure.SelfDatabase.Migrations
{
    [DbContext(typeof(SelfDbContext))]
    [Migration("20230325160833_Initional")]
    partial class Initional
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("WebGP.Domain.SelfEntities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int")
                        .HasColumnName("created_by_id");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("note");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("registration_time");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("role");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("token");

                    b.HasKey("Id");

                    b.HasAlternateKey("Token");

                    b.HasIndex("CreatedById");

                    b.ToTable("admins", (string)null);
                });

            modelBuilder.Entity("WebGP.Domain.SelfEntities.Admin", b =>
                {
                    b.HasOne("WebGP.Domain.SelfEntities.Admin", "CreatedBy")
                        .WithMany("Created")
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("WebGP.Domain.SelfEntities.Admin", b =>
                {
                    b.Navigation("Created");
                });
#pragma warning restore 612, 618
        }
    }
}
