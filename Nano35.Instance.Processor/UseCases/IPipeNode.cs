using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases
{
    public interface IPipeNode<in TIn, TOut>
        where TOut : IResult
    {
        Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken);
    }
    
    public abstract class PipeNodeBase<TIn, TOut> : IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        private readonly IPipeNode<TIn, TOut> _next;
        protected PipeNodeBase(IPipeNode<TIn, TOut> next) => _next = next;
        protected Task<UseCaseResponse<TOut>> DoNext(TIn input, CancellationToken cancellationToken) => _next.Ask(input, cancellationToken);
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken);
        public UseCaseResponse<TOut> Pass(string error) => new(error);
        public UseCaseResponse<TOut> Pass(TOut success) => new(success);
    }

    public abstract class EndPointNodeBase<TIn, TOut> : IPipeNode<TIn, TOut>
        where TIn : IRequest
        where TOut : IResult
    {
        public abstract Task<UseCaseResponse<TOut>> Ask(TIn input, CancellationToken cancellationToken);
        public UseCaseResponse<TOut> Pass(string error) => new(error);
        public UseCaseResponse<TOut> Pass(TOut success) => new(success);
    }
    
    public class MasstransitRequest<TMessage, TResponse> 
        where TMessage : class, IRequest
        where TResponse : class, IResult
    {
        private readonly IRequestClient<TMessage> _requestClient;
        private readonly TMessage _request;
        public MasstransitRequest(IBus bus, TMessage request) { _requestClient = bus.CreateRequestClient<TMessage>(TimeSpan.FromSeconds(10)); _request = request; }
        public async Task<UseCaseResponse<TResponse>> GetResponse() => (await _requestClient.GetResponse<UseCaseResponse<TResponse>>(_request)).Message;
    }
}