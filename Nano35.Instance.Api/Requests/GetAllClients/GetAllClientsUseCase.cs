using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class GetAllClientsUseCase : UseCaseEndPointNodeBase<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly IBus _bus;
        public GetAllClientsUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllClientsResultContract>> Ask(IGetAllClientsRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllClientsRequestContract, IGetAllClientsResultContract>(_bus, input)
                .GetResponse();
    }
}
