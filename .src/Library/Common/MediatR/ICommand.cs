using MediatR;

namespace Common.MediatR;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}