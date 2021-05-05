using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class GetAllCurrentInstancesUseCase : UseCaseEndPointNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public GetAllCurrentInstancesUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IGetAllInstancesResultContract>> Ask(IGetAllInstancesRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(_bus, input)
                .GetResponse();
        }
    }
}