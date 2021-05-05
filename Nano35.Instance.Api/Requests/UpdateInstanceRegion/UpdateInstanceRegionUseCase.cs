using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRegion
{
    public class UpdateInstanceRegionUseCase : UseCaseEndPointNodeBase<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;
        public UpdateInstanceRegionUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateInstanceRegionResultContract>> Ask(IUpdateInstanceRegionRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId; 
            return await new MasstransitUseCaseRequest<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(_bus, input)
                .GetResponse();

        }
    }
}