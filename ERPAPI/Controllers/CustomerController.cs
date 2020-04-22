﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using ERP.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
   // [Produces("application/json")]
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
         private readonly ILogger _logger;

        public CustomerController(ILogger<CustomerController> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de CustomerDocument paginado
        /// </summary>
        /// <returns></returns>    
        [HttpGet("[action]")]
        public async Task<IActionResult> GetCustomerPag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<Customer> Items = new List<Customer>();
            try
            {
                var query = _context.Customer.AsQueryable();
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
        /// Obtiente el listado de todos los clientes.
        /// </summary>
        /// <returns></returns>
        // GET: api/Customer
        [HttpGet("[action]")]
        public async Task<ActionResult<List<Customer>>> GetCustomer()
        {

            try
            {
                List<Customer> Items = await _context.Customer.OrderByDescending(c => c.CustomerId).Take(100).ToListAsync();
                return await Task.Run(() => Ok(Items));
                //  return Ok(Items);
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityCustomer()
        {

            try
            {
                var Items = await _context.Customer.CountAsync();
                return await Task.Run(() => Ok(Items));
                //  return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }



        /// <summary>
        /// Obtiene un cliente , filtrado por su id.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerById/{CustomerId}")]
        public async Task<ActionResult> GetCustomerById(Int64 CustomerId)
        {
            try
            {
                Customer Items = await _context.Customer.Where(q => q.CustomerId == CustomerId).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));
                //return Ok(Items);
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
         
        }


        /// <summary>
        /// Obtiene un cliente , filtrado por su RTN.
        /// </summary>
        /// <param name="RTN"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerByRTN/{RTN}")]
        public async Task<ActionResult> GetCustomerByRTN(string RTN)
        {
            try
            {
                Customer Items = await _context.Customer.Where(q => q.RTN == RTN).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));
                //return Ok(Items);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        /// <summary>
        /// Agrega un nuevo usuario con los datos proporcionados , el CustomerId es un identity.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Customer>> Insert([FromBody]Customer payload)
        {

            try
            {
                Customer customer = payload;
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //string json = JsonConvert.SerializeObject(payload);
                        payload.CityId = payload.CityId == 0 ? null : payload.CityId;
                        payload.CountryId = payload.CountryId == 0 ? null : payload.CountryId;
                        payload.StateId = payload.StateId == 0 ? null : payload.StateId;
                        payload.IdEstado = payload.IdEstado == 0 ? null : payload.IdEstado;
                        payload.CustomerTypeId = payload.CustomerTypeId == 0 ? null : payload.CustomerTypeId;

                        payload.FechaCreacion = DateTime.Now;
                        payload.FechaModificacion = DateTime.Now;
                        _context.Customer.Add(customer);
                        await _context.SaveChangesAsync();

                       

                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }              
               


                // return (customer);
                return await Task.Run(() => Ok(customer));
            }
            catch (Exception ex)
            {
                  _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }
           
        }

        /// <summary>
        /// Actualiza un cliente con el CustomerId y datos del cliente proporcionados.
        /// </summary>
        /// <param name="_customer"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Customer>> Update([FromBody]Customer _customer)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        _customer.CityId = _customer.CityId == 0 ? null : _customer.CityId;
                        _customer.CountryId = _customer.CountryId == 0 ? null : _customer.CountryId;
                        _customer.StateId = _customer.StateId == 0 ? null : _customer.StateId;
                        _customer.IdEstado = _customer.IdEstado == 0 ? null : _customer.IdEstado;
                        _customer.CustomerTypeId = _customer.CustomerTypeId == 0 ? null : _customer.CustomerTypeId;

                        Customer customerq = (from c in _context.Customer
                                     .Where(q => q.CustomerId == _customer.CustomerId)
                                              select c
                                   ).FirstOrDefault();

                        _customer.FechaCreacion = customerq.FechaCreacion;
                        _customer.UsuarioCreacion = customerq.UsuarioCreacion;

                        //_context.Customer.Update(_customer);

                        _context.Entry(customerq).CurrentValues.SetValues((_customer));
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

                // return (customer);
                return await Task.Run(() => Ok(_customer));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }

        /// <summary>
        /// Elimina un cliente con el CustomerId proporcionado.
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Customer>> Remove([FromBody]Customer payload)
        {

            try
            {
                Customer customer = _context.Customer
               .Where(x => x.CustomerId == (Int64)payload.CustomerId)
               .FirstOrDefault();
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                // return (customer);
                return await Task.Run(() => Ok(customer));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
                //return BadRequest($"Ocurrio un error:{ex.Message}");
            }
           

        }
       
    }
}