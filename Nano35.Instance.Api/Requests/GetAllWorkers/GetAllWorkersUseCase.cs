using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class GetAllWorkersUseCase :
        EndPointNodeBase<
            IGetAllWorkersRequestContract, 
            IGetAllWorkersResultContract>
    {
        private readonly IBus _bus;
        public GetAllWorkersUseCase(IBus bus) => _bus = bus;
        public override async Task<IGetAllWorkersResultContract> Ask(IGetAllWorkersRequestContract input) =>
            await new MasstransitRequest<IGetAllWorkersRequestContract, IGetAllWorkersResultContract, IGetAllWorkersSuccessResultContract, IGetAllWorkersErrorResultContract>(_bus, input)
                .GetResponse();
    }
}