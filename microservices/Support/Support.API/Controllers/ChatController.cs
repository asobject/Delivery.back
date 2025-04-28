using Application.Features.Commands.SendMessage;
using Application.Features.Queries.GetChatHistory;
using Domain.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Support.API.Controllers
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController(IMediator mediator) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromHeader(Name = "X-User-Sub")] string sub, SendMessageRequest request)
        {
            await mediator.Send(new SendMessageCommand(
                sub,
                request.Text,
                request.IsClientResponse
            ));
            return Ok();
        }
        [HttpGet("history")]
        public async Task<ActionResult<GetChatHistoryResponse>> GetHistory([FromHeader(Name = "X-User-Sub")] string sub)
        {
            var result = await mediator.Send(new GetChatHistoryQuery(sub));
            return Ok(result);
        }
    }
}
