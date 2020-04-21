using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class TipoGastos
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }
        [Display(Name = "Saldo")]
        public float Saldo { get; set; } = 0;
        [Display(Name = "Presupuesto")]
        public float Presupuesto { get; set; } = 0;
        [Display(Name = "Total")]
        public float Total { get; set; } = 0;
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        [Required]
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
    }
}
