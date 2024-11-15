using Microsoft.EntityFrameworkCore;
using Sol_server_api.Entities;
using Sol_server_api.DTOs;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Sol_server_api.Data
{
    public class SolContext : DbContext
    {
        
        public SolContext(DbContextOptions<SolContext> options) : base(options)
        {

        }


        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Project>? Projects { get; set; }
        public DbSet<ProjectPackage>? ProjectPackages { get; set; }
        public DbSet<Component>? Components { get; set; }
        public DbSet<PackageComponent>? PackageComponents { get; set; }
        public DbSet<Compartment>? Compartments { get; set; }
        public DbSet<Process>? Processes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<Coworker>? Coworkers { get; set; }
        public DbSet<PersonalData>? PersonalDatas { get; set; }
        public DbSet<Permission>? Permissions { get; set; }
        public DbSet<Login>? Logins { get; set; }
        public DbSet<Admin>? Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tábla átnevezések, hogy ne az osztályok nevén legyenek azonosítva
            modelBuilder.Entity<Customer>().ToTable("megrendelo");
            modelBuilder.Entity<Project>().ToTable("projekt");
            modelBuilder.Entity<ProjectPackage>().ToTable("projekt_csomag");
            modelBuilder.Entity<PackageComponent>().ToTable("projektcsomag_alkatrész");
            modelBuilder.Entity<Component>().ToTable("alkatresz");
            modelBuilder.Entity<Compartment>().ToTable("rekesz");
            modelBuilder.Entity<Process>().ToTable("folyamat");

            modelBuilder.Entity<Coworker>().ToTable("munkatars_fo");
            modelBuilder.Entity<PersonalData>().ToTable("szemelyi_adat");
            modelBuilder.Entity<Permission>().ToTable("jogosultsag");
            modelBuilder.Entity<Login>().ToTable("login");

            modelBuilder.Entity<Role>().ToTable("Szerep");
            modelBuilder.Entity<RolePermission>().ToTable("SzerepJogosultsag");
            modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleID, rp.PermissionID });

            //Entitások elsődleges kulcsának beállítása, ahol nem Id van a változónévben
            //modelBuilder.Entity<Customer>().Property(cu => cu.CustomerID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().HasKey(e => e.CustomerID);

            //modelBuilder.Entity<Project>().Property(p => p.ProjectID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>().HasKey(e => e.ProjectID);

            //modelBuilder.Entity<ProjectPackage>().Property(pp => pp.ProjectPackageID).ValueGeneratedOnAdd();
            modelBuilder.Entity<ProjectPackage>().HasKey(pp => pp.ProjectPackageID);

            //modelBuilder.Entity<Component>().Property(comp => comp.ComponentID).ValueGeneratedOnAdd();  
            modelBuilder.Entity<Component>().HasKey(e => e.ComponentID);

            modelBuilder.Entity<PackageComponent>().HasKey(e => e.PackageComponentID);

            //modelBuilder.Entity<Compartment>().Property(compart => compart.CompartmentID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Compartment>().HasKey(e => e.CompartmentID);

            modelBuilder.Entity<Process>().HasKey(e => e.ProcessID);

            //modelBuilder.Entity<Coworker>().Property(c => c.CoworkerID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Coworker>().HasKey(e => e.CoworkerID);

            modelBuilder.Entity<PersonalData>().HasKey(e => e.PersonalDataID);
            modelBuilder.Entity<Permission>().HasKey(e => e.PermissionID);

            //modelBuilder.Entity<Login>().Property(l => l.LoginID).ValueGeneratedOnAdd();
            modelBuilder.Entity<Login>().HasKey(e => e.LoginID);
            modelBuilder.Entity<Login>().HasIndex(l => l.LoginName).IsUnique();

            //Újak
            modelBuilder.Entity<Role>().HasKey(r => r.RoleID);




            //Táblakapcsolatok
            //A - Projekt és Raktározás
            //A1. Customer-Project: egy projekthez egy megrendelő tartozik, egy megrendelőhöz több projekt is tartozhat, Foreign Key: ProjektCode          
            modelBuilder.Entity<Customer>()
                    .HasMany(c => c.Projects)                           // Egy ügyfélhez több projekt tartozhat
                    .WithOne(p => p.Customer)                                                 // Minden projektnek pontosan egy ügyfele van
                    .HasForeignKey(p => p.FKCustomerID);        // Projekt táblában a ProjectCode oszlop lesz a külső kulcs
                                                                // Minden projektnek egy ügyfele van
                                                                // Egy ügyfélhez csak egy projekt tartozik
                                                                // Customer táblában a CustomerId oszlop lesz a külső kulcs

            //A2. Egy projekthez egy projektcsomag tartozik, egy projektcsomaghoz egy projekt  tartozik a projektod a foreign key
            modelBuilder.Entity<ProjectPackage>().HasOne(p => p.Project).WithOne().HasForeignKey<ProjectPackage>(e => e.FKProjectID);

            //A3. Egy projektcsomaghoz több csomag_alkatrész tartozik, egy csomag_alkatrészhez egy projektcsomaghoz tartozik.
            modelBuilder.Entity<ProjectPackage>().HasMany(pp => pp.PackageComponents).WithOne(pc => pc.ProjectPackage).HasForeignKey(pc => pc.ProjectPackageID).OnDelete(DeleteBehavior.Cascade);

            /*//A3. Egy projektcsomaghoz több alkatrész tartozik, egy alkatrészhez egy projektcsomag
            modelBuilder.Entity<ProjectPackage>().HasMany(e => e.Components).WithOne().HasForeignKey(e => e.FKPackageID).OnDelete(DeleteBehavior.SetNull);
            */
            //A4. Egy alkatrészhez egy rekesz tartozik, egy rekeszhez egy alkatresz tarozik foreign key RekeszAlkatrészNév 
            modelBuilder.Entity<Component>().HasOne(e => e.Compartment).WithOne().HasForeignKey<Compartment>(e => e.FKComponentID).OnDelete(DeleteBehavior.SetNull);

            //A5. Egy projekthez egy folyamat tábla tartozik és egy folyamat táblához egy projekt
            modelBuilder.Entity<Project>().HasOne(p => p.Process).WithOne().HasForeignKey<Process>(pr => pr.FKProjectID);


            //B - Munkatársak - Adatok és Admin táblák
            //Egy pojecthez több munkatárs tartozhat, egy munkatárshoz több project tartozhat ~~~many to many kapcsolatnál nem kell Foreign Key-t meghatározni az EF Core automatikusan kezeli~~~
            modelBuilder.Entity<Project>().HasOne(p => p.Coworker).WithMany().HasForeignKey(p => p.FKCoworkerID).OnDelete(DeleteBehavior.SetNull);

            //B2. - Egy munkatarshoz egy szemelyes_adat tartozik, egy munkatars adathoz egy munkatars_fo tartozik, FK MunkatarsTel
            modelBuilder.Entity<Coworker>().HasOne(e => e.PersonalData).WithOne().HasForeignKey<PersonalData>(e => e.CoworkerID);

            //B3. Egy munkatarshoz egy  login tartozik, egy loginhoz egy munkatars_fotabla tartozik
            modelBuilder.Entity<Coworker>().HasOne(e => e.Login).WithOne().HasForeignKey<Login>(e => e.FKLoginCWID);

            //B4. Egy szerephez több munkatárs tartozik, és egy munkatárshoz egy szerep tartozik
            modelBuilder.Entity<Role>().HasMany(r => r.Coworkers).WithOne(c => c.Role).HasForeignKey(c => c.RoleID);

            //B5/a. A RolePermission tábla párosítja az ismétlődő RoleID-kat a unique PermissionID-khoz -> Egy szerephez így tartozik több jogosultság
            modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleID);

            //B5/b.Míg az előbbi a Rolet-kapcsolta a RolePermission táblához ez a Permission-t kapcsolja hozzá
            //Egy RolePermission egy Permission-hoz tartozik, és egy Permissionhoz több RolePermission tartozik.
            modelBuilder.Entity<RolePermission>().HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionID);


            // Kezdeti adatok
            modelBuilder.Entity<Process>().HasData(
                new Process { ProcessID = 1, ProcessName = "New", Desc = "A projekt létrehozásra került, de még nem történt meg a helyszíni felmérés."},
                new Process { ProcessID = 2, ProcessName = "Draft", Desc = "A helyszíni felmérés folyamatban van, a terv még nem került véglegesítésre." },
                new Process { ProcessID = 3, ProcessName = "Wait", Desc = "A helyszíni felmérés megtörtént, de az árkalkulációt nem lehetett befejezni, mert volt olyan alkatrész, amely nincs a raktárban, így az ára nem ismert." },
                new Process { ProcessID = 4, ProcessName = "Scheduled", Desc = "Árkalkuláció elkészült, a projekt a megvalósításra várakozik." },
                new Process { ProcessID = 5, ProcessName = "InProgress", Desc = "A projekt megvalósítása megkezdődött, amelynek első lépése az alkatrészek raktárból való kivételezése" },
                new Process { ProcessID = 6, ProcessName = "Completed", Desc = "A projekt sikeresen megvalósult." },
                new Process { ProcessID = 7, ProcessName = "Failed", Desc = "A projekt megvalósítása nem sikerült." }
                );

            modelBuilder.Entity<Role>().HasData(
                    new Role { RoleID = 1, RoleName = "Szakember" },
                    new Role { RoleID = 2, RoleName = "Raktárvezető" },
                    new Role { RoleID = 3, RoleName = "Raktáros" },
                    new Role { RoleID = 4, RoleName = "Admin" }
                );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { PermissionID = 1, PermissionName = "Új projekt létrehozása", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 2, PermissionName = "Projektek listázása", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 3, PermissionName = "Alkatrészek listázása", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 4, PermissionName = "Alkatrészek projekthez rendelése", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 5, PermissionName = "Becsült munkavégzési idő rögzítése", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 6, PermissionName = "Árkalkuláció elkészítése", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 7, PermissionName = "Projekt lezárása", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 8, PermissionName = "Új alkatrészek felvétele a rendszerbe", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 9, PermissionName = "Árak módosítása", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 10, PermissionName = "Hiányzó alkatrészek listázása", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 11, PermissionName = "Beérkező anyagok felvétele", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 12, PermissionName = "Rekeszeknél a maximálisan elhelyezhető darabszám kezelése", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 13, PermissionName = "Projekt kiválasztása kivételezéshez", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 14, PermissionName = "Projekthez tartozó alkatrészek listázása", RolePermissions = new List<RolePermission>() },

                new Permission { PermissionID = 15, PermissionName = "CREATE_COWORKER", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 16, PermissionName = "UPDATE_COWORKER" , RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 17, PermissionName = "DELETE_COWORKER", RolePermissions = new List<RolePermission>() },
                new Permission { PermissionID = 18, PermissionName = "GET_COWORKER", RolePermissions = new List<RolePermission>()}
                );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { RoleID = 1, PermissionID = 1},
                new RolePermission { RoleID = 1, PermissionID = 2 },
                new RolePermission { RoleID = 1, PermissionID = 3 },
                new RolePermission { RoleID = 1, PermissionID = 4 },
                new RolePermission { RoleID = 1, PermissionID = 5 },
                new RolePermission { RoleID = 1, PermissionID = 6 },
                new RolePermission { RoleID = 1, PermissionID = 7 },
                new RolePermission { RoleID = 2, PermissionID = 8 },
                new RolePermission { RoleID = 2, PermissionID = 9 },
                new RolePermission { RoleID = 2, PermissionID = 10 },
                new RolePermission { RoleID = 2, PermissionID = 11 },
                new RolePermission { RoleID = 2, PermissionID = 12 },
                new RolePermission { RoleID = 3, PermissionID = 13 },
                new RolePermission { RoleID = 3, PermissionID = 14 },
                new RolePermission { RoleID = 4, PermissionID = 15 },
                new RolePermission { RoleID = 4, PermissionID = 16 },
                new RolePermission { RoleID = 4, PermissionID = 17 },
                new RolePermission { RoleID = 4, PermissionID = 18 }
                );

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminID = 1,
                    AdminName = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin-password"),
                    RoleID = 4, 
                }

                );
        }
    }
}
