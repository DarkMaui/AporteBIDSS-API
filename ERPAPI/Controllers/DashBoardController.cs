/********************************************************************************************************
-- NAME   :  CRUDDashBoard
-- PROPOSE:  show methods DashBoard
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
6.0                  11/12/2019          Marvin.Guillen                 Changes of modificate date
5.0                  25/05/2019          Oscar.Gomez                    Metodo de seguridad
4.0                  25/05/2019          Freddy.Chinchilla              Changes of Dashbord
3.0                  19/06/2019          Freddy.Chinchilla              Changes of task return
2.0                  21/05/2019          Freddy.Chinchilla              Changes of dashboard mejora
1.0                  06/05/2019          Freddy.Chinchilla              Creation of controller
********************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Helpers;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/DashBoard")]
    [ApiController]
    public class DashBoardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;
        public DashBoardController(ILogger<DashBoardController> logger
            , ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityBranch([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Branch.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate && 
                    a.FechaCreacion <= _sarpara.EndDate).                    
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityRoles([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Roles.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityDepartamentos([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Departamento.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityUserRol([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.UserRoles.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityCustomers()
        {
            try
            {
                var Items = await _context.Customer.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        [HttpGet("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityProduct()
        {
            try
            {
                var Items = await _context.Product.CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }       

        [HttpPost("[action]")]
        public async Task<ActionResult<Int32>> GetQuantityEmployees([FromBody]DashBoard _sarpara)
        {
            try
            {
                var Items = await _context.Employees.
                    Where(a => a.FechaCreacion >= _sarpara.BeginDate &&
                    a.FechaCreacion <= _sarpara.EndDate).
                    CountAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }







    }
}