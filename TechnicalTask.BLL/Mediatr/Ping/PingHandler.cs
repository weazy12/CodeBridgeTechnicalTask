using FluentResults;
using MediatR;

namespace TechnicalTask.BLL.Mediatr.Ping
{
    public class PingHandler : IRequestHandler<PingQuery, Result<string>>
    {
        public Task<Result<string>> Handle(PingQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Ok("Dogshouseservice.Version1.0.1"));
        }
    }
}
