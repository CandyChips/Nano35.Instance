using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class GetAllClientsUseCase :
        EndPointNodeBase<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly IBus _bus;
        public GetAllClientsUseCase(IBus bus) => _bus = bus;
        public override async Task<IGetAllClientsResultContract> Ask(IGetAllClientsRequestContract input) => 
            await new MasstransitRequest<IGetAllClientsRequestContract, IGetAllClientsResultContract, IGetAllClientsSuccessResultContract, IGetAllClientsErrorResultContract>(_bus, input)
                .GetResponse();
    }
}
