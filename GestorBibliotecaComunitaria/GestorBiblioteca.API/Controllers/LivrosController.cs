using GestorBiblioteca.Application.Commands.Requests.Livros;
using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Application.Queries.Livros;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestorBiblioteca.API.Controllers
{
    [ApiController]
    [Route("api/livros")]
    public class LivrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LivrosController (IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<GenericCommandResponse> Create([FromServices] IMediator mediator, [FromBody] CreateLivroRequest request)
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
        public async Task<GenericCommandResponse> Update([FromServices] IMediator mediator, [FromBody] UpdateLivroRequest request)
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
        public async Task<GenericCommandResponse> Get([FromServices] IMediator mediator, [FromBody] DeleteLivroRequest request)
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
            var result = await _mediator.Send(new GetAllLivrosQuery(search));
            if (!result.IsSuccess)
                return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetLivroByIdQuery(id));
            if (!result.IsSuccess)
                return NotFound(result.Message);
            return Ok(result.Data);
        }

    }
}
