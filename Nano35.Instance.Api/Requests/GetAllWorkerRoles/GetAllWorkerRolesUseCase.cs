using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class GetAllWorkerRolesUseCase : UseCaseEndPointNodeBase<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>
    {
        private readonly IBus _bus;
        public GetAllWorkerRolesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllWorkerRolesResultContract>> Ask(IGetAllWorkerRolesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(_bus, input)
                .GetResponse();
    }
}