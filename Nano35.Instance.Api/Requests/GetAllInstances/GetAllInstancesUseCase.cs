using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstances
{
    public class GetAllInstancesUseCase : UseCaseEndPointNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IBus _bus;
        public GetAllInstancesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllInstancesResultContract>> Ask(IGetAllInstancesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(_bus, input)
                .GetResponse();
    }
}