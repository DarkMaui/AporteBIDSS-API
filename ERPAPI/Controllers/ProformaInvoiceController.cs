﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/ProformaInvoice")]
    [ApiController]
    public class ProformaInvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ProformaInvoiceController(ILogger<ProformaInvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Obtiene el Listado de ProformaInvoice paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProformaInvoicePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                var query = _context.ProformaInvoice.AsQueryable();
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
        /// Obtiene el Listado de ProformaInvoicees 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProformaInvoice()
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                Items = await _context.ProformaInvoice.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetProformaInvoiceByCustomer(Int64 CustomerId)
        {
            List<ProformaInvoice> Items = new List<ProformaInvoice>();
            try
            {
                Items = await _context.ProformaInvoice.Where(q=>q.CustomerId==CustomerId).ToListAsync();
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
        /// Obtiene los Datos de la ProformaInvoice por medio del Id enviado.
        /// </summary>
        /// <param name="ProformaInvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{ProformaInvoiceId}")]
        public async Task<IActionResult> GetProformaInvoiceById(Int64 ProformaInvoiceId)
        {
            ProformaInvoice Items = new ProformaInvoice();
            try
            {
                Items = await _context.ProformaInvoice.Where(q => q.ProformaId == ProformaInvoiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene los Datos de la ProformaInvoice por medio del Id enviado.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetLastProformaInvoice(Int64 CustomerId)
        {
            ProformaInvoice Items = new ProformaInvoice();
            try
            {
                Items = await _context.ProformaInvoice
                              .OrderByDescending(q=>q.ProformaId)
                              .Where(q => q.CustomerId == CustomerId)
                             .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetProformaInvoiceLineById([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice Items = new ProformaInvoice();
            try
            {
                    Items = await _context.ProformaInvoice.Include(q => q.ProformaInvoiceLine).Where(q => q.ProformaId == _ProformaInvoice.ProformaId).FirstOrDefaultAsync();


            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{ProformaInvoiceId}")]
        public async Task<IActionResult> GetInvoiceCalculation(Int64 ProformaInvoiceId)
        {
            List< InvoiceCalculation> Items = new List<InvoiceCalculation>();
            try
            {
                Items = await _context.InvoiceCalculation.Where(q => q.ProformaInvoiceId == ProformaInvoiceId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }


        [HttpGet("[action]/{Identificador}")]
        public async Task<IActionResult> GetInvoiceCalculationByIdentificador(Guid Identificador)
        {
            List<InvoiceCalculation> Items = new List<InvoiceCalculation>();
            try
            {
                Items = await _context.InvoiceCalculation.Where(q => q.Identificador == Identificador).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


            return await Task.Run(() => Ok(Items));
        }





        [HttpPost("[action]")]
        public async Task<ActionResult<ProformaInvoice>> InsertWithInventory([FromBody]ProformaInvoiceDTO _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = _ProformaInvoice;

           
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.ProformaInvoice.Add(_ProformaInvoiceq);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _ProformaInvoice.ProformaInvoiceLine)
                        {
                            

                            item.ProformaInvoiceId = _ProformaInvoice.ProformaId;
                           

                            //KardexViale items2 = new KardexViale();

                            //items2 = await _context.KardexViale.Where(q => q.ProducId == item.ProductId && q.BranchId == _ProformaInvoice.BranchId).OrderByDescending(o => o.KardexDate).FirstOrDefaultAsync();
                            
                            //KardexViale kardexViale = items2;

                            //kardexViale.QuantityOut = item.Quantity;
                            //kardexViale.QuantityEntry = 0;
                            //kardexViale.SaldoAnterior = kardexViale.Total;
                            //kardexViale.Total = kardexViale.Total - item.Quantity;
                            //kardexViale.Id = 0;
                            //kardexViale.KardexDate = DateTime.Now;
                            //kardexViale.TypeOperationId = 2;
                            //kardexViale.TypeOperationName = "Salida";
                            //kardexViale.UsuarioCreacion = _ProformaInvoice.UsuarioCreacion;
                            //kardexViale.BranchId = _ProformaInvoice.BranchId;
                            //kardexViale.TypeOfDocumentId = 1;
                            //kardexViale.TypeOfDocumentName = "Factura al Contado";
                            //kardexViale.DocumentId = item.ProformaInvoiceId;

                            _context.ProformaInvoiceLine.Add(item);
                           // _context.KardexViale.Add(kardexViale);
                        }

                        await _context.SaveChangesAsync();


                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ProformaInvoice.CustomerId,
                            DocType = "ProformaInvoice",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ProformaInvoice.UsuarioCreacion,
                            UsuarioModificacion = _ProformaInvoice.UsuarioModificacion,
                            UsuarioEjecucion = _ProformaInvoice.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();


                       
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));
        }




        /// <summary>
        /// Inserta una nueva ProformaInvoice
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ProformaInvoice>> Insert([FromBody]ProformaInvoiceDTO _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.ProformaInvoice.Add(_ProformaInvoice);
                        //await _context.SaveChangesAsync();

                        foreach (var item in _ProformaInvoice.ProformaInvoiceLine)
                        {
                            item.ProformaInvoiceId = _ProformaInvoice.ProformaId;
                            _context.ProformaInvoiceLine.Add(item);

                            
                        }

                        _context.Entry(_ProformaInvoiceq).CurrentValues.SetValues((_ProformaInvoice));

                        await _context.SaveChangesAsync();

                        //   if(_ProformaInvoice.Identificador!="")
                        var calculo =await _context.InvoiceCalculation.Where(q => q.Identificador == _ProformaInvoice.Identificador).ToListAsync();
                         calculo.ForEach(q => q.ProformaInvoiceId = _ProformaInvoice.ProformaId);

                        await _context.SaveChangesAsync();


                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _ProformaInvoice.CustomerId,
                            DocType = "ProformaInvoice",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_ProformaInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _ProformaInvoice.UsuarioCreacion,
                            UsuarioModificacion = _ProformaInvoice.UsuarioModificacion,
                            UsuarioEjecucion = _ProformaInvoice.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                //_ProformaInvoiceq = _ProformaInvoice;
                //_context.ProformaInvoice.Add(_ProformaInvoiceq);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
        
            return await Task.Run(() => Ok(_ProformaInvoice));
        }

        /// <summary>
        /// Actualiza la ProformaInvoice
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<ProformaInvoice>> Update([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = _ProformaInvoice;
            try
            {
                _ProformaInvoiceq = await (from c in _context.ProformaInvoice
                                 .Where(q => q.ProformaId == _ProformaInvoice.ProformaId)
                                           select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_ProformaInvoiceq).CurrentValues.SetValues((_ProformaInvoice));

                //_context.ProformaInvoice.Update(_ProformaInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));
        }

        /// <summary>
        /// Elimina una ProformaInvoice       
        /// </summary>
        /// <param name="_ProformaInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]ProformaInvoice _ProformaInvoice)
        {
            ProformaInvoice _ProformaInvoiceq = new ProformaInvoice();
            try
            {
                _ProformaInvoiceq = _context.ProformaInvoice
                .Where(x => x.ProformaId == (Int64)_ProformaInvoice.ProformaId)
                .FirstOrDefault();

                _context.ProformaInvoice.Remove(_ProformaInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_ProformaInvoiceq));

        }







    }
}