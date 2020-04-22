﻿/********************************************************************************************************
-- NAME   :  CRUDCurency
-- PROPOSE:  show relation Moneda
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
12.0                 22/12/2019          Marvin.Guillen                     Validation to eliminate
11.0                 18/12/2019          Marvin.Guillen                     Validation to eliminate
10.0                 23/11/2019          Marvin.Guillen                     Changes of Currency
9.0                  21/11/2019          Marvin.Guillen                     Changes of Currency
8.0                  16/09/2019          Freddy.Chinchilla                  Changes of Currency COntroller
7.0                  19/06/2019          Freddy.Chinchilla                  Changes of Task Return
6.0                  27/05/2019          Freddy.Chinchilla                  Changes of Currency COntroller
5.0                  30/04/2019          Freddy.Chinchilla                  Changes of Add Colums
4.0                  26/04/2019          Freddy.Chinchilla                  Changes of Get by Id
3.0                  24/04/2019          Freddy.Chinchilla                  Changes of Mejoras de controllers
2.0                  22/04/2019          Freddy.Chinchilla                  Changes of Numeracion SAR
1.0                  11/04/2019          Freddy.Chinchilla                  Creation of Controller
********************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ERPAPI.Controllers
{
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    [Route("api/Currency")]
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CurrencyController(ILogger<CurrencyController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Currency paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCurrencyPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Currency> Items = new List<Currency>();
            try
            {
                var query = _context.Currency.AsQueryable();
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
        /// Obtiene el listado de Monedas.
        /// </summary>
        /// <returns></returns>
        // GET: api/Currency
        [HttpGet("[action]")]
        public async Task<ActionResult<Currency>> GetCurrency()
        {
            List<Currency> Items = new List<Currency>();
            try
            {
               Items =  await _context.Currency.ToListAsync();
               // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return await Task.Run(() => Ok( Items));
        }

        /// <summary>
        /// Obtiene los datos de la moneda con el id enviado
        /// </summary>
        /// <param name="CurrencyId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CurrencyId}")]
        public async Task<ActionResult<Currency>> GetCurrencyById(int CurrencyId)
        {
            Currency Items = new Currency();
            try
            {
                Items = await _context.Currency.Where(q=>q.CurrencyId== CurrencyId).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }

        [HttpGet("[action]/{CurrencyId}")]
        public async Task<ActionResult<Int32>> ValidationDelete(int CurrencyId)
        {
            try
            {
                //var Items = await _context.Product.CountAsync();
                Int32 Items = await _context.Branch
                                    .CountAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        /// <summary>
        /// Obtiene los datos de la moneda con el id enviado
        /// </summary>
        /// <param name="CurrencyName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{CurrencyName}")]
        public async Task<ActionResult<Currency>> GetCurrencyByCurrencyName(string CurrencyName)
        {
            Currency Items = new Currency();
            try
            {
                Items = await _context.Currency.Where(q => q.CurrencyName == CurrencyName).FirstOrDefaultAsync();
                // int Count = Items.Count();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }



            return Ok(Items);
        }

        /// <summary>
        /// Inserta la moneda 
        /// </summary>
        /// <param name="_Currency"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Currency>> Insert([FromBody]Currency _Currency)
        {
            Currency currency = _Currency;
            try
            {
                _context.Currency.Add(currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
               return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(currency));
        }

        /// <summary>
        /// Actualiza la moneda
        /// </summary>
        /// <param name="_Currency"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Currency>> Update([FromBody]Currency _Currency)
        {
          
            try
            {
                Currency currencyq  = (from c in _context.Currency
                                        .Where(q => q.CurrencyId == _Currency.CurrencyId)
                                        select c
                                      ).FirstOrDefault();

                _Currency.FechaCreacion = currencyq.FechaCreacion;
                _Currency.UsuarioCreacion = currencyq.UsuarioCreacion;

                _context.Entry(currencyq).CurrentValues.SetValues((_Currency));
                //  _context.Currency.Update(_Currency);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Currency));
        }

        /// <summary>
        /// Elimina la moneda
        /// </summary>
        /// <param name="_Currency"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Currency>> Delete([FromBody]Currency _Currency)
        {
            Currency currency = new Currency();
            try
            {
                bool flag = false;
               
                
                var VariableBranch = _context.Branch
                                    .FirstOrDefault();
                if (VariableBranch == null)
                {
                    flag = true;
                }
                
                var VariableEmployees = _context.Employees.Where(a => a.IdCurrency == _Currency.CurrencyId)
                                                    .FirstOrDefault();
                if (VariableEmployees == null)
                {
                    flag = true;
                }
                if (flag) { 
                         currency = _context.Currency
                        .Where(x => x.CurrencyId == (int)_Currency.CurrencyId)
                        .FirstOrDefault();
                         _context.Currency.Remove(currency);
                        await  _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                 _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                 return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(currency));

        }
      



    }
}