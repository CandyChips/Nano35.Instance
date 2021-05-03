using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class GetAllRolesUseCase : UseCaseEndPointNodeBase<IGetAllRolesRequestContract, IGetAllRolesSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllRolesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllRolesSuccessResultContract>> Ask(IGetAllRolesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllRolesRequestContract, IGetAllRolesSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}