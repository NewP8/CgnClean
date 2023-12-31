namespace CgnClean.CgnFintech.Domain.Exceptions;

/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class TenantDomainException : Exception
{
    public TenantDomainException()
    { }

    public TenantDomainException(string message)
        : base(message)
    { }

    public TenantDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
