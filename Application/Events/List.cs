using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class List
    {
        public class Query : IRequest<Result<List<EventDto>>>
        {
            public EventParams Params { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<EventDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IUserAccess _userAccess;

            public Handler(DataContext context, IMapper mapper, IUserAccess userAccess)
            {
                _context = context;
                _mapper = mapper;
                _userAccess = userAccess;
            }
            public async Task<Result<List<EventDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _context.Events
                    .Where(d => d.Date >= request.Params.StartDate)
                    .OrderBy(d => d.Date)
                    .ProjectTo<EventDto>(_mapper.ConfigurationProvider)
                    .AsQueryable();
                if (request.Params.IsGoing && !request.Params.IsHost)
                    query = query.Where(x => x.Participants.Any(p => p.UserName.ToUpper() == _userAccess.GetUserName().ToUpper()));
                if (request.Params.IsHost && !request.Params.IsGoing)
                    query = query.Where(x => x.HostUsername == _userAccess.GetUserName());
                if(!request.Params.IsHost && !request.Params.IsGoing)
                    query = query.Where(x => x.Participants.All(p => p.UserName.ToUpper() != _userAccess.GetUserName().ToUpper()));

                return Result<List<EventDto>>.Success(await query.ToListAsync());
            }
        }
    }
}
