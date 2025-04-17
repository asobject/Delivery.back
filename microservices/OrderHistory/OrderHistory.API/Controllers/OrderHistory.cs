
using Application.Features.OrderChanges.Queries.PointChange;
using Application.Features.OrderChanges.Queries.StatusChange;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OrderHistory.API.Controllers;

[Route("api/orders/{id}/history")]
[ApiController]
public class OrderHistory(IMediator mediator) : ControllerBase
{
   
    [HttpGet]
    public async Task<IActionResult> OrderChanges(int id)
    {
        var pointChangeQuery = new OrderPointChangeQuery(id);
        var statusChangeQuery = new OrderStatusChangeQuery(id);

        var pointChanges = await mediator.Send(pointChangeQuery);
        var statusChanges = await mediator.Send(statusChangeQuery);
        return Ok(new { pointChanges, statusChanges });
    }
}
