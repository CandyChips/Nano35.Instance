using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateWorkersName
{
    public class UpdateWorkersNameUseCase : UseCaseEndPointNodeBase<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>
    {
        private readonly IBus _bus;
        public UpdateWorkersNameUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateWorkersNameResultContract>> Ask(IUpdateWorkersNameRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(_bus, input)
                .GetResponse();

        }
    }
}