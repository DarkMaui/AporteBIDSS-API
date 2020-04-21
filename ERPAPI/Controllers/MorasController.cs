using System;
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
    [Route("api/Moras")]
    [ApiController]
    public class MorasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public MorasController(ILogger<MorasController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Moras
        /// </summary>
        /// <returns></returns>  
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMora()

        {
            List<Moras> Items = new List<Moras>();
            try
            {
                Items = await _context.Moras.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
            //return await _context.Dimensions.ToListAsync();
        }

        /// <summary>
        /// Obtiene la sucursal mediante el Id enviado.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMorasById(int Id)
        {
            Moras Items = new Moras();
            try
            {
                Items = await _context.Moras.Where(q => q.Id == Id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {



                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }



            return await Task.Run(() => Ok(Items));
        }



        [HttpGet("[action]/{Nombre}")]
        public async Task<IActionResult> GetMorasByName(String Nombre)
        {
            Moras Items = new Moras();
            try
            {
                Items = await _context.Moras.Where(q => q.Nombre == Nombre).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una sucursal
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Insert([FromBody]Moras payload)
        {
            Moras mora = new Moras();
            try
            {
                mora = payload;
                _context.Moras.Add(mora);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(mora));
        }



        [HttpPut("[action]")]
        public async Task<ActionResult<Moras>> Update([FromBody]Moras _Mora)
        {
            Moras _Moraq = _Mora;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _Moraq = await (from c in _context.Moras
                                         .Where(q => q.Id == _Mora.Id)
                                          select c
                                        ).FirstOrDefaultAsync();

                        _context.Entry(_Moraq).CurrentValues.SetValues((_Mora));

                        //_context.Alert.Update(_Alertq);
                        await _context.SaveChangesAsync();
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _Mora.Id,
                            DocType = "Moras",
                            ClaseInicial =
                              Newtonsoft.Json.JsonConvert.SerializeObject(_Moraq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_Mora, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Actualizar",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _Mora.UsuarioCreacion,
                            UsuarioModificacion = _Mora.UsuarioModificacion,
                            UsuarioEjecucion = _Mora.UsuarioModificacion,

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

            return await Task.Run(() => Ok(_Moraq));

        }
        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="_Moras"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Moras _Moras)
        {
            Moras mora = new Moras();
            try
            {

                mora = _context.Moras
               .Where(x => x.Id == (int)_Moras.Id)
               .FirstOrDefault();
                _context.Moras.Remove(mora);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run(() => Ok(mora));



        }

    }
}