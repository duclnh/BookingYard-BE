using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.Support.Commands.CreateAdvise;
using Fieldy.BookingYard.Application.Features.Support.Commands.CreateContact;
using Fieldy.BookingYard.Application.Features.Support.Queries.GetAllSupport;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fieldy.BookingYard.Api.Controllers
{
    [Route("api/support")]
    [Authorize]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SupportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("advise")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAdvise(
            [FromBody] CreateAdviseCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, result);
        }

        [AllowAnonymous]
        [HttpPost("contact")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateContact(
            [FromBody] CreateContactCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, result);
        }

        [AllowAnonymous]
        [HttpPost()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PagingResult<SupportDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSupport(
            [FromBody] RequestParams requestParams,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllSupportQuery(requestParams), cancellationToken);
            return Ok(result);
        }
    }
}
