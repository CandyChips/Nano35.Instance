using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRegion
{
    public class UpdateInstanceRegionUseCase : UseCaseEndPointNodeBase<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>
    {
        private readonly IBus _bus;
        public UpdateInstanceRegionUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateInstanceRegionResultContract>> Ask(IUpdateInstanceRegionRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(_bus, input)
                .GetResponse();

        }
    }
}