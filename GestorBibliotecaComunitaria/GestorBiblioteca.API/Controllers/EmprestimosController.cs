using GestorBiblioteca.Application.Commands.Requests.Emprestimo;

using GestorBiblioteca.Application.Commands.Responses;
using GestorBiblioteca.Domain.Entities;
using MediatR;
using MicroServico.Domain.Validators;
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
        public async Task<GenericCommandResponse> Get([FromServices] IMediator mediator, [FromBody] DeleteEmprestimoRequest request)
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



        //[HttpPost]
        //public async Task<IActionResult>  Post([FromBody] InsertEmprestimoCommand command)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        if (command.DataDevolucao < command.DataEmprestimo)
        //        {
        //            return BadRequest("Data invalida");
        //        }
        //        //var id = _emprestimoService.Insert(model);
        //        var result = await _mediator.Send(command);

        //        if(!result.IsSuccess) 
        //            return BadRequest(result.Message);


        //        return CreatedAtAction(nameof(GetById), new { id = result.Data}, command);

        //    } 
        //    catch (Exception ex)
        //    {
        //        return UnprocessableEntity(new {Erro = ex.Message}); //captura erro emitido pelo metodo Insert()
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task <IActionResult> GetById(int id)
        //{
        //    //buscar emprestimo
        //    //var emprestimo = _emprestimoService.GetById(id);
        //    var result = await _mediator.Send(new GetEmprestimoByIdQuery(id));

        //    if (!result.IsSuccess)
        //        return BadRequest(result.Message);

        //   /* if (emprestimo == null)
        //        return NotFound();*/

        //    return Ok(result);
        //}

        //        [HttpGet]
        //        public async Task <IActionResult> GetAll(string search = "")
        //        {
        //            //var result = _emprestimoService.GetAll(query);  
        //            var query = new GetAllEmprestimosQuery
        //            {
        //                Query = search
        //            };

        //            var result = await _mediator.Send(query);

        //            //buscar todos os emprestimos com base numa pesquisa/busca
        ////            var emprestimos = _emprestimoService.GetAll(query);
        //            return Ok(result);
        //        }

        //[HttpPut]
        //public async Task <IActionResult> Put(int id, UpdateEmprestimoCommand command)
        //{
        //    if (command.DataDevolucao < command.DataEmprestimo)
        //    {
        //        return BadRequest("Data invalida");
        //    }

        //    var result = await _mediator.Send(command);

        //    //            _emprestimoService.Update(command);
        //    if (!result.IsSuccess)
        //        return BadRequest(result.Message);

        //    return NotFound("Emprestimo não encontrado");
        //}

        //[HttpPut("{id}/devolucao")]
        //public IActionResult DevolverLivro (int id, [FromBody] DateTime dataEntrega)
        //{
        //    try
        //    {
        //        int diasAtraso = _emprestimoService.DevolverLivro(id, dataEntrega);
        //        return Ok(new
        //        {
        //            Mensagem = diasAtraso > 0
        //            ? $"Livro devolvido com {diasAtraso} dia(s) de atraso."
        //            : "Livro devoldido dentro do prazo.",
        //            DiasAtraso = diasAtraso
        //        });
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { Message = ex.Message });  //captura erro ao n encontrar o id do Emprestimo
        //    } 
        //    catch (InvalidOperationException ex)
        //    {
        //        return UnprocessableEntity(new { Erro = ex.Message }); //captura erro emitido pelo metodo Devolver()
        //    }
        //}



        //[HttpDelete("{id}")]
        //public async Task <IActionResult> Delete (int id)
        //{
        //    var result = await _mediator.Send(new DeleteEmprestimoCommmand(id));

        //    if (!result.IsSuccess)
        //        return NotFound(new { Mensagem = result.Message });

        //    return Ok(new {result.Message});
        //}
    }
}
