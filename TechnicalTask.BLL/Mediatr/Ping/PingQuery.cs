using FluentResults;
using MediatR;

namespace TechnicalTask.BLL.Mediatr.Ping
{
    public record PingQuery : IRequest<Result<string>>;
}
