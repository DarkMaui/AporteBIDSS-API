﻿using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Vendor
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 VendorId { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Display(Name = "Vendor Type")]        
        public Int64 VendorTypeId { get; set; }
        [ForeignKey("VendorTypeId")]
        public VendorType VendorType { get; set; }
        [Display(Name = "Tipo de Proveedor")]
        public string VendorTypeName { get; set; }

        [Display(Name = "Dirección")]
        public string Address { get; set; }


        public long CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public long StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
        public long CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        [Display(Name = "Categoria Proveedor")]
        public Int64? IdEstadoVendorConfi { get; set; }
        [Display(Name = "Categoria Proveedor")]
        public string EstadoVendorConfi { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Telefono")]
        public string Phone { get; set; }
        public string Email { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Required]
        public string UsuarioCreacion { get; set; }
        [Required]
        public string UsuarioModificacion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        [Required]
        [Display(Name = "RTN del Proveedor")]
        public string RTN { get; set; }
     
        [Display(Name = "Identidad del contacto")]
        public string Identidad { get; set; }
        [Display(Name = "Moneda")]
        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        [Required]
        [Display(Name = "Limite Mensual")]
        public double QtyMonth { get; set; }
        [Display(Name = "Telefono Referencia")]
        public string PhoneReferenceone { get; set; }

        [Display(Name = "Empresa Referencia")]
        public string CompanyReferenceone { get; set; }
        [Display(Name = "Telefono Referencia")]
        public string PhoneReferencetwo { get; set; }

        [Display(Name = "Empresa Referencia")]
        public string CompanyReferencetwo { get; set; }
        [Display(Name = "Activo/Inactivo ")]
        public Int64 IdEstado { get; set; }
        [ForeignKey("IdEstado")]
        public Estados Estados { get; set; }
        public string Estado { get; set; }

        [Display(Name = "Identidad del repressentante Legal")]
        public string IdentityRepresentative { get; set; }
        [Display(Name = "RTN del representante legal")]
        public string RTNRepresentative { get; set; }
        [Display(Name = "Nombre del Representante")]
        public string RepresentativeName { get; set; }
        


    }
}
