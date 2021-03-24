using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class GetAllRegionsUseCase :
        EndPointNodeBase<
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract>
    {
        private readonly IBus _bus;

        public GetAllRegionsUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input) =>
            (await (new GetAllRegionsRequest(_bus, input)).GetResponse());
    }
}