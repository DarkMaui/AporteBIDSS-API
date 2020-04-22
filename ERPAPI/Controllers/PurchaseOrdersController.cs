using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/PurchaseOrder")]
    [ApiController]
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public PurchaseOrdersController(ILogger<PurchaseOrdersController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de PurchaseOrder paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchaseOrderPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<PurchaseOrder> Items = new List<PurchaseOrder>();
            try
            {
                var query = _context.PurchaseOrder.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene el Listado de PurchaseOrderes 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPurchaseOrder()
        {
            List<PurchaseOrder> Items = new List<PurchaseOrder>();
            try
            {
                Items = await _context.PurchaseOrder
                    .Include(c =>c.Estados)
                    .ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{VendorId}")]
        public async Task<IActionResult> GetPurchaseOrderByVendor(Int64 VendorId)
        {
            List<PurchaseOrder> Items = new List<PurchaseOrder>();
            try
            {
                Items = await _context.PurchaseOrder.Where(q => q.VendorId == VendorId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }



        /// <summary>
        /// Obtiene los Datos de la PurchaseOrder por medio del Id enviado.
        /// </summary>
        /// <param name="PurchaseOrderId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{PurchaseOrderId}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderById(Int64 PurchaseOrderId)
        {
            PurchaseOrder Items = new PurchaseOrder();
            try
            {
                Items = await _context.PurchaseOrder.Include(q => q.PurchaseOrderLines).Where(q => q.Id == PurchaseOrderId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
        /// <summary>
        /// Retorna el Correlativo Nuevo para orden de Compra
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPONumberCorrelative() {
            try
            {
                int maxId = await _context.PurchaseOrder.MaxAsync(q =>q.Id) +1 ;
                string correlativo = "OC-" + maxId.ToString().PadLeft(8,'0');
                return await Task.Run(() => Ok(correlativo));
            }
            catch (Exception ex )
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            
        
        }




        /// <summary>
        /// Inserta una nueva PurchaseOrder
        /// </summary>
        /// <param name="_PurchaseOrder"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<PurchaseOrder>> Insert([FromBody]PurchaseOrder _PurchaseOrder)
        {
            PurchaseOrder _PurchaseOrderq = new PurchaseOrder();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.PurchaseOrder.Add(_PurchaseOrder);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _PurchaseOrder.PurchaseOrderLines)
                        {
                            item.PurchaseOrderId = _PurchaseOrder.Id;
                            _context.PurchaseOrderLine.Add(item);
                        }
                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                //_PurchaseOrderq = _PurchaseOrder;
                //_context.PurchaseOrder.Add(_PurchaseOrderq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrderq));
        }

        /// <summary>
        /// Actualiza la PurchaseOrder
        /// </summary>
        /// <param name="_PurchaseOrder"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<PurchaseOrder>> Update([FromBody]PurchaseOrder _PurchaseOrder)
        {
            PurchaseOrder _PurchaseOrderq = _PurchaseOrder;
            try
            {
                _PurchaseOrderq = await _context.PurchaseOrder.Where(w => w.Id == _PurchaseOrder.Id).Include( b => b.Branch).FirstOrDefaultAsync();

                _context.Entry(_PurchaseOrderq).CurrentValues.SetValues((_PurchaseOrder));



                if (_PurchaseOrderq.EstadoId == 6 )
                {
                    List<PurchaseOrderLine> purchaseOrderLines = await _context.PurchaseOrderLine.Where(w => w.PurchaseOrderId == _PurchaseOrderq.Id).ToListAsync();
                    foreach (var item in  purchaseOrderLines)
                    {
                        KardexViale kardexMax = await _context.KardexViale.Where(w => w.BranchId == _PurchaseOrderq.BranchId && w.ProducId == item.ProductId).OrderByDescending(d => d.KardexDate).FirstOrDefaultAsync();
                        KardexViale nuevokardex = new KardexViale();
                        if (kardexMax!= null)
                        {
                            nuevokardex = new KardexViale {
                                BranchId = Convert.ToInt64(_PurchaseOrderq.BranchId),
                                BranchName = _PurchaseOrderq.Branch.BranchName,
                                KardexDate = DateTime.Now,
                                ProducId = item.ProductId==null?0:Convert.ToInt64(item.ProductId),
                                ProductName = kardexMax.ProductName,
                                DocumentId = _PurchaseOrderq.Id,
                                SaldoAnterior = kardexMax.Total,
                                TypeOfDocumentId = 5,
                                TypeOfDocumentName = "Orden de Compra",
                                TypeOperationId = 1,
                                TypeOperationName = "Entrada",
                                QuantityEntry = Convert.ToDouble(item.QtyReceived),
                                QuantityOut = 0 ,
                                Total = Convert.ToDouble(kardexMax.Total + item.QtyReceived),
                                MinimumExistance = 1,
                                UsuarioCreacion = _PurchaseOrderq.UsuarioModificacion,
                                WareHouseId = 1,
                                WareHouseName = " "
                            };
                        }
                        else
                        {
                            nuevokardex = new KardexViale
                            {
                                BranchId = Convert.ToInt64(_PurchaseOrderq.BranchId),
                                BranchName = _PurchaseOrderq.Branch.BranchName,
                                KardexDate = DateTime.Now,
                                ProducId = item.ProductId == null ? 0 : Convert.ToInt64(item.ProductId),
                                ProductName = item.ProductDescription,
                                DocumentId = _PurchaseOrderq.Id,
                                SaldoAnterior = 0 ,
                                TypeOfDocumentId = 5,
                                TypeOfDocumentName = "Orden de Compra",
                                TypeOperationId = 1,
                                TypeOperationName = "Entrada",
                                QuantityEntry = Convert.ToDouble(item.QtyReceived),
                                QuantityOut = 0,
                                Total = Convert.ToDouble( item.QtyReceived),
                                MinimumExistance = 1,
                                UsuarioCreacion = _PurchaseOrderq.UsuarioModificacion,
                                WareHouseId = 1,
                                WareHouseName = " "
                            };
                        }
                        _context.KardexViale.Add(nuevokardex);
                       

                    }
                    await _context.SaveChangesAsync();
                }

                //_context.PurchaseOrder.Update(_PurchaseOrderq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrderq));
        }

        /// <summary>
        /// Elimina una PurchaseOrder       
        /// </summary>
        /// <param name="_PurchaseOrder"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]PurchaseOrder _PurchaseOrder)
        {
            PurchaseOrder _PurchaseOrderq = new PurchaseOrder();
            try
            {
                _PurchaseOrderq = _context.PurchaseOrder
                .Where(x => x.Id == (Int64)_PurchaseOrder.Id)
                .FirstOrDefault();

                _context.PurchaseOrder.Remove(_PurchaseOrderq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_PurchaseOrderq));

        }







    }
}
