using Microsoft.AspNetCore.Authorization;

namespace Template.App.CleanArchitecture.Infrastructure.Authorization;

internal sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
