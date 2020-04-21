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
    public class RegistroGastosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public RegistroGastosController(ILogger<RegistroGastosController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetRegistroGastos()
        {
            List<RegistroGastos> Items = new List<RegistroGastos>();
            try
            {
                Items = await _context.RegistroGastos.ToListAsync();
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
        public async Task<IActionResult> GetRegistroGastosById(int Id)
        {
            RegistroGastos Items = new RegistroGastos();
            try
            {
                Items = await _context.RegistroGastos.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult<RegistroGastos>> Insert([FromBody]RegistroGastos payload)
        {
            RegistroGastos RegistroGastos = payload;

            try
            {
                _context.RegistroGastos.Add(RegistroGastos);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(RegistroGastos));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<RegistroGastos>> Update([FromBody]RegistroGastos _RegistroGastos)
        {

            try
            {
                RegistroGastos _RegistroGastosQ = (from c in _context.RegistroGastos
                   .Where(q => q.Id == _RegistroGastos.Id)
                                          select c
                     ).FirstOrDefault();

                _RegistroGastos.FechaCreacion = _RegistroGastosQ.FechaCreacion;
                _RegistroGastos.UsuarioCreacion = _RegistroGastosQ.UsuarioCreacion;

                _context.Entry(_RegistroGastosQ).CurrentValues.SetValues((_RegistroGastos));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_RegistroGastos));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]RegistroGastos payload)
        {
            RegistroGastos RegistroGastos = new RegistroGastos();
            try
            {
                RegistroGastos = _context.RegistroGastos
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.RegistroGastos.Remove(RegistroGastos);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(RegistroGastos));
        }
    }
}