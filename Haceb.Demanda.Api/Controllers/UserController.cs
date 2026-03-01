using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Common.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haceb.Demanda.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_userPort"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserPort _userPort) : ControllerBase
    {
        /// <summary>
        /// Metodo encargado de obtener un usuario por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var demand = await _userPort.GetById(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = demand });
        }


        /// <summary>
        /// Metodo encargado de obtener todos usuarios activos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var demands = await _userPort.GetList();
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = demands });
        }
    }
}
