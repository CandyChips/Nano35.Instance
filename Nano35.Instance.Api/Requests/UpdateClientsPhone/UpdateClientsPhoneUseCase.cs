using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsPhone
{
    public class UpdateClientsPhoneUseCase : UseCaseEndPointNodeBase<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>
    {
        private readonly IBus _bus;
        public UpdateClientsPhoneUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateClientsPhoneResultContract>> Ask(IUpdateClientsPhoneRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(_bus, input).GetResponse();
        }
    }
}