using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Common.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haceb.Demanda.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_lookupItemPort"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class LookupsController(ILookupItemPort _lookupItemPort) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de obtener todos los datos principales
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var demands = await _lookupItemPort.Get();
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = demands });
        }
    }
}
