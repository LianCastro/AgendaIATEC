using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class Create
    {
        public class Command : IRequest
        {
            public Event Event { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccess _userAccess;

            public Handler(DataContext context, IUserAccess userAccess)
            {
                _context = context;
                _userAccess = userAccess;
            }

            async Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccess.GetUserName());
                var participant = new UserEvents
                {
                    User = user,
                    DateJoined = DateTime.UtcNow,
                    IsActive = true,
                    IsHost = true,
                    Event = request.Event
                };
                request.Event.Participants.Add(participant);

                _context.Events.Add(request.Event);
                await _context.SaveChangesAsync();
            }
        }
    }
}
