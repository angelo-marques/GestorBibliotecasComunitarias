using GestorBiblioteca.Application.Commands.Requests.Emprestimo;
using GestorBiblioteca.Application.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestorBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/emprestimos")]
    public class EmprestimosController: ControllerBase
    {
   
        private readonly IMediator _mediator;

       public EmprestimosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<GenericCommandResponse> Create([FromServices] IMediator mediator, [FromBody] CreateEmprestimoRequest request)
        {
            try
            {
                return await mediator.Send(request);
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }
        }

        [HttpPut]
        public async Task<GenericCommandResponse> Update([FromServices] IMediator mediator, [FromBody] UpdateEmprestimoRequest request)
        {
            try
            {
                return await mediator.Send(request);
            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }
        }

        [HttpDelete]
        public async Task<GenericCommandResponse> Delete([FromServices] IMediator mediator, [FromBody] DeleteEmprestimoRequest request)
        {
            try
            {
                return await mediator.Send(request);

            }
            catch (Exception ex)
            {
                return new GenericCommandResponse(false, ex.Message, null);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search)
        {
            var result = await _mediator.Send(new GestorBiblioteca.Application.Queries.Emprestimos.GetAllEmprestimosQuery(search));
            if (!result.IsSuccess)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GestorBiblioteca.Application.Queries.Emprestimos.GetEmprestimoByIdQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Message);
            return Ok(result.Data);
        }

        [HttpPut("{id:int}/devolver")]
        public async Task<GenericCommandResponse> Devolver(int id)
        {
            return await _mediator.Send(new ReturnEmprestimoRequest(id));
        }


    }
}
