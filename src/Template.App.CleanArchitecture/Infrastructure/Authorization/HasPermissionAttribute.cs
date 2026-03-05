using Microsoft.AspNetCore.Authorization;

namespace Template.App.CleanArchitecture.Infrastructure.Authorization;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission);
