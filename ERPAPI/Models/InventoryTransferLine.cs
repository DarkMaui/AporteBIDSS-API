﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InventoryTransferLine
    {
        [Key]
        public int Id { get; set; }

        public int InventoryTransferId { get; set; }
        [ForeignKey("InventoryTransferId")]
        public InventoryTransfer InventoryTransfer { get; set; }

        public Int64 ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string ProductName { get; set; }

        public double QtyStock { get; set; }
        public double CantidadRecibida { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
