using Application.Events;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    public class EventsController : BaseApiController
    {
        public EventsController()
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetEvents()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]        
        public async Task CreateEvent(Event @event)
        {
            await Mediator.Send(new Create.Command { Event = @event});
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpPut("{id}")]
        public async Task EditEvent(Guid id, Event @event)
        {
            @event.Id = id;
            await Mediator.Send(new Edit.Command { Event = @event });
        }

        [Authorize(Policy = "IsEventHost")]
        [HttpDelete("{id}")]
        public async Task DeleteEvent(Guid id)
        {
            await Mediator.Send(new Delete.Command { Id = id });
        }

        [HttpPost("{id}/participate")]
        public async Task Participate(Guid id)
        {
            var userName = Request.Form["userName"].FirstOrDefault().ToString();
            await Mediator.Send(new UpdateParticipants.Command { Id = id, UserName = userName });
        }
    }
}
