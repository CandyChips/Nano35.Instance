using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class GetAllWorkerRolesUseCase :
        EndPointNodeBase<
            IGetAllWorkerRolesRequestContract, 
            IGetAllWorkerRolesResultContract>
    {
        private readonly IBus _bus;
        public GetAllWorkerRolesUseCase(IBus bus) => _bus = bus;
        public override async Task<IGetAllWorkerRolesResultContract> Ask(IGetAllWorkerRolesRequestContract input) =>
            await new MasstransitRequest<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract, IGetAllWorkerRolesSuccessResultContract, IGetAllWorkerRolesErrorResultContract>(_bus, input)
                .GetResponse();
    }
}