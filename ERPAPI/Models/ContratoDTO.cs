using System.Collections.Generic;

namespace ERPAPI.Models
{
    public class ContratoDTO
    {
        public Contrato Contrato { get; set; }

        public virtual ICollection<Contrato_detalle> Contrato_detalle { get; set; }

        public virtual ICollection<Contrato_movimientos> Contrato_movimientos { get; set; }
        public virtual ICollection<Contrato_plan_pagos> Contrato_plan_pagos { get; set; }
    }
}
