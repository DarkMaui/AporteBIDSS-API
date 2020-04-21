using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Contrato")]
    [ApiController]
    public class ContratoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ContratoController(ILogger<ContratoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        // GET: /<controller>/
       
        [HttpGet("[action]/{ContratoId}")]
        public async Task<ActionResult<Contrato>> GetContratoById(Int64 ContratoId)
        {
            Contrato Items = new Contrato();
            try
            {
                Items = await _context.Contrato.Include(q => q.Contrato_detalle).Where(q => q.ContratoId == ContratoId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }

        /// <param name="_Contrato"></param>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato>> GetContratoByBranch([FromBody]Contrato _Contrato)
        {
            List<Contrato> Items = new List<Contrato>();
            try
            {
                Items = await _context.Contrato.Include(q => q.Branch).Include(q => q.Customer).Where(q => q.BranchId == _Contrato.BranchId && (q.Fecha_inicio >= _Contrato.Proxima_fecha_de_pago && q.Fecha_inicio <= _Contrato.Ultima_fecha_de_pago) && (q.Dias_mora >= _Contrato.IdEmpleado && q.Dias_mora <= _Contrato.CustomerId)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetContratoByCustomerId(Int64 CustomerId)
        {
            Contrato Items = new Contrato();
            try
            {
                Items = await _context.Contrato.Where(q => q.CustomerId == CustomerId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetContratoPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Contrato> Items = new List<Contrato>();
            try
            {
                var query = _context.Contrato.AsQueryable();
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

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Inserta una nueva Contrato
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato>> Insert([FromBody]Contrato _Contrato)
        {
            Contrato _Contratoq = _Contrato;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Contrato.Add(_Contratoq);
                        foreach (var item in _Contrato.Contrato_detalle)
                        {
                            item.ContratoId = _Contrato.ContratoId;
                            _context.Contrato_detalle.Add(item);
                        }
                        await _context.SaveChangesAsync();
                        //Para agregar plan de pago
                        var plazo = _Contratoq.Plazo;
                        if (plazo > 0)
                        {
                            for (var i = 1; i <= plazo; i++)
                            {
                                var plan = new Contrato_plan_pagos();
                                plan.ContratoId = _Contratoq.ContratoId;
                                plan.Fechacuota = DateTime.Now.AddMonths(i);
                                plan.Valorcapital = 0;
                                plan.Valorintereses = 0;
                                plan.Valorseguros = 0;
                                plan.Interesesmoratorios = 0;
                                plan.Valorotroscargos = 0;
                                plan.Estadocuota = 0;
                                plan.Valorpagado = 0;
                                plan.Fechapago = DateTime.Now.AddMonths(i).AddDays(2);
                                plan.Recibopago = "ninguno";
                                plan.UsuarioCreacion = "user";
                                plan.UsuarioModificacion = "user";
                                plan.CreatedDate = DateTime.Now;
                                plan.ModifiedDate = DateTime.Now;
                                _context.Contrato_plan_pagos.Add(plan);
                                await _context.SaveChangesAsync();
                            }
                        }
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contratoq.ContratoId,
                            DocType = "Contrato",

                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Contratoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contratoq.UsuarioCreacion,
                            UsuarioModificacion = _Contratoq.UsuarioModificacion,
                            UsuarioEjecucion = _Contratoq.UsuarioModificacion,
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
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return await Task.Run(() => Ok(_Contratoq));
        }
        /// <summary>
        /// Inserta una nueva Contrato, con informacion consolidad de detalle y plan de pagos
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato>> InsertConsolidado([FromBody]ContratoDTO _Contrato)
        {
            Contrato _Contratoq = new Contrato();
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contratoq = _Contrato.Contrato;
                        _context.Contrato.Add(_Contratoq);
                        await _context.SaveChangesAsync();

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contratoq.ContratoId,
                            DocType = "Contrato",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_Contratoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contratoq.UsuarioCreacion,
                            UsuarioModificacion = _Contratoq.UsuarioModificacion,
                            UsuarioEjecucion = _Contratoq.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();


                        Contrato_detalle _contratodetalle = new Contrato_detalle();
                        foreach (Contrato_detalle _cd in _Contratoq.Contrato_detalle)
                        {
                            _contratodetalle = _cd;
                            _contratodetalle.Contrato = _Contratoq;
                            _context.Contrato_detalle.Add(_contratodetalle);
                            await _context.SaveChangesAsync();

                            _write = new BitacoraWrite(_context, new Bitacora
                            {
                                IdOperacion = _contratodetalle.Contrato_detalleId,
                                DocType = "Contrato_detalle",
                                ClaseInicial =
                                Newtonsoft.Json.JsonConvert.SerializeObject(_contratodetalle, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                Accion = "Insertar",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = _contratodetalle.UsuarioCreacion,
                                UsuarioModificacion = _contratodetalle.UsuarioModificacion,
                                UsuarioEjecucion = _contratodetalle.UsuarioModificacion,

                            });

                            await _context.SaveChangesAsync();
                        }

                        Contrato_plan_pagos _Contrato_plan_pagosq = new Contrato_plan_pagos();

                        foreach(Contrato_plan_pagos _cpp in _Contratoq.Contrato_plan_pagos)
                        {
                            _Contrato_plan_pagosq = _cpp;
                            _Contrato_plan_pagosq.Contrato = _Contratoq;
                            _context.Contrato_plan_pagos.Add(_Contrato_plan_pagosq);
                            await _context.SaveChangesAsync();

                            _write = new BitacoraWrite(_context, new Bitacora
                            {
                                IdOperacion = _Contrato_plan_pagosq.Nro_cuota,
                                DocType = "Contrato_plan_pagos",
                                ClaseInicial =
                                Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato_plan_pagosq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                                Accion = "Insertar",
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = _Contrato_plan_pagosq.UsuarioCreacion,
                                UsuarioModificacion = _Contrato_plan_pagosq.UsuarioModificacion,
                                UsuarioEjecucion = _Contrato_plan_pagosq.UsuarioModificacion,

                            });
                        }

                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Contratoq));
        }

        /// <summary>
        /// Actualiza  Contrato
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPut("[action]")]

        public async Task<ActionResult<Contrato>> Update([FromBody]Contrato _Contrato)
        {
            Contrato _Contratoq = _Contrato;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Contratoq = await (from c in _context.Contrato
                                         .Where(q => q.ContratoId == _Contrato.ContratoId)
                                            select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Contratoq).CurrentValues.SetValues((_Contrato));

                        //_context.Contrato.Update(_Contratoq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Contrato.ContratoId,
                            DocType = "Contrato",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Contratoq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Contrato, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insertar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Contrato.UsuarioCreacion,
                            UsuarioModificacion = _Contrato.UsuarioModificacion,
                            UsuarioEjecucion = _Contrato.UsuarioModificacion,

                        });

                        await _context.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contratoq));
        }

        /// <summary>
        /// Elimina  Contrato       
        /// </summary>
        /// <param name="_Contrato"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Contrato>> Delete([FromBody]Contrato _Contrato)
        {
            Contrato _Contratoq = new Contrato();
            try
            {
                _Contratoq = _context.Contrato
                .Where(x => x.ContratoId == (Int64)_Contrato.ContratoId)
                .FirstOrDefault();

                _context.Contrato.Remove(_Contratoq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Contratoq));
        }
        //get para retornar en el grid
        [HttpGet("[action]")]
        public async Task<IActionResult> GetContrato()
        {
            List<Contrato> Items = new List<Contrato>();
            try
            {
                Items = await _context.Contrato.ToListAsync();
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
        /// 
        /// </summary>
        /// 
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetContratoPendientesByCustomerId(Int64 CustomerId)
        {

            try
            {
                //var resultsQueryable = from a in _context.Contrato 
                //                       //join c in _context.Contrato_plan_pagos on a.ContratoId equals c.ContratoId into acGroup
                //                       //from ac in acGroup
                //                       where a.ContratoId == CustomerId //&& a.Estado != 1
                //                       select new
                //                       {
                //                           a.Fecha,
                //                           a.ContratoId,
                //                           a.TotalContrato,
                //                           a.Plazo,
                //                           a.ValorCuota,
                //                           a.Fecha_de_vencimiento
                //                       };
                //var Item = resultsQueryable.ToList();
                var Item = await (from a in _context.Contrato
                                  where a.CustomerId == CustomerId && a.TotalContrato > 0
                                  select new { a.Fecha_inicio, a.ContratoId, a.ValorContado, a.ValorPrima, a.SaldoCredito, a.Plazo, a.ValorCuota, a.Fecha_de_vencimiento }).ToListAsync();
                                  // select new { a, b, c }).ToListAsync();
                return await Task.Run(() => Ok(Item));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            //return await Task.Run(() => Ok(Items));
        }
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetContratoPagadosByCustomerId(Int64 CustomerId)
        {
            try
            {
                var Item = await (from a in _context.Contrato
                                  where a.CustomerId == CustomerId && a.TotalContrato == 0
                                  select new { a.Fecha_inicio, a.ContratoId, a.ValorContado, a.ValorPrima, a.SaldoCredito, a.Plazo, a.ValorCuota, a.Fecha_de_vencimiento }).ToListAsync();
                // select new { a, b, c }).ToListAsync();
                return await Task.Run(() => Ok(Item));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
        }

        /// <summary>
        /// Obtine los contratos pendientes y pagados
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CustomerId}")]
        public async Task<IActionResult> GetContratoPendientesAndPagadosByCustomerId(Int64 CustomerId)
        {
            List<Contrato> Items = new List<Contrato>();
            Contrato Item = new Contrato();
            Contrato_plan_pagos ItemPlanPagos = new Contrato_plan_pagos();
            try
            {
                var ContratoPedientesAndPagados = "";
                ContratoPedientesAndPagados =
                $"SELECT cont.ContratoId as IDContrato, cont.[Fecha] as Fecha, cont.[TotalContrato] as TotalContrato, cont.[ValorPrima] as ValorPrima, cont.[Plazo] as Plazo, cont.[ValorCuota] as ValorCuota," +
                $"cont.[Fecha_de_vencimiento] as FechaVencimiento, cont.[SinFinaciar] as SinFinaciar, cont.[ValorContado] as ValorContado, cont.[SaldoCredito] as SaldoCredito, [Contrato_plan_pagos].[Fechacuota] as FechaCuota, " +
                $"IsNull([Contrato_plan_pagos].[Contrato_movimientosId], 0) as ContratoMovimiento, cont.[NombreEstado] as Estado " +
                $"FROM [dbo].[Contrato] as Cont INNER JOIN [dbo].[Contrato_plan_pagos] ON cont.[ContratoId] = [Contrato_plan_pagos].[ContratoId] " +
                $"WHERE cont.[CustomerId] = '{CustomerId}' AND [Contrato_plan_pagos].[Nro_cuota] IN (SELECT TOP(1)[Contrato_plan_pagos].[Nro_cuota] AS NRO " +
                $"																				FROM [dbo].[Contrato_plan_pagos] " +
                $"																				WHERE [Contrato_plan_pagos].ContratoId IN (cont.[ContratoId]) " +
                $"																				GROUP BY [Contrato_plan_pagos].[ContratoId], [Contrato_plan_pagos].[Nro_cuota], [Contrato_plan_pagos].[Fechacuota] " +
                $"																				ORDER BY [Contrato_plan_pagos].[Fechacuota] DESC) ";

                using (var dr = await _context.Database.ExecuteSqlQueryAsync(ContratoPedientesAndPagados))
                {
                    var reader = dr.DbDataReader;
                    while (reader.Read())
                    {
                        var Index = 0;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Item.FechaModificacion = DateTime.Now;
                            Item.ContratoId = Convert.ToInt64(reader[Index]);
                            Item.Fecha = Convert.ToDateTime(reader[Index + 1]);
                            Item.TotalContrato = Convert.ToDouble(reader[Index + 2]);
                            Item.ValorPrima = Convert.ToDouble(reader[Index + 3]);
                            Item.Plazo = Convert.ToInt32(reader[Index + 4]);
                            Item.ValorCuota = Convert.ToDouble(reader[Index + 5]);
                            Item.Fecha_de_vencimiento = Convert.ToDateTime(reader[Index + 6]);
                            Item.SinFinaciar = Convert.ToDouble(reader[Index + 7]);
                            Item.ValorContado = Convert.ToDouble(reader[Index + 8]);
                            Item.SaldoCredito = Convert.ToDouble(reader[Index + 9]);
                            ItemPlanPagos.ContratoId = Convert.ToInt64(reader[Index]);
                            ItemPlanPagos.Fechacuota = Convert.ToDateTime(reader[Index + 10]);
                            ItemPlanPagos.Contrato_movimientosId = Convert.ToInt32(reader[Index + 11]);
                            Item.NombreEstado = Convert.ToString(reader[Index + 12]);
                            break;
                        }
                        Items.Add(new Contrato() { ContratoId = Item.ContratoId, Fecha = Item.Fecha, TotalContrato = Item.TotalContrato, Plazo = Item.Plazo, ValorPrima = Item.ValorPrima
                                                    , ValorCuota = Item.ValorCuota, Fecha_de_vencimiento = Item.Fecha_de_vencimiento, FechaModificacion = Item.FechaModificacion
                                                    , SinFinaciar = Item.SinFinaciar, ValorContado = Item.ValorContado, SaldoCredito = Item.SaldoCredito, NombreEstado = Item.NombreEstado
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }
    }
}
