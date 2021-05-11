using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class UpdateUnitsNameUseCase : UseCaseEndPointNodeBase<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateUnitsNameUseCase(IBus bus)
        {
            _bus = bus;
        }
        public override async Task<UseCaseResponse<IUpdateUnitsNameResultContract>> Ask(IUpdateUnitsNameRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(_bus, input)
                .GetResponse();
    }
}