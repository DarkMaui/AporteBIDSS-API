using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class RegistroGastos
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [Display(Name = "Concepto de gasto")]
        public string Detalle { get; set; }
        public string Identidad { get; set; }
        [Required]
        [Display(Name = "Tipo de gasto")]
        public int TipoGastosId { get; set; }
        [ForeignKey("TipoGastosId")]
        public TipoGastos TipoGastos { get; set; }
        public string Concepto { get; set; }
        public string Documento { get; set; }
        public double monto { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de Modificación")]
        public DateTime FechaModificacion { get; set; }

    }
}
