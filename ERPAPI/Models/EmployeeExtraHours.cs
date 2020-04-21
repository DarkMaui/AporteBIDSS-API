﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class EmployeeExtraHours
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 EmployeeExtraHoursId { get; set; }

        [Display(Name = "Id Empleado")]
        public Int64 EmployeeId { get; set; }

        [Display(Name = "Empleado")]
        public string EmployeeName { get; set; }

        [Display(Name = "Fecha de trabajo")]
        public DateTime WorkDate { get; set; }

        [Display(Name = "Motivo")]
        public string Motivo { get; set; }

        //public Customer Customer { get; set; }
        //[Display(Name = "Id Cliente")]
        //[ForeignKey("CustomerId")]
        //public Int64? CustomerId { get; set; }

        [Display(Name = "Usuario modificación")]
        public string UsuarioModificacion { get; set; }

        [Display(Name = "Usuario creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Fecha de  modificación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Usuario modificación")]
        public DateTime FechaModificacion { get; set; }

     
        public List<EmployeeExtraHoursDetail> EmployeeExtraHoursDetail { get; set; }
    }
}
