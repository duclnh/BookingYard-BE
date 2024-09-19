using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fieldy.BookingYard.Api.Controllers
{
    [Route("api/court")]
    [ApiController]
    [Authorize]
    public class CourtController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourtController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourt(
              [FromBody] CreateCourtCommand command,
              CancellationToken cancellationToken = default
       )
        {

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}
