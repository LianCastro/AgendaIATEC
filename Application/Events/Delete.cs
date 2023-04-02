using MediatR;
using Persistence;

namespace Application.Events
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var @event = await _context.Events.FindAsync(request.Id);
                _context.Remove(@event);
                await _context.SaveChangesAsync();
            }
        }
    }
}
