using Application.Events;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAPI.Controllers
{
    public class EventsController : BaseApiController
    {
        public EventsController()
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetEvents()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]        
        public async Task CreateEvent(Event @event)
        {
            await Mediator.Send(new Create.Command { Event = @event});
        }

        [HttpPut("{id}")]
        public async Task EditEvent(Guid id, Event @event)
        {
            @event.Id = id;
            await Mediator.Send(new Edit.Command { Event = @event });
        }

        [HttpDelete("{id}")]
        public async Task DeleteEvent(Guid id)
        {
            await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}
