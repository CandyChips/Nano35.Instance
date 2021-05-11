using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientUseCase : UseCaseEndPointNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public CreateClientUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<ICreateClientResultContract>> Ask(ICreateClientRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            return await new MasstransitUseCaseRequest<ICreateClientRequestContract, ICreateClientResultContract>(_bus, input)
                .GetResponse();
        }
    }
}