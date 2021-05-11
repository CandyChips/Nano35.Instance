using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeUseCase : UseCaseEndPointNodeBase<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly IBus _bus;
        public UpdateUnitsTypeUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateUnitsTypeResultContract>> Ask(IUpdateUnitsTypeRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(_bus, input).GetResponse();
        }
    }
}