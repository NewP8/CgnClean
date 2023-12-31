using MediatR;

namespace CgnClean.CgnFintech.Application.Commands;


public class CreaTenant
    : IRequest<int>
{
    public string TenantName { get; set; } = "";
}
