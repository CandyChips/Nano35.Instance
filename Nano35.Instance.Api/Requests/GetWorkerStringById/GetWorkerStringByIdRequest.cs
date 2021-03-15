using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerStringById
{
    public class GetWorkerStringByIdRequest :
        IPipelineNode<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetWorkerStringByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetWorkerStringByIdResultContract> Ask(IGetWorkerStringByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetWorkerStringByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetWorkerStringByIdSuccessResultContract, IGetWorkerStringByIdErrorResultContract>(input);
            if (response.Is(out Response<IGetWorkerStringByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<IGetWorkerStringByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}