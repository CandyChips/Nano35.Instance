using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstancePhone
{
    public class UpdateInstancePhoneUseCase : UseCaseEndPointNodeBase<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>
    {
        private readonly IBus _bus;
        public UpdateInstancePhoneUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IUpdateInstancePhoneResultContract>> Ask(IUpdateInstancePhoneRequestContract input) =>
            await new MasstransitUseCaseRequest<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(_bus, input)
                .GetResponse();
    }
}