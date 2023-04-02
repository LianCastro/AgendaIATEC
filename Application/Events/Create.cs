using Domain;
using MediatR;
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

            public Handler(DataContext context)
            {
                _context = context;
            }

            async Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Events.Add(request.Event);

                await _context.SaveChangesAsync();
            }
        }
    }
}
