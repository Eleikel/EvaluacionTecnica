using EvaluacionTecnica.Business.Interfaces.Service;
using EvaluacionTecnica.Business.ViewModels.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionTecnica.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los roles
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get()
        {
            try
            {
                var roles = await _roleService.GetAll();

                if (roles == null || !roles.Any())
                {
                    return Ok(new { success = true, data = new List<RoleViewModel>(), message = "No hay roles registrados" });
                }

                return Ok(new { success = true, data = roles });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los roles");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene un rol por su ID
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

                var role = await _roleService.GetById(id);

                if (role == null)
                {
                    return NotFound(new { success = false, message = $"No se encontró el rol con ID {id}" });
                }

                return Ok(new { success = true, data = role });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol con ID {Id}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Crea un nuevo rol
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Create([FromBody] RoleViewModel role)
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

                if (role == null)
                {
                    return BadRequest(new { success = false, message = "El rol no puede ser nulo" });
                }

                var createdRole = await _roleService.Create(role);

                if (createdRole == null)
                {
                    return BadRequest(new { success = false, message = "No se pudo crear el rol" });
                }

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = createdRole.Id },
                    new { success = true, data = createdRole, message = "Rol creado exitosamente" }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el rol");
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Actualiza un rol existente
        /// </summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(int id, [FromBody] RoleViewModel role)
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

                if (role == null)
                {
                    return BadRequest(new { success = false, message = "El rol no puede ser nulo" });
                }

                if (role.Id != id)
                {
                    return BadRequest(new { success = false, message = "El ID de la ruta no coincide con el ID del objeto" });
                }

                var existingRole = await _roleService.GetById(id);
                if (existingRole == null)
                {
                    return NotFound(new { success = false, message = $"No se encontró el rol con ID {id}" });
                }

                var updatedRole = await _roleService.Update(id, role);

                if (updatedRole == null)
                {
                    return BadRequest(new { success = false, message = "No se pudo actualizar el rol" });
                }

                return Ok(new { success = true, data = updatedRole, message = "Rol actualizado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el rol con ID {Id}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Elimina un rol
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

                var existingRole = await _roleService.GetById(id);
                if (existingRole == null)
                {
                    return NotFound(new { success = false, message = $"No se encontró el rol con ID {id}" });
                }

                var result = await _roleService.Delete(id);

                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se pudo eliminar el rol" });
                }

                return Ok(new { success = true, message = "Rol eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el rol con ID {Id}", id);
                return StatusCode(500, new { success = false, message = "Error interno del servidor" });
            }
        }
    }
}
