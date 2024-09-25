using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.Court;
using Fieldy.BookingYard.Application.Features.Court.Commands.CreateCourt;
using Fieldy.BookingYard.Application.Features.Court.Commands.UpdateCourt;
using Fieldy.BookingYard.Application.Features.Court.Queries.GetAllCourt;
using Fieldy.BookingYard.Application.Features.Court.Queries.GetCourtById;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,CourtOwner")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCourt(
              [FromForm] CreateCourtCommand command,
              CancellationToken cancellationToken = default
        )
        {

            var result = await _mediator.Send(command, cancellationToken);
            return Created("", result);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,CourtOwner")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCourt(
            [FromForm] UpdateCourtCommand command,
            CancellationToken cancellationToken = default
        )
        {

            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("/api/court-facility/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,CourtOwner")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<CourtDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCourt(
             [FromRoute] Guid id,
             CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(new GetAllCourtQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpGet("/api/court/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,CourtOwner")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(CourtDetailDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCourtById(
             [FromRoute] int id,
             CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(new GetCourtByIdQuery(id), cancellationToken);
            return Ok(result);
        }
    }
}
