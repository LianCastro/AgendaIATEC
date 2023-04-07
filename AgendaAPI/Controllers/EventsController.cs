using Application.Events;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    [AllowAnonymous]
    public class EventsController : BaseApiController
    {
        public EventsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]        
        public async Task<IActionResult> CreateEvent(Event @event)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Event = @event}));
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEvent(Guid id, Event @event)
        {
            @event.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Event = @event }));
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/participate")]
        public async Task<IActionResult> Participate(Guid id)
        {
            var userName = Request.Form["userName"].FirstOrDefault().ToString();
            return HandleResult(await Mediator.Send(new UpdateParticipants.Command { Id = id, UserName = userName }));
        }
    }
}
