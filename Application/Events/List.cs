using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class List
    {
        public class Query : IRequest<List<EventDto>> { }

        public class Handler : IRequestHandler<Query, List<EventDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<EventDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Events.ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToListAsync();
            }
        }
    }
}
