using System.Net.Mime;
using Fieldy.BookingYard.Application.Features.Auth.Commands.Google;
using Fieldy.BookingYard.Application.Features.Auth.Commands.Register;
using Fieldy.BookingYard.Application.Features.Auth.Commands.SendVerificationCode;
using Fieldy.BookingYard.Application.Features.Auth.Commands.UpdatePassword;
using Fieldy.BookingYard.Application.Features.Auth.Commands.UpdateResetPassword;
using Fieldy.BookingYard.Application.Features.Auth.Commands.Verification;
using Fieldy.BookingYard.Application.Features.Auth.Commands.VerificationResetPassword;
using Fieldy.BookingYard.Application.Features.Auth.Queries.Login;
using Fieldy.BookingYard.Application.Features.Auth.SendResetPassword;
using Fieldy.BookingYard.Application.Models.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiedly.BookingYard.Api.Controllers
{
    [Route("api/auth")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(
            [FromBody] LoginQuery query,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("google")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginGoogle(
            [FromBody] GoogleCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(
            [FromBody] RegisterCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, result);
        }

        [HttpPost]
        [Route("send-verification")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendVerificationCode(
            [FromBody] SendVerificationCodeCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        [Route("verification-account")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Verification(
            [FromBody] VerificationCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("send-reset-password")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendRestPassword(
            [FromBody] ResetPasswordCommand command,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("verification-reset-password")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VerificationResetPassword(
            [FromBody] VerificationResetPasswordCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("reset-password")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateRestPassword(
           [FromBody] UpdateResetPasswordCommand command,
           CancellationToken cancellationToken = default
       )
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("password")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePassword(
            [FromBody] UpdatePasswordCommand command,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
    }
}