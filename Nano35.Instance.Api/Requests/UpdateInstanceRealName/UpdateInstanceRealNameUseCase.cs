using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameUseCase : UseCaseEndPointNodeBase<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateInstanceRealNameUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IUpdateInstanceRealNameResultContract>> Ask(IUpdateInstanceRealNameRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(_bus, input)
                .GetResponse();
    }
}