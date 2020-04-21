﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _rolemanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public CuentaController(
            UserManager<ApplicationUser> userManager,
             RoleManager<ApplicationRole> rolemanager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            , ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _rolemanager = rolemanager;
            _context = context;
        }



        /// <summary>
        /// Crea una nueva cuenta de usuario,con el email y el Usuario , el cual puede autenticarse a la api , y generar un web token.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                JwtSecurityToken token = await BuildToken(new List<Claim>());
                UserToken userToken = new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
                return await Task.Run(() => userToken);
            }
            else
            {
                return await Task.Run(() => BadRequest("Username or password invalid"));
            }


        }


        [HttpPost("CambiarPassword")]
        public async Task<ActionResult<UserToken>> CambiarPassword([FromBody] UserInfo model)
        {
            var user = _context.Users.Where(q => q.Email == model.Email).FirstOrDefault();
            user.LastPasswordChangedDate = DateTime.Now;

            var result = await _userManager.ChangePasswordAsync(user, model.PasswordAnterior, model.Password);

            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return await Task.Run(() => Ok());
            }
            else
            {
                return await Task.Run(() => BadRequest(@"Contrasña o usuario incorrecto"));
            }
        }


        /// <summary>
        /// Cambia la Contraseña a un usuario proporcionando el email y la contraseña actual
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CambiarPasswordPoliticas")]
        public async Task<ActionResult<ApplicationUser>> CambiarPasswordPoliticas([FromBody] UserInfo model)
        {
            try
            {
                ApplicationUser ApplicationUserq = _context.Users.Where(q => q.Email == model.Email).FirstOrDefault();
                //string password = ApplicationUserq.PasswordHash;
                var passwordValidator = new PasswordValidator<ApplicationUser>();
                //_userManager.Options.Password.RequireDigit = true;
                //_userManager.Options.Password.RequiredLength = Convert.ToInt32(_context.ElementoConfiguracion.Where(w => w.Id == 21).FirstOrDefault().Valordecimal);

                var resultvalidador = await passwordValidator.ValidateAsync(_userManager, null, model.Password);
                if (resultvalidador.Succeeded)
                {
                    //Verifica en los historicos 
                    //var validarhistoricos = _context.PasswordHistory.Where(w => w.UserId == ApplicationUserq.Id.ToString() & w.CreatedDate >= DateTime.Now.AddMonths(-6)).ToList();
                    //if (validarhistoricos != null)
                    //{
                    //    foreach (var item in validarhistoricos)
                    //    {
                    //        PasswordVerificationResult passwordMatch = _userManager.PasswordHasher.VerifyHashedPassword(ApplicationUserq, item.PasswordHash, model.Password);
                    //        if (passwordMatch == PasswordVerificationResult.Success)
                    //        {
                    //            return await Task.Run(() => BadRequest(@"Error Ya ha utilizado esta contraseña"));
                    //        }
                    //    }
                    //}

                    ApplicationUserq.LastPasswordChangedDate = ApplicationUserq.LastPasswordChangedDate = DateTime.Now;
                    var result = await _userManager.ChangePasswordAsync(ApplicationUserq, model.PasswordAnterior, model.Password);
                    if (result.Succeeded)
                    {


                        _context.PasswordHistory.Add(new PasswordHistory()
                        {
                            UserId = ApplicationUserq.Id.ToString(),
                            PasswordHash = ApplicationUserq.PasswordHash,
                        });
                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            // IdOperacion =,
                            Descripcion = ApplicationUserq.Id.ToString(),
                            DocType = "Usuario",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(ApplicationUserq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(ApplicationUserq, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "ChangePassword",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = ApplicationUserq.UsuarioCreacion,
                            UsuarioModificacion = ApplicationUserq.UsuarioModificacion,
                            UsuarioEjecucion = ApplicationUserq.UsuarioModificacion,

                        });

                        //await _context.SaveChangesAsync();
                        await _context.SaveChangesAsync();
                        return await Task.Run(() => Ok(ApplicationUserq));
                    }
                    else
                    {
                        return await Task.Run(() => BadRequest(@"Contraseña o usuario incorrecto"));

                    }


                }
                else
                {
                    string errores = "La Contraseña ";
                    foreach (var item in resultvalidador.Errors)
                    {
                        switch (item.Code)
                        {
                            case ("PasswordTooShort"):
                                errores += "es muy corta, ";
                                break;
                            case ("PasswordRequiresUniqueChars"):
                                errores += "requiere caracteres unicos, ";
                                break;
                            case ("PasswordRequiresNonAlphanumeric"):
                                errores += "requiere caracteres alfanumericos, ";
                                break;
                            case ("PasswordRequiresDigit"):
                                errores += "requiere caracteres numeros, ";
                                break;
                            case ("PasswordRequiresLower"):
                                errores += "requiere minúsculas, ";
                                break;
                            case ("PasswordRequiresUppe"):
                                errores += "requiere mayúsculas, ";
                                break;

                        }
                    }
                    return await Task.Run(() => BadRequest($"{errores}"));
                    //return await Task.Run(() => BadRequest($" El password debe tener mayusculas, minusculas y caracteres especiales!"));
                }

            }
            catch (Exception ex)
            {

                // ILogger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error: {ex.Message}"));
            }

        }



        /// <summary>
        /// Le permite a un usuario creado , autenticarse y generar el web token para el uso de los endpoints de la API.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {

            try
            {

                //_signInManager.Options.Lockout.MaxFailedAccessAttempts = Convert.ToInt32(_context.ElementoConfiguracion.Where(w => w.Id == 15).FirstOrDefault().Valordecimal);
                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, isPersistent: false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    ApplicationUser usuario = await _userManager.FindByEmailAsync(userInfo.Email);
                    var _approle = await _userManager.GetRolesAsync(usuario);
                    if (_approle == null)
                    {
                        return BadRequest($"El Usuario no tiene ningun rol asignado");
                    }
                    var claims = (List<Claim>)await _userManager.GetClaimsAsync(usuario);
                    var branch = _context.Branch.Where(q => q.BranchId == usuario.BranchId).FirstOrDefault();
                    var listaRoles = _approle.Select(async x => await _rolemanager.FindByNameAsync(x));
                    var listClaims = listaRoles.Select(x => _rolemanager.GetClaimsAsync(x.Result).Result.Where(c => c.Value.Equals("true")).ToList());
                    foreach (IList<Claim> claimsRole in listClaims)
                    {
                        claims.AddRange(claimsRole.ToArray());
                    }
                    //Verificacion manual debido a que la accion permite que se invoque de forma anonima
                    var permiso = claims.FirstOrDefault(x => x.Type.Equals("Seguridad.Iniciar Sesion") && x.Value.Equals("true"));
                    if (permiso == null)
                    {
                        return BadRequest($"El Usuario no tiene permiso para iniciar sesión");
                    }

                    claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
                    claims.Add(new Claim(ClaimTypes.Name, usuario.UserName));
                    claims.Add(new Claim("Branch",usuario.BranchId.ToString()));
                    claims.Add(new Claim("BranchName", branch.BranchName.ToString()));

                    JwtSecurityToken token = await BuildToken(claims);

                    Int32? cambiopassworddias = (Int32?)_context.ElementoConfiguracion.Where(q => q.Id == 20).Select(q => q.Valordecimal).FirstOrDefault();
                    if (cambiopassworddias == null)
                    {
                        cambiopassworddias = 0;
                    }

                    UserToken userToken = new UserToken()
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        BranchId = Convert.ToInt32(usuario.BranchId),
                        IsEnabled = usuario.IsEnabled,
                        LastPasswordChangedDate = usuario.LastPasswordChangedDate,
                        Passworddias = cambiopassworddias.Value,
                        BranchName = branch.BranchName

                    };

                    return await Task.Run(() => userToken);
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Usuario bloqueado.");
                    return await Task.Run(() => BadRequest(ModelState));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario y contraseña invalidos.");
                    return await Task.Run(() => BadRequest(ModelState));
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocurrio un error: {ex.Message}");
            }

        }

        private async Task<JwtSecurityToken> BuildToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(120);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds
               );

            return token;
        }






    }
}