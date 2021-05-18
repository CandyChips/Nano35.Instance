using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRolesByUser
{
    public class GetAllRolesByUserUseCase : UseCaseEndPointNodeBase<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>
    {
        private readonly IBus _bus;
        public GetAllRolesByUserUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllRolesByUserResultContract>> Ask(IGetAllRolesByUserRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>(_bus, input).GetResponse();
    }
}