using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class GetAllClientsUseCase :
        EndPointNodeBase<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly IBus _bus;

        public GetAllClientsUseCase(IBus bus) { _bus = bus; }

        public override async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input) => (await (new GetAllClientsRequest(_bus, input)).GetResponse());
    }
}
