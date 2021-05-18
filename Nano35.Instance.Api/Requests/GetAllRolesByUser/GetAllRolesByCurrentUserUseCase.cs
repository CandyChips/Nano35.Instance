using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.GetAllRolesByUser
{
    public class GetAllRolesByCurrentUserUseCase : UseCaseEndPointNodeBase<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _customAuthStateProvider;
        public GetAllRolesByCurrentUserUseCase(IBus bus, ICustomAuthStateProvider customAuthStateProvider) { _bus = bus; _customAuthStateProvider = customAuthStateProvider; }
        public override async Task<UseCaseResponse<IGetAllRolesByUserResultContract>> Ask(IGetAllRolesByUserRequestContract input)
        {
            input.UserId = _customAuthStateProvider.CurrentUserId;
            return await new MasstransitUseCaseRequest<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>(_bus, input).GetResponse();
        }
    }
}