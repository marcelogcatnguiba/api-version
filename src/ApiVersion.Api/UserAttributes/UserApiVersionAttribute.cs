using Asp.Versioning;

namespace ApiVersion.Api.UserAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
public class UserApiVersionAttribute(double version) : ApiVersionAttribute(version)
{
    
}
