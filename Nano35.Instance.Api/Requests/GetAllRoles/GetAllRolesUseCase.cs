using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class GetAllRolesUseCase : UseCaseEndPointNodeBase<IGetAllRolesRequestContract, IGetAllRolesResultContract>
    {
        private readonly IBus _bus;
        public GetAllRolesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllRolesResultContract>> Ask(IGetAllRolesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllRolesRequestContract, IGetAllRolesResultContract>(_bus, input)
                .GetResponse();
    }
}