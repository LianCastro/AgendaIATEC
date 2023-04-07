using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class Details
    {
        public class Query : IRequest<Result<EventDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<EventDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var @event = await _context.Events.ProjectTo<EventDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(e => e.Id == request.Id);
                return Result<EventDto>.Success(@event);
            }
        }
    }
}
