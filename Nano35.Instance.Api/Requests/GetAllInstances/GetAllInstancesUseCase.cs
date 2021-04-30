using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstances
{
    public class GetAllInstancesUseCase :
        EndPointNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IBus _bus;
        public GetAllInstancesUseCase(IBus bus) => _bus = bus;
        public override async Task<IGetAllInstancesResultContract> Ask(IGetAllInstancesRequestContract input) =>
            await new MasstransitRequest<IGetAllInstancesRequestContract, IGetAllInstancesResultContract, IGetAllInstancesSuccessResultContract, IGetAllInstancesErrorResultContract>(_bus, input)
                .GetResponse();
    }
}