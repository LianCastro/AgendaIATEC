using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class UpdateParticipants
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccess _userAccess;

            public Handler(DataContext context, IUserAccess userAccessor)
            {
                _context = context;
                _userAccess = userAccessor;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = await _context.Events
                    .Include(e => e.Participants).ThenInclude(e => e.User)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);

                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccess.GetUserName());

                var hostUserName = @event.Participants.FirstOrDefault(x => x.IsHost)?.User.UserName;

                var participant = @event.Participants.FirstOrDefault(x => x.User.UserName == request.UserName);

                if (hostUserName == currentUser.UserName)
                {
                    if (participant != null) { @event.Participants.Remove(participant); }
                    else
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

                        participant = new UserEvents
                        {
                            User = user,
                            Event = @event,
                            IsHost = false,
                            IsActive = true,
                            DateJoined = DateTime.Now
                        };

                        @event.Participants.Add(participant);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
