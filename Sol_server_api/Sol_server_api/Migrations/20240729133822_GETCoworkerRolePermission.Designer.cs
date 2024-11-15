﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sol_server_api.Data;

#nullable disable

namespace Sol_server_api.Migrations
{
    [DbContext(typeof(SolContext))]
    [Migration("20240729133822_GETCoworkerRolePermission")]
    partial class GETCoworkerRolePermission
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoworkerProject", b =>
                {
                    b.Property<int>("CoworkersCoworkerID")
                        .HasColumnType("int");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.HasKey("CoworkersCoworkerID", "ProjectID");

                    b.HasIndex("ProjectID");

                    b.ToTable("CoworkerProject");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonalDataID")
                        .HasColumnType("int");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("AdminID");

                    b.HasIndex("PersonalDataID");

                    b.HasIndex("RoleID");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            AdminID = 1,
                            AdminName = "admin",
                            Password = "$2a$11$5QnhghU49LcDvwn5DF9W1u66B47nSLG5cTWk9hvTFN2tvZy5.aiDq",
                            RoleID = 4
                        });
                });

            modelBuilder.Entity("Sol_server_api.Entities.Compartment", b =>
                {
                    b.Property<int>("CompartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompartmentID"));

                    b.Property<int>("Bracket")
                        .HasColumnType("int");

                    b.Property<int>("Col")
                        .HasColumnType("int");

                    b.Property<int?>("FKComponentID")
                        .HasColumnType("int");

                    b.Property<int>("MaximumPiece")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<string>("StoragedComponentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StoragedPiece")
                        .HasColumnType("int");

                    b.HasKey("CompartmentID");

                    b.HasIndex("FKComponentID")
                        .IsUnique()
                        .HasFilter("[FKComponentID] IS NOT NULL");

                    b.ToTable("rekesz", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Component", b =>
                {
                    b.Property<int>("ComponentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ComponentID"));

                    b.Property<string>("CompartmentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FKPackageID")
                        .HasColumnType("int");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("StockStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ComponentID");

                    b.HasIndex("FKPackageID");

                    b.ToTable("alkatresz", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Coworker", b =>
                {
                    b.Property<int>("CoworkerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CoworkerID"));

                    b.Property<string>("CoworkerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("CoworkerID");

                    b.HasIndex("RoleID");

                    b.ToTable("munkatars_fo", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("megrendelo", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Login", b =>
                {
                    b.Property<int>("LoginID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoginID"));

                    b.Property<int>("FKLoginCWID")
                        .HasColumnType("int");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginID");

                    b.HasIndex("FKLoginCWID")
                        .IsUnique();

                    b.HasIndex("LoginName")
                        .IsUnique();

                    b.ToTable("login", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Permission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PermissionID"));

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("PermissionID");

                    b.ToTable("jogosultsag", (string)null);

                    b.HasData(
                        new
                        {
                            PermissionID = 1,
                            PermissionName = "Új projekt létrehozása"
                        },
                        new
                        {
                            PermissionID = 2,
                            PermissionName = "Projektek listázása"
                        },
                        new
                        {
                            PermissionID = 3,
                            PermissionName = "Alkatrészek listázása"
                        },
                        new
                        {
                            PermissionID = 4,
                            PermissionName = "Alkatrészek projekthez rendelése"
                        },
                        new
                        {
                            PermissionID = 5,
                            PermissionName = "Becsült munkavégzési idő rögzítése"
                        },
                        new
                        {
                            PermissionID = 6,
                            PermissionName = "Árkalkuláció elkészítése"
                        },
                        new
                        {
                            PermissionID = 7,
                            PermissionName = "Projekt lezárása"
                        },
                        new
                        {
                            PermissionID = 8,
                            PermissionName = "Új alkatrészek felvétele a rendszerbe"
                        },
                        new
                        {
                            PermissionID = 9,
                            PermissionName = "Árak módosítása"
                        },
                        new
                        {
                            PermissionID = 10,
                            PermissionName = "Hiányzó alkatrészek listázása"
                        },
                        new
                        {
                            PermissionID = 11,
                            PermissionName = "Beérkező anyagok felvétele"
                        },
                        new
                        {
                            PermissionID = 12,
                            PermissionName = "Rekeszeknél a maximálisan elhelyezhető darabszám kezelése"
                        },
                        new
                        {
                            PermissionID = 13,
                            PermissionName = "Projekt kiválasztása kivételezéshez"
                        },
                        new
                        {
                            PermissionID = 14,
                            PermissionName = "Projekthez tartozó alkatrészek listázása"
                        },
                        new
                        {
                            PermissionID = 15,
                            PermissionName = "CREATE_COWORKER"
                        },
                        new
                        {
                            PermissionID = 16,
                            PermissionName = "UPDATE_COWORKER"
                        },
                        new
                        {
                            PermissionID = 17,
                            PermissionName = "DELETE_COWORKER"
                        },
                        new
                        {
                            PermissionID = 18,
                            PermissionName = "GET_COWORKER"
                        });
                });

            modelBuilder.Entity("Sol_server_api.Entities.PersonalData", b =>
                {
                    b.Property<int>("PersonalDataID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonalDataID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CoworkerID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonalDataID");

                    b.HasIndex("CoworkerID")
                        .IsUnique()
                        .HasFilter("[CoworkerID] IS NOT NULL");

                    b.ToTable("szemelyi_adat", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Process", b =>
                {
                    b.Property<int>("ProcessID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProcessID"));

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FKProjectID")
                        .HasColumnType("int");

                    b.Property<string>("ProcessName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProcessID");

                    b.HasIndex("FKProjectID")
                        .IsUnique();

                    b.ToTable("folyamat", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectID"));

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FKCustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("projectDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProjectID");

                    b.HasIndex("FKCustomerID");

                    b.ToTable("projekt", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.ProjectPackage", b =>
                {
                    b.Property<int>("ProjectPackageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectPackageID"));

                    b.Property<int>("FKProjectID")
                        .HasColumnType("int");

                    b.Property<string>("RequiredComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequiredPiece")
                        .HasColumnType("int");

                    b.HasKey("ProjectPackageID");

                    b.HasIndex("FKProjectID")
                        .IsUnique();

                    b.ToTable("projekt_csomag", (string)null);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(42)
                        .HasColumnType("nvarchar(42)");

                    b.HasKey("RoleID");

                    b.ToTable("Szerep", (string)null);

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            RoleName = "Szakember"
                        },
                        new
                        {
                            RoleID = 2,
                            RoleName = "Raktárvezető"
                        },
                        new
                        {
                            RoleID = 3,
                            RoleName = "Raktáros"
                        },
                        new
                        {
                            RoleID = 4,
                            RoleName = "Admin"
                        });
                });

            modelBuilder.Entity("Sol_server_api.Entities.RolePermission", b =>
                {
                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("PermissionID")
                        .HasColumnType("int");

                    b.HasKey("RoleID", "PermissionID");

                    b.HasIndex("PermissionID");

                    b.ToTable("SzerepJogosultsag", (string)null);

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            PermissionID = 1
                        },
                        new
                        {
                            RoleID = 1,
                            PermissionID = 2
                        },
                        new
                        {
                            RoleID = 1,
                            PermissionID = 3
                        },
                        new
                        {
                            RoleID = 1,
                            PermissionID = 4
                        },
                        new
                        {
                            RoleID = 1,
                            PermissionID = 5
                        },
                        new
                        {
                            RoleID = 1,
                            PermissionID = 6
                        },
                        new
                        {
                            RoleID = 1,
                            PermissionID = 7
                        },
                        new
                        {
                            RoleID = 2,
                            PermissionID = 8
                        },
                        new
                        {
                            RoleID = 2,
                            PermissionID = 9
                        },
                        new
                        {
                            RoleID = 2,
                            PermissionID = 10
                        },
                        new
                        {
                            RoleID = 2,
                            PermissionID = 11
                        },
                        new
                        {
                            RoleID = 2,
                            PermissionID = 12
                        },
                        new
                        {
                            RoleID = 3,
                            PermissionID = 13
                        },
                        new
                        {
                            RoleID = 3,
                            PermissionID = 14
                        },
                        new
                        {
                            RoleID = 4,
                            PermissionID = 15
                        },
                        new
                        {
                            RoleID = 4,
                            PermissionID = 16
                        },
                        new
                        {
                            RoleID = 4,
                            PermissionID = 17
                        },
                        new
                        {
                            RoleID = 4,
                            PermissionID = 18
                        });
                });

            modelBuilder.Entity("CoworkerProject", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Coworker", null)
                        .WithMany()
                        .HasForeignKey("CoworkersCoworkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sol_server_api.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.Admin", b =>
                {
                    b.HasOne("Sol_server_api.Entities.PersonalData", "PersonalData")
                        .WithMany()
                        .HasForeignKey("PersonalDataID");

                    b.HasOne("Sol_server_api.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PersonalData");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Compartment", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Component", null)
                        .WithOne("Compartment")
                        .HasForeignKey("Sol_server_api.Entities.Compartment", "FKComponentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Component", b =>
                {
                    b.HasOne("Sol_server_api.Entities.ProjectPackage", null)
                        .WithMany("Components")
                        .HasForeignKey("FKPackageID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sol_server_api.Entities.Coworker", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Role", "Role")
                        .WithMany("Coworkers")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Login", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Coworker", null)
                        .WithOne("Login")
                        .HasForeignKey("Sol_server_api.Entities.Login", "FKLoginCWID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.PersonalData", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Coworker", null)
                        .WithOne("PersonalData")
                        .HasForeignKey("Sol_server_api.Entities.PersonalData", "CoworkerID");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Process", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Project", null)
                        .WithOne("Process")
                        .HasForeignKey("Sol_server_api.Entities.Process", "FKProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.Project", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Customer", null)
                        .WithMany("Projects")
                        .HasForeignKey("FKCustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.ProjectPackage", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Project", null)
                        .WithOne("ProjectPackage")
                        .HasForeignKey("Sol_server_api.Entities.ProjectPackage", "FKProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.RolePermission", b =>
                {
                    b.HasOne("Sol_server_api.Entities.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sol_server_api.Entities.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Component", b =>
                {
                    b.Navigation("Compartment");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Coworker", b =>
                {
                    b.Navigation("Login");

                    b.Navigation("PersonalData")
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.Customer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Project", b =>
                {
                    b.Navigation("Process")
                        .IsRequired();

                    b.Navigation("ProjectPackage")
                        .IsRequired();
                });

            modelBuilder.Entity("Sol_server_api.Entities.ProjectPackage", b =>
                {
                    b.Navigation("Components");
                });

            modelBuilder.Entity("Sol_server_api.Entities.Role", b =>
                {
                    b.Navigation("Coworkers");

                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
