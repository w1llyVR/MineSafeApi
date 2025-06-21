using Application.Dto.FotoCondicion;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Swashbuckle.AspNetCore.Annotations;

namespace MineSafeApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class FotoCondicionController : Controller
    {
        private readonly IFotoCondicionService _fotoCondicionService;

        public FotoCondicionController(IFotoCondicionService fotoCondicionService)
        {
            _fotoCondicionService = fotoCondicionService;
        }

        //[Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Listar todas las fotos de condición", Description = "Devuelve el listado completo de fotos vinculadas a condiciones inseguras.")]
        [ProducesResponseType(typeof(Response<IEnumerable<FotoCondicionResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _fotoCondicionService.GetAllAsync();
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener foto por ID", Description = "Devuelve la información de una foto de condición específica.")]
        [ProducesResponseType(typeof(Response<FotoCondicionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<FotoCondicionResponseDto>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _fotoCondicionService.GetByIdAsync(id);
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpPost]
        [SwaggerOperation(Summary = "Registrar foto de condición", Description = "Guarda una nueva imagen relacionada a una condición insegura.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FotoCondicionRequestDto_Create request)
        {
            var result = await _fotoCondicionService.CreateAsync(request);
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpPost("update")]
        [SwaggerOperation(Summary = "Actualizar foto de condición", Description = "Modifica los datos de una imagen previamente registrada.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] FotoCondicionRequestDto_Update request)
        {
            var result = await _fotoCondicionService.UpdateAsync(request);
            return StatusCode((int)result.CodeError, result);
        }

        //[Authorize]
        [HttpPost("{id}")]
        [SwaggerOperation(Summary = "Eliminar foto de condición", Description = "Elimina del sistema una imagen específica por ID.")]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RegistroResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _fotoCondicionService.DeleteAsync(id);
            return StatusCode((int)result.CodeError, result);
        }
    }
}
