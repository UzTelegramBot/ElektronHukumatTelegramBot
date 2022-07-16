﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220706120605_AbaseForOrganization")]
    partial class AbaseForOrganization
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Domains.BotTextData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Eng")
                        .HasColumnType("text");

                    b.Property<string>("Ru")
                        .HasColumnType("text");

                    b.Property<int>("TypeData")
                        .HasColumnType("integer");

                    b.Property<string>("Uz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BotTextDatas");
                });

            modelBuilder.Entity("Domains.Manager", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Creaetedby")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("LastModifiedby")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<Guid?>("OrganizationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RegionId");

                    b.ToTable("Managers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f14c5b3a-59ae-4bad-a62e-cde394855cec"),
                            Creaetedby = new Guid("00000000-0000-0000-0000-000000000000"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "abdumurodovnodirxon@gmail.com",
                            FirstName = "Nodirxon",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastModifiedby = new Guid("00000000-0000-0000-0000-000000000000"),
                            LastName = "Abdumurotov",
                            Login = "Nodirkhan",
                            Password = "12345",
                            PhoneNumber = "+998900255013",
                            RegionId = new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"),
                            Role = 0
                        });
                });

            modelBuilder.Entity("Domains.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("RegionId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Domains.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("Creaetedby")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("LastModifiedby")
                        .HasColumnType("uuid");

                    b.Property<string>("MessageTitle")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Domains.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("RegionIndex")
                        .HasColumnType("bigint");

                    b.Property<string>("UzName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("936da01f-9abd-4d9d-80c7-02af85c822a8"),
                            RegionIndex = 17L,
                            UzName = "O‘zbekiston Respublikasi"
                        });
                });

            modelBuilder.Entity("Domains.User", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("Language")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("RegionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domains.Manager", b =>
                {
                    b.HasOne("Domains.Organization", "Organization")
                        .WithMany("Managers")
                        .HasForeignKey("OrganizationId");

                    b.HasOne("Domains.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domains.Message", b =>
                {
                    b.HasOne("Domains.Manager", "Manager")
                        .WithMany("Messages")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domains.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domains.Organization", b =>
                {
                    b.HasOne("Domains.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domains.User", b =>
                {
                    b.HasOne("Domains.Region", "Region")
                        .WithMany("Users")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("Domains.Manager", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Domains.Organization", b =>
                {
                    b.Navigation("Managers");
                });

            modelBuilder.Entity("Domains.Region", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}