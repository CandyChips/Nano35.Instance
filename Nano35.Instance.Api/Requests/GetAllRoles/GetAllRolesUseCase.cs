using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class GetAllRolesUseCase :
        EndPointNodeBase<
            IGetAllRolesRequestContract, 
            IGetAllRolesResultContract>
    {
        private readonly IBus _bus;

        public GetAllRolesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input) =>
            (await (new GetAllRolesRequest(_bus, input)).GetResponse());
    }
}