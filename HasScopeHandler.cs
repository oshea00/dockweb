using Microsoft.AspNetCore.Authorization;

namespace dockweb
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            HasScopeRequirement requirement)
        {
            if (context == null ||
                context.User == null ||
                requirement == null ||
                requirement.Issuer == null ||
                requirement.Scope == null)
            {
                return Task.CompletedTask;
            }

            if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            {
                return Task.CompletedTask;
            }

            var scope = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);
            if (scope != null)
            {
                var scopes = scope.Value.Split(' ');
                if (scopes.Any(s => s == requirement.Scope))
                    context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}