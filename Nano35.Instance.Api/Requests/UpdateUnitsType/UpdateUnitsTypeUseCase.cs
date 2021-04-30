using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeUseCase : EndPointNodeBase<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateUnitsTypeUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<IUpdateUnitsTypeResultContract> Ask(IUpdateUnitsTypeRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitRequest<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract, IUpdateUnitsTypeSuccessResultContract, IUpdateUnitsTypeErrorResultContract>(_bus, input)
                .GetResponse();
        }
    }
}