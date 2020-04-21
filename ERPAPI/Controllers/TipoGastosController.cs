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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoGastosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public TipoGastosController(ILogger<TipoGastosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetTipoGastos()
        {
            List<TipoGastos> Items = new List<TipoGastos>();
            try
            {
                Items = await _context.TipoGastos.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetTipoGastosById(int Id)
        {
            TipoGastos Items = new TipoGastos();
            try
            {
                Items = await _context.TipoGastos.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Descripcion}")]
        public async Task<IActionResult> GetTipoGastosByDescripcion(String Descripcion)
        {
            TipoGastos Items = new TipoGastos();
            try
            {
                Items = await _context.TipoGastos.Where(q => q.Descripcion == Descripcion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<TipoGastos>> Insert([FromBody]TipoGastos payload)
        {
            TipoGastos TipoGastos = payload;

            try
            {
                _context.TipoGastos.Add(TipoGastos);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(TipoGastos));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<TipoGastos>> Update([FromBody]TipoGastos _TipoGastos)
        {

            try
            {
                TipoGastos TipoGastosq = (from c in _context.TipoGastos
                   .Where(q => q.Id == _TipoGastos.Id)
                                                                            select c
                     ).FirstOrDefault();

                _TipoGastos.FechaCreacion = TipoGastosq.FechaCreacion;
                _TipoGastos.UsuarioCreacion = TipoGastosq.UsuarioCreacion;

                _context.Entry(TipoGastosq).CurrentValues.SetValues((_TipoGastos));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_TipoGastos));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]TipoGastos payload)
        {
            TipoGastos TipoGastos = new TipoGastos();
            try
            {
                TipoGastos = _context.TipoGastos
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.TipoGastos.Remove(TipoGastos);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(TipoGastos));
        }
    }
}