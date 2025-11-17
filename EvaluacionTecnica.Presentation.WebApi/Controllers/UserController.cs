using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvaluacionTecnica.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            IRoleService roleService,
            ILogger<UserController> logger)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var users = await _userService.GetAll();

                if (users == null || !users.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        data = new List<UserViewModel>(),
                        message = "No hay usuarios registrados"
                    });
                }               

                return Ok(new { success = true, data = users });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }


        /// <summary>
        /// Obtiene un usuario por su ID
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { success = false, message = "El ID debe ser mayor a 0" });
                }

                var user = await _userService.GetById(id);

                if (user == null)
                {
                    return NotFound(new { success = false, message = $"No se encontró el usuario con ID {id}" });
                }

                return Ok(new { success = true, data = user });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el usuario con ID {Id}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }


        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] UserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Datos inválidos",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                if (user == null)
                {
                    return BadRequest(new { success = false, message = "El usuario no puede ser nulo" });
                }                           
                
                var roleExists = await _roleService.GetById(user.RoleId);
                if (roleExists == null)
                {
                    return BadRequest(new { success = false, message = $"El rol con ID {user.RoleId} no existe" });
                }              

                var createdUser = await _userService.Create(user);

                if (createdUser == null)
                {
                    return BadRequest(new { success = false, message = "No se pudo crear el usuario" });
                }


                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdUser.Id },
                    new { success = true, data = createdUser, message = "Usuario creado exitosamente" }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }


        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromBody] UserViewModel user)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { success = false, message = "El ID debe ser mayor a 0" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Datos inválidos",
                        errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                if (user == null)
                {
                    return BadRequest(new { success = false, message = "El usuario no puede ser nulo" });
                }

                if (user.Id != 0 && user.Id != id)
                {
                    return BadRequest(new { success = false, message = "El ID de la ruta no coincide con el ID del objeto" });
                }

                var existingUser = await _userService.GetById(id);
                if (existingUser == null)
                {
                    return NotFound(new { success = false, message = $"No se encontró el usuario con ID {id}" });
                }

                var roleExists = await _roleService.GetById(user.RoleId);
                if (roleExists == null)
                {
                    return BadRequest(new { success = false, message = $"El rol con ID {user.RoleId} no existe" });
                }

                var updatedUser = await _userService.Update(id, user);

                if (updatedUser == null)
                {
                    return BadRequest(new { success = false, message = "No se pudo actualizar el usuario" });
                }

                return Ok(new { success = true, data = updatedUser, message = "Usuario actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el usuario con ID {Id}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { success = false, message = "El ID debe ser mayor a 0" });
                }

                var existingUser = await _userService.GetById(id);
                if (existingUser == null)
                {
                    return NotFound(new { success = false, message = $"No se encontró el usuario con ID {id}" });
                }             

                var result = await _userService.Delete(id);

                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se pudo eliminar el usuario" });
                }

                return Ok(new { success = true, message = "Usuario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el usuario con ID {Id}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

    }


}
