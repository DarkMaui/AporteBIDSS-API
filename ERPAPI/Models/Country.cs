﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Country
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } // bigint
        [Display(Name = "Nombre")]
        public string SortName { get; set; } // text
        [Display(Name = "Nombre")]
        public string Name { get; set; } // text
        [Display(Name = "Código de telefono")]
        public int? PhoneCode { get; set; } // integer
        [Display(Name = "GAFI")]
        public bool GAFI { get; set; } = false;
        [Display(Name = "Lista")]
        public int ListaId { get; set; }
        [Display(Name = "Lista")]
        public string ListaName { get; set; }

        [Display(Name = "Actualización")]
        public DateTime Actualizacion { get; set; }

        [Display(Name = "Nivel de riesgo")]
        public int NivelRiesgo { get; set; }

        [Display(Name = "Nivel de riesgo")]
        public string NivelRiesgoName { get; set; }

        [Display(Name = "Tipo alerta")]
        public int TipoAlertaId { get; set; }

        [Display(Name = "Tipo de alerta")]
        public string TipoAlertaName { get; set; }

        [Display(Name = "Nivel de riesgo")]
        public int AccionId { get; set; }

        [Display(Name = "Nivel de riesgo")]
        public string AccionName { get; set; }

        [Display(Name = "Seguimiento o monitoreo")]
        public int SeguimientoId { get; set; }

        [Display(Name = "Seguimiento o monitoreo")]
        public string SeguimientoName { get; set; }

        [Display(Name = "Comentarios")]
        public string Comments { get; set; }

        [Display(Name = "Estado")]
        public long IdEstado { get; set; } // bigint
        [Display(Name = "Estado")]
        public string Estado { get; set; } // text

        [Display(Name = "Usuario de creación")]
        public string Usuariocreacion { get; set; }
        [Display(Name = "Usuario de modificación")]
        public string Usuariomodificacion { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime? FechaCreacion { get; set; }
        [Display(Name = "Fecha de modificación")]
        public DateTime? FechaModificacion { get; set; }
        public List<State> State { get; set; }

        public virtual List<Employees> Employees { get;set;}
    }


   

}
