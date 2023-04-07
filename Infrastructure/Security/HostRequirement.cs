using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Security.Claims;

namespace Infrastructure.Security
{
    public class HostRequirement : IAuthorizationRequirement
    {
    }

    public class HostRequirementHandler : AuthorizationHandler<HostRequirement>
    {
        private readonly DataContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HostRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HostRequirement requirement)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) { return Task.CompletedTask; }

            var eventId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                .SingleOrDefault(x => x.Key == "id").Value?.ToString());

            var participant = _dbContext.UserEvents
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserId == userId && x.EventId == eventId)
                .Result;

            if (participant == null) { return Task.CompletedTask; }

            if (participant.IsHost) { context.Succeed(requirement); }

            return Task.CompletedTask;
        }
    }
}
