using Fieldy.BookingYard.Application.Features.Facility.Commands.CreateFacility;
using Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityDetail;
using Fieldy.BookingYard.Application.Models.Paging;
using Fieldy.BookingYard.Application.Models.Query;
using Fieldy.BookingYard.Application.Features.Facility.Queries;
using Fieldy.BookingYard.Application.Features.Facility.Queries.GetAllFacilityAdmin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.Facility.Queries.FacilityCustomer;
using Fieldy.BookingYard.Application.Features.Facility.Queries.DTO;
using Fieldy.BookingYard.Application.Features.Facility.Queries.GetFacilityHome;

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
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(FacilityDetailDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFacilityDetail(
                [FromRoute] Guid id,
          CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetFacilityDetailQuery(id, cancellationToken));
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

        [AllowAnonymous]
        [HttpGet("/api/facility-booking")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(PagingResult<FacilityCustomerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllFacilityCustomer(
          [FromQuery] RequestParams request,
          [FromQuery] float? longitude,
          [FromQuery] float? latitude,
          CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetAllFacilityCustomerQuery(request, longitude, latitude), cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("/api/facility-home")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IList<FacilityHomeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFacilityHome(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetFacilityHomeQuery(), cancellationToken);
            return Ok(result);
        }
    }
}
