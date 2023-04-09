using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Event Event { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Event).SetValidator(new EventValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccess _userAccess;

            public Handler(DataContext context, IUserAccess userAccess)
            {
                _context = context;
                _userAccess = userAccess;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccess.GetUserName());
                var participant = new UserEvents
                {
                    User = user,
                    DateJoined = DateTime.Now,
                    IsActive = true,
                    IsHost = true,
                    Event = request.Event
                };
                request.Event.Participants.Add(participant);

                _context.Events.Add(request.Event);
                var result = await _context.SaveChangesAsync() > 0;

                if (result) return Result<Unit>.Success(Unit.Value);

                return Result<Unit>.Failure("Falha ao criar evento.");
            }
        }
    }
}
