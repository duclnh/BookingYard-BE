using Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;
using Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityDetail;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Domain.Entities;
using Fieldy.BookingYard.Application.Features.Facility.Queries;
using Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllFacilityAdmin;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Fieldy.BookingYard.Api.Controllers
{
	  [Route("api/facility")]
    [ApiController]
    [Authorize]
    public class FacilityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FacilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFacility(
           [FromForm] CreateFacilityCommand command,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created("", result);

		    }

        [AllowAnonymous]
        [HttpGet("{facilityID}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Facility), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFacilityDetail(
                [FromRoute] Guid facilityID,
          CancellationToken cancellationToken = default)
        {
          var result = await _mediator.Send(new GetFacilityDetailQuery(facilityID, cancellationToken));
          return Ok(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PagingResult<FacilityAdminDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFacilityAdmin(
           [FromQuery] RequestParams request,
           CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllFacilityAdminQuery(request), cancellationToken);
            return Ok(result);
        }
    }
}
