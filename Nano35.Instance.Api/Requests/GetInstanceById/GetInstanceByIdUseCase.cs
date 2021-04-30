using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class GetInstanceByIdUseCase : 
        EndPointNodeBase<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly IBus _bus;
        public GetInstanceByIdUseCase(IBus bus) => _bus = bus;

        public override async Task<IGetInstanceByIdResultContract> Ask(IGetInstanceByIdRequestContract input) =>
            await new MasstransitRequest<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract, IGetInstanceByIdSuccessResultContract, IGetInstanceByIdErrorResultContract>(_bus, input)
                .GetResponse();
    }
}