using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Application.Features.Examples.Commands.CreateExampleCommand;
using WebAPI.Application.Features.Examples.Commands.DeleteExampleCommand;
using WebAPI.Application.Features.Examples.Commands.UpdateExampleCommand;
using WebAPI.Application.Features.Examples.Notifications;
using WebAPI.Application.Features.Examples.Queries.GetAllExamplesQuerry;
using WebAPI.Application.Features.Examples.Queries.GetExampleByIdQuerry;

namespace WebAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamplesController : BaseApiController
    {

        // If using Notification, use IMediator
        private readonly ISender _mediatr;

        public ExamplesController(ISender mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExampleById(Guid id, bool bypassCache = false)
        {
            var query = new GetExampleQuery(id, bypassCache);
            var result = await _mediatr.Send(query);

            if (result.IsFailed)
                return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool bypassCache = false)
        {
            var query = new ListExamplesQuery(bypassCache);
            var result = await _mediatr.Send(query);

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

            /* If Notification
               await MediatR.Publish(new CreateExampleNotification(result.Value));
            */

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
