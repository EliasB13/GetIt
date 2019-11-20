﻿// <auto-generated />
using System;
using APZ_BACKEND.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APZ_BACKEND.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.BusinessUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BusinessUsers");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BusinessUserId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeesRoleId")
                        .HasColumnType("int");

                    b.Property<int?>("PrivateUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUserId");

                    b.HasIndex("EmployeesRoleId");

                    b.HasIndex("PrivateUserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.EmployeeRoleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeesRoleId")
                        .HasColumnType("int");

                    b.Property<int>("SharedItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeesRoleId");

                    b.HasIndex("SharedItemId");

                    b.ToTable("EmployeeRoleItems");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.EmployeesRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BusinessUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUserId");

                    b.ToTable("EmployeesRoles");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.ItemTaking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PrivateUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TakingTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PrivateUserId");

                    b.ToTable("ItemTakings");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.ItemTakingLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTaken")
                        .HasColumnType("bit");

                    b.Property<int?>("ItemTakingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturningTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SharedItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemTakingId");

                    b.HasIndex("SharedItemId");

                    b.ToTable("ItemTakingLines");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.PrivateUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RfidNumber")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("SearchId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PrivateUsers");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.SharedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BusinessUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RfidNumber")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("BusinessUserId");

                    b.ToTable("SharedItems");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.Employee", b =>
                {
                    b.HasOne("APZ_BACKEND.Core.Entities.BusinessUser", "BusinessUser")
                        .WithMany("Employees")
                        .HasForeignKey("BusinessUserId");

                    b.HasOne("APZ_BACKEND.Core.Entities.EmployeesRole", "EmployeesRole")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeesRoleId");

                    b.HasOne("APZ_BACKEND.Core.Entities.PrivateUser", "PrivateUser")
                        .WithMany("Employees")
                        .HasForeignKey("PrivateUserId");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.EmployeeRoleItem", b =>
                {
                    b.HasOne("APZ_BACKEND.Core.Entities.EmployeesRole", "EmployeesRole")
                        .WithMany()
                        .HasForeignKey("EmployeesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APZ_BACKEND.Core.Entities.SharedItem", "SharedItem")
                        .WithMany("EmployeeRoleItems")
                        .HasForeignKey("SharedItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.EmployeesRole", b =>
                {
                    b.HasOne("APZ_BACKEND.Core.Entities.BusinessUser", "BusinessUser")
                        .WithMany("EmployeesRoles")
                        .HasForeignKey("BusinessUserId");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.ItemTaking", b =>
                {
                    b.HasOne("APZ_BACKEND.Core.Entities.PrivateUser", "PrivateUser")
                        .WithMany("ItemTakings")
                        .HasForeignKey("PrivateUserId");
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.ItemTakingLine", b =>
                {
                    b.HasOne("APZ_BACKEND.Core.Entities.ItemTaking", "ItemTaking")
                        .WithMany("ItemTakingLines")
                        .HasForeignKey("ItemTakingId");

                    b.HasOne("APZ_BACKEND.Core.Entities.SharedItem", "SharedItem")
                        .WithMany("ItemTakingLines")
                        .HasForeignKey("SharedItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APZ_BACKEND.Core.Entities.SharedItem", b =>
                {
                    b.HasOne("APZ_BACKEND.Core.Entities.BusinessUser", "BusinessUser")
                        .WithMany("SharedItems")
                        .HasForeignKey("BusinessUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
