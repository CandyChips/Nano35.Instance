using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class GetAllWorkerRolesUseCase : UseCaseEndPointNodeBase<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllWorkerRolesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllWorkerRolesSuccessResultContract>> Ask(IGetAllWorkerRolesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}