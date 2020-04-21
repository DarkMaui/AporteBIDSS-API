﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class SalesOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesOrderId { get; set; }
        [Display(Name = "Nombre cotizacion")]
        public string SalesOrderName { get; set; }

        [Display(Name = "Tipo de contrato")]
        public Int64 TypeContractId { get; set; }

        [Display(Name = "Nombre de contrato")]
        public string NameContract { get; set; }

        [Display(Name = "Tipo de Facturación")]
        public Int64 TypeInvoiceId { get; set; }

        [Display(Name = "Tipo de Facturación")]
        public string TypeInvoiceName { get; set; }

        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Display(Name = "Telefono")]
        public string Tefono { get; set; }

       
        //[EmailAddress(ErrorMessage ="Agregue una direccion de correo valida")]
        [EmailAddress]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Correo invalido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Id Sucursal")]
        public int BranchId { get; set; }

        [Display(Name = "Sucursal Nombre")]
        public string BranchName { get; set; }

        [Display(Name = "Cliente Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }
        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Moneda tasa")]
        public double Currency { get; set; }

        [Display(Name = "Id")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Nombre Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Numero de referencia de cliente")]
        public string CustomerRefNumber { get; set; }
        [Display(Name = "Tipo de ventas")]
        public int SalesTypeId { get; set; }
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

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        public string Impreso { get; set; }

        public List<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
