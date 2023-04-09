using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class UpdateParticipants
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccess _userAccess;

            public Handler(DataContext context, IUserAccess userAccessor)
            {
                _context = context;
                _userAccess = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var @event = await _context.Events
                    .Include(e => e.Participants).ThenInclude(e => e.User)
                    .SingleOrDefaultAsync(x => x.Id == request.Id);
                if (@event == null) return null;

                var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.NormalizedUserName == _userAccess.GetUserName().ToUpper());

                var checkIsParticipant = @event.Participants.Any(x => x.User.NormalizedUserName == currentUser.UserName.ToUpper());

                if (!checkIsParticipant)
                {
                    var participant = new UserEvents
                    {
                        User = currentUser,
                        Event = @event,
                        IsHost = false,
                        IsActive = true,
                        DateJoined = DateTime.Now
                    };

                    @event.Participants.Add(participant);
                }

                var result = await _context.SaveChangesAsync() > 0;
                if (result)
                    return Result<Unit>.Success(Unit.Value);
                return Result<Unit>.Failure("Falha ao atualizar evento.");
            }
        }
    }
}
