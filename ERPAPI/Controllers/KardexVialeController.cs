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
    public class KardexVialeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public KardexVialeController(ILogger<KardexVialeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("[action]/{ProductId}")]
        public async Task<IActionResult> Get(Int64 ProductId)
        {
            List<KardexViale> Items = new List<KardexViale>();
            try
            {
                Items = await _context.KardexViale.Where(q => q.ProducId == ProductId).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            
            return await Task.Run(() => Ok(Items));
        }
        [HttpGet("[action]/{SourceBranchId}")]
        public async Task<IActionResult> GetKardexViale(Int64 SourceBranchId)
        {
            List<KardexViale> Items = new List<KardexViale>();
            try
            {
                Items = await _context.KardexViale.Where(q => q.BranchId == SourceBranchId).ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        [HttpGet("[action]/{Id}/{BranchId}")]
        public async Task<IActionResult> GetKardexByProductId(int Id, int BranchId)
        {
            KardexViale Items = new KardexViale();
            try
            {
                Items = await _context.KardexViale.Where(q => q.ProducId == Id&& q.BranchId==BranchId).OrderByDescending(o => o.KardexDate).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(Items));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<KardexViale>> Insert([FromBody]KardexViale payload)
        {
            KardexViale KardexViale = payload;
            try
            {
                _context.KardexViale.Add(KardexViale);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(KardexViale));
        }
    }
}