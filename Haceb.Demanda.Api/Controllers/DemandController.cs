using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Common.ResponseModel;
using Haceb.Demanda.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haceb.Demanda.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_demandPort"></param>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MessageResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageResponse))]
    public class DemandController(IDemandPort _demandPort) : ControllerBase
    {
        /// <summary>
        /// Metodo encargo de registrar una demanda en el sistema.
        /// </summary>
        /// <param name="demandRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(DemandRequest demandRequest)
        {
            DemandRequestValidator validator = new();
            var validatorResult = await validator.ValidateAsync(demandRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _demandPort.AddDemand(demandRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status201Created, Message = "Registro Exitoso" });
        }

        /// <summary>
        /// Metodo para actualizar la demanda y el historial.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="demandUpdateRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DemandUpdateRequest demandUpdateRequest)
        {
            demandUpdateRequest.Id = id;
            DemandUpdateRequestValidator validator = new();
            var validatorResult = await validator.ValidateAsync(demandUpdateRequest);
            if (!validatorResult.IsValid)
                InvalidModel.Response(validatorResult);

            await _demandPort.UpdateDemand(demandUpdateRequest);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Registro actualizado" });
        }

        /// <summary>
        /// Metodo encargado de obtener todas las demandas.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var demands = await _demandPort.GetList(pageNumber, pageSize);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = demands });
        }

        /// <summary>
        /// Metodo encargado de obtener una demanda por id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var demand = await _demandPort.GetById(id);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = demand });
        }

        /// <summary>
        /// Metodo encargado de filtrar por tipo demanda y estado.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("{type}, {status}")]
        public async Task<IActionResult> GetFilter(int type, DemandStatus status)
        {
            var demands = await _demandPort.GetByFilter(type, status);
            return Ok(new MessageResponse() { Status = StatusCodes.Status200OK, Message = "Consulta finalizada.", Data = demands });
        }
    }
}
