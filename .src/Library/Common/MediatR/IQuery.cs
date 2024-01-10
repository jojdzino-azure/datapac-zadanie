using MediatR;

namespace Common.MediatR;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}