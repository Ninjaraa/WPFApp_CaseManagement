﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WPFApp_CaseManagement.Context;

#nullable disable

namespace WPFApp_CaseManagement.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230318103746_InitNewDb")]
    partial class InitNewDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.AccountEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AccountNo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("char(13)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.CaseDescriptionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime?>("CommentAdded")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("CaseDescriptions");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.CaseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CaseCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("SLAId")
                        .HasColumnType("int");

                    b.Property<int?>("Severity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SLAId")
                        .IsUnique()
                        .HasFilter("[SLAId] IS NOT NULL");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("EmployeeNo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.SLAEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgreementResolution")
                        .HasColumnType("int");

                    b.Property<int?>("AgreementResponse")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResolutionTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ResponseTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SLAs");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.CaseDescriptionEntity", b =>
                {
                    b.HasOne("WPFApp_CaseManagement.Models.Entities.CaseEntity", "Case")
                        .WithMany("CaseDescriptions")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.CaseEntity", b =>
                {
                    b.HasOne("WPFApp_CaseManagement.Models.Entities.AccountEntity", "Account")
                        .WithMany("Cases")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WPFApp_CaseManagement.Models.Entities.SLAEntity", "SLA")
                        .WithOne("Case")
                        .HasForeignKey("WPFApp_CaseManagement.Models.Entities.CaseEntity", "SLAId");

                    b.Navigation("Account");

                    b.Navigation("SLA");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.AccountEntity", b =>
                {
                    b.Navigation("Cases");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.CaseEntity", b =>
                {
                    b.Navigation("CaseDescriptions");
                });

            modelBuilder.Entity("WPFApp_CaseManagement.Models.Entities.SLAEntity", b =>
                {
                    b.Navigation("Case");
                });
#pragma warning restore 612, 618
        }
    }
}
