
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ERPAPI.Helpers;

namespace ERP.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
    ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, AspNetUserLogins,
    AspNetRoleClaims, AspNetUserTokens>
    
    //  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //public class ApplicationDbContext :
    //    IdentityDbContext<ApplicationUser, IdentityRole, string, ApplicationUserClaim, IdentityUserRole<int>
    //        , IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<TipoGastos> TipoGastos { get; set; }
        public DbSet<InventoryTransfer> InventoryTransfer { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<MotivosAjuste> MotivosAjuste { get; set; }
        public DbSet<InventoryTransferLine> InventoryTransferLine { get; set; }

        public DbSet<GrupoEstado> GrupoEstado { get; set; }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<RegistroGastos> RegistroGastos { get; set; }

        public DbSet<Estados> Estados { get; set; }
        public DbSet<Policy> Policy { get; set; }
        public DbSet<PolicyClaims> PolicyClaims { get; set; }
        public DbSet<PolicyRoles> PolicyRoles { get; set; }

        public DbSet<ApplicationUserClaim> ApplicationUserClaim { get; set; }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Tax> Tax { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }
        public DbSet<ProformaInvoice> ProformaInvoice { get; set; }
        public DbSet<ProformaInvoiceLine> ProformaInvoiceLine { get; set; }
        public DbSet<CAI> CAI { get; set; }
        public DbSet<NumeracionSAR> NumeracionSAR { get; set; }
        public DbSet<PuntoEmision> PuntoEmision { get; set; }
        public DbSet<TiposDocumento> TiposDocumento { get; set; }
        public DbSet<SubProduct> SubProduct { get; set; }
        public DbSet<ElementoConfiguracion> ElementoConfiguracion { get; set; }
        public DbSet<GrupoConfiguracion> GrupoConfiguracion { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<PasswordHistory> PasswordHistory { get; set; }

       
        public DbSet<CompanyInfo> CompanyInfo { get; set; }
      
        public DbSet<ERPAPI.Models.Puesto> Puesto { get; set; }
        public DbSet<ERPAPI.Models.Departamento> Departamento { get; set; }
       
        public DbSet<ERPAPI.Models.TipoDocumento> TipoDocumento { get; set; }
      
        public DbSet<Linea> Linea { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
       

        public DbSet<FundingInterestRate> FundingInterestRate { get; set; }


        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<PurchaseOrderLine> PurchaseOrderLine { get; set; }

        
        public DbSet<GrupoConfiguracionIntereses> GrupoConfiguracionIntereses { get; set; }
        public DbSet<KardexViale> KardexViale { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PolicyClaims>()
           .HasKey(c => new { c.idroleclaim, c.IdPolicy });



           


            modelBuilder.Entity<Product>()
           .HasIndex(p => new { p.ProductCode })
           .IsUnique(true);

            modelBuilder.Entity<UnitOfMeasure>()
             .HasIndex(p => new { p.UnitOfMeasureName })
              .IsUnique(true);

           

            modelBuilder.Entity<SubProduct>()
           .HasIndex(p => new { p.ProductCode })
           .IsUnique(true);

            modelBuilder.Entity<Customer>()
           .HasIndex(p => new { p.RTN })
           .IsUnique(true);

            modelBuilder.Entity<Departamento>()
            .HasIndex(e => e.NombreDepartamento)
            .IsUnique(true);

            

            modelBuilder.Entity<Customer>()
                 .HasOne(i => i.Departamento)
                 .WithMany(c => c.Customer)
                 //.IsRequired()
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasOne(i => i.Departamento)
                .WithMany(c => c.Branch)
                //.IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
              .HasOne(i => i.Currency)
              .WithMany(c => c.Product)
              //.IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

          


            modelBuilder.Entity<Employees>()
           .HasOne(i => i.City)
           .WithMany(c => c.Employees)
           //.IsRequired()
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
              .HasOne(i => i.Country)
              .WithMany(c => c.Employees)
                //.IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
             .HasOne(i => i.Currency)
             .WithMany(c => c.Employees)
             //.IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
            .HasOne(i => i.Estados)
            .WithMany(c => c.Employees)
            //.IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employees>()
          .HasOne(i => i.State)
          .WithMany(c => c.Employees)
          //.IsRequired()
          .OnDelete(DeleteBehavior.Restrict);

          

            modelBuilder.Entity<GrupoConfiguracionIntereses>().ToTable("GrupoConfiguracionIntereses");
            modelBuilder.Entity<TipoGastos>().ToTable("TipoGastos");
            modelBuilder.Entity<RegistroGastos>().ToTable("RegistroGastos");
            modelBuilder.Entity<MotivosAjuste>().ToTable("MotivosAjuste");
            modelBuilder.Entity<KardexViale>().ToTable("KardexViale");
            modelBuilder.Entity<Country>().ToTable("Country");
        }
    }
}
