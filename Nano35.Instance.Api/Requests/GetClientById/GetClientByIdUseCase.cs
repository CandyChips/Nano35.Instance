using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class GetClientByIdUseCase : UseCaseEndPointNodeBase<IGetClientByIdRequestContract, IGetClientByIdSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetClientByIdUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetClientByIdSuccessResultContract>> Ask(IGetClientByIdRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetClientByIdRequestContract, IGetClientByIdSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}