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
    public class MotivosAjusteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public MotivosAjusteController(ILogger<MotivosAjusteController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetMotivosAjuste()
        {
            List<MotivosAjuste> Items = new List<MotivosAjuste>();
            try
            {
                Items = await _context.MotivosAjuste.Include(q=>q.Estados).ToListAsync();
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
        public async Task<IActionResult> GetMotivosAjustesById(int Id)
        {
            MotivosAjuste Items = new MotivosAjuste();
            try
            {
                Items = await _context.MotivosAjuste.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Descripcion}")]
        public async Task<IActionResult> GetMotivosAjusteByDescripcion(String Descripcion)
        {
            MotivosAjuste Items = new MotivosAjuste();
            try
            {
                Items = await _context.MotivosAjuste.Where(q => q.Descripcion == Descripcion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<MotivosAjuste>> Insert([FromBody]MotivosAjuste payload)
        {
            MotivosAjuste MotivosAjuste = payload;

            try
            {
                _context.MotivosAjuste.Add(MotivosAjuste);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(MotivosAjuste));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<MotivosAjuste>> Update([FromBody]MotivosAjuste MotivosAjuste)
        {
            try
            {
                MotivosAjuste MotivosAjusteq = (from c in _context.MotivosAjuste
                   .Where(q => q.Id == MotivosAjuste.Id)
                                          select c
                     ).FirstOrDefault();

                MotivosAjuste.FechaCreacion = MotivosAjusteq.FechaCreacion;
                MotivosAjuste.UsuarioCreacion = MotivosAjusteq.UsuarioCreacion;

                _context.Entry(MotivosAjusteq).CurrentValues.SetValues((MotivosAjuste));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(MotivosAjuste));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]MotivosAjuste payload)
        {
            MotivosAjuste MotivosAjuste = new MotivosAjuste();
            try
            {
                MotivosAjuste = _context.MotivosAjuste
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.MotivosAjuste.Remove(MotivosAjuste);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(MotivosAjuste));
        }
    }
}