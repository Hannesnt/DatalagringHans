﻿// <auto-generated />
using System;
using DatalagringHans.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatalagringHans.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230315141649_EntityFix")]
    partial class EntityFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DatalagringHans.Models.Entities.AddressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nchar(20)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("char(6)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CaseEntity", b =>
                {
                    b.Property<Guid>("CaseNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CaseCreated")
                        .HasColumnType("datetime");

                    b.Property<int>("CaseStatusId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseNumber");

                    b.HasIndex("CaseStatusId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CaseStatusEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("char(20)");

                    b.HasKey("Id");

                    b.ToTable("CaseStatuses");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedComment")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CustomerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("char(15)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CaseEntity", b =>
                {
                    b.HasOne("DatalagringHans.Models.Entities.CaseStatusEntity", "CaseStatus")
                        .WithMany("Case")
                        .HasForeignKey("CaseStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatalagringHans.Models.Entities.CustomerEntity", "Customer")
                        .WithMany("Case")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaseStatus");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CommentEntity", b =>
                {
                    b.HasOne("DatalagringHans.Models.Entities.CaseEntity", "Case")
                        .WithMany("Comment")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatalagringHans.Models.Entities.EmployeeEntity", "Employee")
                        .WithMany("Comment")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CustomerEntity", b =>
                {
                    b.HasOne("DatalagringHans.Models.Entities.AddressEntity", "Address")
                        .WithMany("Customer")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.AddressEntity", b =>
                {
                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CaseEntity", b =>
                {
                    b.Navigation("Comment");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CaseStatusEntity", b =>
                {
                    b.Navigation("Case");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.CustomerEntity", b =>
                {
                    b.Navigation("Case");
                });

            modelBuilder.Entity("DatalagringHans.Models.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("Comment");
                });
#pragma warning restore 612, 618
        }
    }
}
