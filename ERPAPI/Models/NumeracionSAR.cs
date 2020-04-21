﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class NumeracionSAR
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // [Key]
        public Int64 IdNumeracion { get; set; }

        public Int64 IdCAI { get; set; }

        public string _cai { get; set; }

        public string NoInicio { get; set; }
        public string NoFin { get; set; }
        public DateTime FechaLimite { get; set; }
        public int CantidadOtorgada { get; set; }
        public string SiguienteNumero { get; set; }

        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }

        public Int64 IdPuntoEmision { get; set; }
        public string PuntoEmision { get; set; }
        public Int64 DocTypeId { get; set; }
        public string DocType { get; set; }
        public Int64 DocSubTypeId { get; set; }
        public string DocSubType { get; set; }
        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }


    }
}
