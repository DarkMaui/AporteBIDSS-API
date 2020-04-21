using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string Correlative { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }


        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        [Display(Name = "UOM")]
        public int? UnitOfMeasureId { get; set; } = null;
        [ForeignKey("UnitOfMeasureId")]
        public  UnitOfMeasure UnitOfMeasure { get; set; }
        public decimal DefaultBuyingPrice { get; set; } = 0;
        public decimal DefaultSellingPrice { get; set; } = 0;
        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }
        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }
        [ForeignKey("BranchId")]
        public  Branch Branch { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        public int LineaId { get; set; }
        [ForeignKey("LineaId")]
        public Linea Linea { get; set; }
        public int GrupoId { get; set; }
        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }

        public int FundingInterestRateId { get; set; }
        [ForeignKey("FundingInterestRateId")]
        public FundingInterestRate FundingInterestRate { get; set; }

        public decimal? Prima { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? Valor_prima { get; set; }

        public long? ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ElementoConfiguracion ProductType { get; set; }

        [StringLength(50)]
        public string Serie { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }

        public bool FlagConsignacion { get; set; }

        [StringLength(100)]
        public string SerieMotor { get; set; }
        [StringLength(100)]
        public string SerieChasis{ get; set; }

        [Display(Name = "Impuesto a Aplicar para Contratos")]
        public Int64? TaxId { get; set; }
        [ForeignKey("TaxId")]
        public Tax Tax { get; set; }

        [Display(Name = "Porcentaje de Descuentos")]
        public decimal? PorcentajeDescuento { get; set; }

        [Display(Name = "Regalia")]
        public bool Regalia { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }



        public List<ProductRelation> ProductRelation { get; set; }
    }
}
