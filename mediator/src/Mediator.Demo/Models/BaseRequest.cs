using MediatR;

namespace Mediator.Demo
{
    public class BaseRequest<T> : IRequest<T>
        where T : BaseResponse
    {
        public AuthorizationToken Token { get; set; }
    }

    public class BaseResponse
    {
    }
}