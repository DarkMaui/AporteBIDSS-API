﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GoodsDeliveryAuthorizationLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id Linea")]
        public Int64 GoodsDeliveryAuthorizationLineId { get; set; }
        [Display(Name = "Autorizacion Id")]
        public Int64 GoodsDeliveryAuthorizationId { get; set; }
        [Display(Name = "Número de certificado")]
        public Int64 NoCertificadoDeposito { get; set; }

        [Display(Name = "Producto cliente")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Producto cliente")]
        public string SubProductName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 WarehouseId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string WarehouseName { get; set; }

        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }

        [Display(Name = "Precio")]
        public double Price { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Valor del certificado")]
        public double valorcertificado { get; set; }
        [Display(Name = "Valor financiado")]
        public double valorfinanciado { get; set; }

        [Display(Name = "Valor a pagar impuestos")]
        public double ValorImpuestos { get; set; }

        [Display(Name = "Saldo de producto")]
        public double SaldoProducto { get; set; }


    }
}
