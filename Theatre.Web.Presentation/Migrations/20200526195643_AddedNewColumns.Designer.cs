﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Theatre.Web.Core.Enums;
using Theatre.Web.Infrastructure.Data.Context;

namespace Theatre.Web.Presentation.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200526195643_AddedNewColumns")]
    partial class AddedNewColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.Reservation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)");

                    b.Property<Guid>("SpectatorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TheatrePlayId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SpectatorId");

                    b.HasIndex("TheatrePlayId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.TheatrePlay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("TheatrePlays");
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("ReservationId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.Property<int>("Section")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(256)");

                    b.Property<byte>("Type")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Username")
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<byte>("Type");
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.Administrator", b =>
                {
                    b.HasBaseType("Theatre.Web.Core.Models.Entities.User");

                    b.Property<DateTime>("LastLogged")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(256)");

                    b.HasDiscriminator().HasValue((byte)1);
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.Spectator", b =>
                {
                    b.HasBaseType("Theatre.Web.Core.Models.Entities.User");

                    b.Property<string>("Address")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256)");

                    b.HasDiscriminator().HasValue((byte)0);
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.Reservation", b =>
                {
                    b.HasOne("Theatre.Web.Core.Models.Entities.Spectator", "Spectator")
                        .WithMany("Reservations")
                        .HasForeignKey("SpectatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Theatre.Web.Core.Models.Entities.TheatrePlay", "TheatrePlay")
                        .WithMany("Reservations")
                        .HasForeignKey("TheatrePlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Theatre.Web.Core.Models.Entities.Ticket", b =>
                {
                    b.HasOne("Theatre.Web.Core.Models.Entities.Reservation", "Reservation")
                        .WithMany("Tickets")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
