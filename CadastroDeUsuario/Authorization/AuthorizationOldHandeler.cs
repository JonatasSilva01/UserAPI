using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CadastroDeUsuario.Authorization
{
    public class AuthorizationOldHandeler : AuthorizationHandler<AuthorizationOld>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationOld requirement)
        {
            var barthdayUser = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

            if (barthdayUser is null) return Task.CompletedTask;

            var barthday = Convert.ToDateTime(barthdayUser.Value);

            var dateTimeOld = DateTime.Today.Year - barthday.Year;

            if (barthday > DateTime.Today.AddYears(-dateTimeOld)) dateTimeOld--;

            if (dateTimeOld >= requirement.Old) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
