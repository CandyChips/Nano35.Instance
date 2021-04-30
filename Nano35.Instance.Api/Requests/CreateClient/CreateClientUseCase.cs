using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientUseCase : EndPointNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public CreateClientUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<ICreateClientResultContract> Ask(ICreateClientRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            
            return await new MasstransitRequest<ICreateClientRequestContract, ICreateClientResultContract, ICreateClientSuccessResultContract, ICreateClientErrorResultContract>(_bus, input)
                .GetResponse();
        }
    }
}