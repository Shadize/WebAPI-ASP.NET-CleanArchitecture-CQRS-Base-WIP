﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Features.Examples.Commands.CreateExampleCommand;
using WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand;
using WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand;
using WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry;
using WebAPI.Application.Features.Examples.Queries.GetExampleByIdQuerry;

namespace WebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : BaseApiController
    {
        private readonly ISender _mediatr;

        public ExamplesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExampleById(Guid id)
        {
            var query = new GetExampleQuery(id);
            var result = await _mediatr.Send(query);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        // to do
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediatr.Send(new ListExamplesQuery());
            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }


        [HttpPost]
        public async Task<IActionResult> CreateExample([FromBody] CreateExampleCommand command)
        {
            var result = await _mediatr.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetExampleById), new { id = result.Value }, result.Value);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExample(Guid id, [FromBody] UpdateExampleCommand command)
        {
            if(id != command.Id)
                return BadRequest();


            var result = await _mediatr.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExample(Guid id)
        {
            var command = new DeleteExampleCommand(id);
            var result = await _mediatr.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return NoContent();
        }






    }
}