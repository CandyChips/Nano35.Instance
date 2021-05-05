using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceInfo
{
    public class UpdateInstanceInfoUseCase : UseCaseEndPointNodeBase<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>
    {
        private readonly IBus _bus;
        public UpdateInstanceInfoUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdateInstanceInfoResultContract>> Ask(IUpdateInstanceInfoRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(_bus, input)
                .GetResponse();
    }
}