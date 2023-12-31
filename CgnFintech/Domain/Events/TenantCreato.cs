
using MediatR;

namespace CgnClean.CgnFintech.Domain.Events;

/// <summary>
/// Event used when an order is created
/// </summary>
public record class TenantCreato(
    int TenantId,
    string Name,
    string Guid) : INotification;
