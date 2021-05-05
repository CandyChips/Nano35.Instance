using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class GetClientByIdUseCase : UseCaseEndPointNodeBase<IGetClientByIdRequestContract, IGetClientByIdResultContract>
    {
        private readonly IBus _bus;
        public GetClientByIdUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetClientByIdResultContract>> Ask(IGetClientByIdRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetClientByIdRequestContract, IGetClientByIdResultContract>(_bus, input)
                .GetResponse();
    }
}