using Application.Services.Repositories;
using Domain.Constants;
using System.Security.Claims;

namespace WebAPI.Middleware;

public class DataFilterMiddleware
{
    private readonly RequestDelegate _next;

    public DataFilterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;

        if (user.Identity?.IsAuthenticated ?? false)
        {
            var userRoles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            // HR Assistant - Sadece Employee endpoints
            if (userRoles.Contains(Roles.HRAssistant) && !userRoles.Contains(Roles.Admin))
            {
                var path = context.Request.Path.Value?.ToLower() ?? "";
                
                var allowedPaths = new[]
                {
                    "/api/employees",
                    "/api/departments",
                    "/api/jobs",
                    "/api/leavetypes",
                    "/api/entitledleaves",
                    "/api/employeeleaveusages",
                    "/api/timekeepings",
                    "/api/overtimes",
                    "/api/auth"
                };

                bool isAllowed = allowedPaths.Any(ap => path.StartsWith(ap));

                if (!isAllowed)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsJsonAsync(new { error = "Access denied" });
                    return;
                }
            }

            // Moderator - Assigned Quarries Only
            if (userRoles.Contains(Roles.Moderator) && !userRoles.Contains(Roles.Admin))
            {
                var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "");
                var quarryModeratorRepository = context.RequestServices
                    .GetRequiredService<IQuarryModeratorRepository>();

                var assignedQuarries = await quarryModeratorRepository.GetListAsync(
                    predicate: qm => qm.UserId == userId
                );

                var assignedQuarryIds = assignedQuarries.Items.Select(qm => qm.QuarryId).ToList();
                context.Items["ModeratorQuarryIds"] = assignedQuarryIds;
            }
        }

        await _next(context);
    }
}
