using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateUnit
{
    public partial class CreateUnitUseCase : UseCaseEndPointNodeBase<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public CreateUnitUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<ICreateUnitResultContract>> Ask(ICreateUnitRequestContract input)
        {
            input.CreatorId = _auth.CurrentUserId;
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);
            return await new MasstransitUseCaseRequest<ICreateUnitRequestContract, ICreateUnitResultContract>(_bus, input)
                .GetResponse();
        }
    }
}