﻿using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Events
{
    public class List
    {
        public class Query : IRequest<Result<List<EventDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<EventDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Result<List<EventDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<EventDto>>.Success(await _context.Events.ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToListAsync());
            }
        }
    }
}
