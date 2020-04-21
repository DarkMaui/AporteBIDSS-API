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
    public class GrupoConfiguracionInteresesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public GrupoConfiguracionInteresesController(ILogger<GrupoConfiguracionInteresesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetGrupoConfiguracionIntereses()
        {
            List<GrupoConfiguracionIntereses> Items = new List<GrupoConfiguracionIntereses>();
            try
            {
                Items = await _context.GrupoConfiguracionIntereses.ToListAsync();
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
        public async Task<IActionResult> GetGrupoConfiguracionInteresesById(int Id)
        {
            GrupoConfiguracionIntereses Items = new GrupoConfiguracionIntereses();
            try
            {
                Items = await _context.GrupoConfiguracionIntereses.Where(q => q.Id.Equals(Id)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Descripcion}")]
        public async Task<IActionResult> GetGrupoConfiguracionInteresesByDescripcion(String Descripcion)
        {
            GrupoConfiguracionIntereses Items = new GrupoConfiguracionIntereses();
            try
            {
                Items = await _context.GrupoConfiguracionIntereses.Where(q => q.Nombre == Descripcion).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<GrupoConfiguracionIntereses>> Insert([FromBody]GrupoConfiguracionIntereses payload)
        {
            GrupoConfiguracionIntereses GrupoConfiguracionIntereses = payload;

            try
            {
                _context.GrupoConfiguracionIntereses.Add(GrupoConfiguracionIntereses);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(GrupoConfiguracionIntereses));
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<GrupoConfiguracionIntereses>> Update([FromBody]GrupoConfiguracionIntereses _GrupoConfiguracionIntereses)
        {

            try
            {
                GrupoConfiguracionIntereses GrupoConfiguracionInteresesq = (from c in _context.GrupoConfiguracionIntereses
                   .Where(q => q.Id == _GrupoConfiguracionIntereses.Id)
                                                            select c
                     ).FirstOrDefault();

                _GrupoConfiguracionIntereses.FechaCreacion = GrupoConfiguracionInteresesq.FechaCreacion;
                _GrupoConfiguracionIntereses.UsuarioCreacion = GrupoConfiguracionInteresesq.UsuarioCreacion;

                _context.Entry(GrupoConfiguracionInteresesq).CurrentValues.SetValues((_GrupoConfiguracionIntereses));
                // _context.FundingInterestRate.Update(_FundingInterestRate);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_GrupoConfiguracionIntereses));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]GrupoConfiguracionIntereses payload)
        {
            GrupoConfiguracionIntereses GrupoConfiguracionIntereses = new GrupoConfiguracionIntereses();
            try
            {
                GrupoConfiguracionIntereses = _context.GrupoConfiguracionIntereses
                .Where(x => x.Id == (int)payload.Id)
                .FirstOrDefault();
                _context.GrupoConfiguracionIntereses.Remove(GrupoConfiguracionIntereses);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(GrupoConfiguracionIntereses));

        }
    }
}
