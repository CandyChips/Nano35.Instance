using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAddress
{
    public class UpdateUnitsAddressUseCase : UseCaseEndPointNodeBase<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsAddressUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateUnitsAddressResultContract>> Ask(IUpdateUnitsAddressRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>(_bus, input)
                .GetResponse();

        }
    }
}