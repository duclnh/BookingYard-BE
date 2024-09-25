using Fieldy.BookingYard.Application.Features.Sport.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Sport.Queries.GetSportCreate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fieldy.BookingYard.Api.Controllers
{
    [Route("api/sport")]
    [ApiController]
    [Authorize]
    public class SportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("/api/sport-create")]
        [ProducesResponseType(typeof(SportCreateDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSportCreate(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetSportCreateQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
