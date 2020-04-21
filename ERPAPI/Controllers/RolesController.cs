﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _rolemanager;
        private readonly IMapper mapper;
        private readonly ILogger _logger;

        public RolesController(ILogger<RolesController> logger,
            ApplicationDbContext context
            , RoleManager<ApplicationRole> rolemanager
            , IMapper mapper)
        {
            this.mapper = mapper;
            _context = context;
            _rolemanager = rolemanager;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene un rol , filtrado por su id.
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RoleId}")]
        public async Task<ActionResult> GetRoleById(Guid RoleId)
        {
            try
            {
                ApplicationRole Items = await _context.Roles.Where(q => q.Id == RoleId).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }
        /// <summary>
        /// Obtiene un rol , filtrado por su Nombre.
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        [HttpGet("[action]/{RoleName}")]
        public async Task<ActionResult> GetRoleByName(String RoleName)
        {
            try
            {
                ApplicationRole Items = await _context.Roles.Where(q => q.Name == RoleName).FirstOrDefaultAsync();
                return await Task.Run(() => Ok(Items));


            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

        }

        /// <summary>
        /// Obtiene los roles en formato Json
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<List<ApplicationRole>>> GetJsonRoles()
        {
            List<ApplicationRole> _users = new List<ApplicationRole>();
            try
            {
                var _rol = await _context.Roles.ToListAsync();
                _users = mapper.Map<List<ApplicationRole>>(_rol);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }
            return await Task.Run(() => Ok(_users));
        }



        /// <summary>
        /// Obtiene el listado de roles 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<ActionResult<List<ApplicationRole>>> GetRoles()
        {
            List<ApplicationRole> roles = new List<ApplicationRole>();
            try
            {
                roles = await _context.Roles.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }

            return await Task.Run(() => roles);
        }




        /// <summary>
        /// Crea un rol que permite tener accesos a recursos del API.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateRole")]
        public async Task<ActionResult<ApplicationRole>> CreateRole([FromBody] ApplicationRole model)
        {
            try
            {
                // IdentityRole _idrole = mapper.Map<ApplicationRole, IdentityRole>(model);
                //new ApplicationRole { Name = model.Name, NormalizedName = model.NormalizedName }
                var result = await _rolemanager.CreateAsync(model);
                if (result.Succeeded)
                {
                    return model;
                }
                else
                {
                    return await Task.Run(() => BadRequest("Role exists"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

        }


        /// <summary>
        /// Modifica/Actualiza un Rol 
        /// </summary>
        /// <param name="_rol"></param>
        /// <returns></returns>
        [HttpPut("PutRol")]
        public async Task<ActionResult<ApplicationRole>> PutRol([FromBody]ApplicationRole _rol)
        {
            try
            {
                ApplicationRole ApplicationRoleq = await _context.Roles.Where(q => q.Id == _rol.Id).FirstOrDefaultAsync();

                _rol.FechaCreacion = ApplicationRoleq.FechaCreacion;
                _rol.UsuarioCreacion = ApplicationRoleq.UsuarioCreacion;
                _rol.FechaModificacion = DateTime.Now;


                _context.Entry(ApplicationRoleq).CurrentValues.SetValues(_rol);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }


            return await Task.Run(() => _rol);
        }

        /// <summary>
        /// Elimina un rol que no este asignado a un usuario con el Id proporcionado.
        /// </summary>
        /// <param name="_ApplicationRole"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<ApplicationRole>> Delete([FromBody]ApplicationRole _ApplicationRole)
        {
            try
            {
                List<ApplicationUserRole> _rolasignado = await _context.UserRoles.Where(q => q.RoleId == _ApplicationRole.Id).ToListAsync();
                if (_rolasignado.Count == 0)
                {
                    _context.Roles.Remove(_ApplicationRole);
                    await _context.SaveChangesAsync();
                    return await Task.Run(() => (_ApplicationRole));
                }
                else
                {
                    return await Task.Run(() => BadRequest("El rol esta asignado a Usuarios"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }


        }

       // [Authorize(Policy = "Seguridad.Listar Permisos")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ListarPermisos([FromRoute(Name = "id")]string idRol)
        {
            List<string> permisos = new List<string>();
            try
            {
                var _appRole = await _rolemanager.FindByIdAsync(idRol);
                if (_appRole != null)
                {
                    var listClaims = await _rolemanager.GetClaimsAsync(_appRole);
                    permisos.AddRange(listClaims.Select(x => x.Type).ToList());
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
            return await Task.Run((() => Ok(permisos)));
        }

        //[Authorize(Policy = "Seguridad.Modificar Permisos Rol")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GuardarPermisosAsignados([FromBody] PostAsignacionesPermisoRol asignaciones)
        {
            try
            {
                var rol = await _rolemanager.FindByIdAsync(asignaciones.IdRol);
                var rolId = Guid.Parse(asignaciones.IdRol);
                if (rol != null)
                {
                    var listClaims = _context.RoleClaims.Where(p => p.RoleId.Equals(rolId)).ToList();

                    List<AspNetRoleClaims> permisosBorrar = new List<AspNetRoleClaims>();
                    List<AspNetRoleClaims> permisosInsertar = new List<AspNetRoleClaims>();

                    foreach (var claim in listClaims)
                    {
                        if (asignaciones.Permisos.FirstOrDefault(p => p.Id.Equals(claim.ClaimType)) == null)
                            permisosBorrar.Add(claim);
                    }

                    foreach (var permiso in asignaciones.Permisos)
                    {
                        if (listClaims.FirstOrDefault(p => p.ClaimType.Equals(permiso.Id)) == null)
                            permisosInsertar.Add(new AspNetRoleClaims()
                            {
                                ClaimType = permiso.Id,
                                ClaimValue = "true",
                                RoleId = rolId
                            });
                    }

                    _context.RoleClaims.RemoveRange(permisosBorrar);
                    _context.RoleClaims.AddRange(permisosInsertar);
                    _context.SaveChanges();

                    return new EmptyResult();


                }
                else
                {
                    return BadRequest($"Rol no existe");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }
        }

    }
}