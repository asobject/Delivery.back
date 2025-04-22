
using Application.Features.Users.Commands.ChangeUserPassword;
using Application.Features.Users.Commands.ForgotUserPassword;
using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.LogoutUser;
using Application.Features.Users.Commands.RefreshTokenUser;
using Application.Features.Users.Commands.RegisterUser;
using BuildingBlocks.Interfaces.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.Users.Commands.ResetUserPassword;

namespace UserAuth.API.Controllers;

[ApiController]
[Route("api/user-auth")]
public class UserAuthController(IMediator mediator, ITokenExtractionService tokenExtractionService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        _ = await mediator.Send(command);
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var response = await mediator.Send(command);

        return Ok(response);
    }
    [HttpPut("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenUserCommand command = new()
        {
            AccessToken = tokenExtractionService.GetAccessTokenFromHeader(),
            RefreshToken = tokenExtractionService.GetRefreshTokenFromCookie()
        };
        var response = await mediator.Send(command);
        return Ok(response);
    }
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromHeader(Name = "X-User-Sub")] string sub, [FromBody] ChangeUserPasswordRequest request)
    {
        ChangeUserPasswordCommand command = new(sub, request.CurrentPassword, request.NewPassword);
        var response = await mediator.Send(command);
        return Ok(response);
    }
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromHeader(Name = "X-User-Email")] string email)
    {
        ForgotUserPasswordCommand command = new(email);
        var response = await mediator.Send(command);
        return Ok(response);
    }
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromHeader(Name = "X-User-Email")] string email, [FromBody] ResetUserPasswordRequest request)
    {
        ResetUserPasswordCommand command = new(email, request.Token, request.NewPassword);
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        LogoutUserCommand command = new()
        {
            AccessToken = tokenExtractionService.GetAccessTokenFromHeader(),
            RefreshToken = tokenExtractionService.GetRefreshTokenFromCookie()
        };
        _ = await mediator.Send(command);
        return NoContent();
    }
}