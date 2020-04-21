﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class VendorInvoice
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorInvoiceId { get; set; }
        public string VendorInvoiceName { get; set; }
        [Display(Name = "Envio")]
        public int ShipmentId { get; set; }

        public int? PurchaseOrderId { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        [Display(Name = "Fecha de Factura")]
        public DateTime VendorInvoiceDate { get; set; }
        [Display(Name = "Fecha de vencimiento")]
        public DateTime VendorInvoiceDueDate { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Tipo de Factura")]
        public int VendorInvoiceTypeId { get; set; }    

        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }
        

        [Display(Name = "Numero de Factura")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Numero de Factura")]
        public int NumeroDEI { get; set; }

        [Display(Name = "Numero de inicio")]
        public string NoInicio { get; set; }

        [Display(Name = "Numero fin")]
        public string NoFin { get; set; }

        [Display(Name = "Fecha Limite")]
        public DateTime FechaLimiteEmision { get; set; }

        [Display(Name = "Numero de Factura")]
        public string CAI { get; set; }

        [Display(Name = "Numero de orden de compra exenta")]
        public string NoOCExenta { get; set; }

        [Display(Name = "Numero de constancia de registro de exoneracion")]
        public string NoConstanciadeRegistro { get; set; }

        [Display(Name = "Numero de registro de la SAG")]
        public string NoSAG { get; set; }

        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Display(Name = "Telefono")]
        public string Tefono { get; set; }

        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }

        [Display(Name = "Nombre Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Vendor")]
        public long VendorId { get; set; }

        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        [Display(Name = "Nombre Proveedor")]
        public string VendorName { get; set; }

   


        public DateTime OrderDate { get; set; }
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Moneda")]
        public int CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Moneda tasa")]
        public double Currency { get; set; }

        [Display(Name = "Numero de referencia de Proveedor")]
        public string VendorRefNumber { get; set; }
        [Display(Name = "Tipo de ventas")]
        public int SalesTypeId { get; set; }

        [Display(Name = "Observacion")]
        public string Remarks { get; set; }

        [Display(Name = "Monto")]
        public double Amount { get; set; }
        public double SubTotal { get; set; }

        [Display(Name = "Descuento")]
        public double Discount { get; set; }


        [Display(Name = "Impuesto")]
        public double Tax { get; set; }

        [Display(Name = "Impuesto 18%")]
        public double Tax18 { get; set; }


        [Display(Name = "Flete")]
        public double Freight { get; set; }

        [Display(Name = "Total exento")]
        public double TotalExento { get; set; }

        [Display(Name = "Total exonerado")]
        public double TotalExonerado { get; set; }

        [Display(Name = "Total Gravado")]
        public double TotalGravado { get; set; }

        [Display(Name = "Total Gravado 18%")]
        public double TotalGravado18 { get; set; }

        public double Total { get; set; }

        public string TotalLetras { get; set; }

        public Int64 IdEstado { get; set; }

        public string Estado { get; set; }

        public Int64 AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Accounting  Account { get; set; }


        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Impreso { get; set; }
        public List<VendorInvoiceLine> VendorInvoiceLine { get; set; } = new List<VendorInvoiceLine>();
    }
}
