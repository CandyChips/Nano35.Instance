using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class GetUnitByIdUseCase :
        EndPointNodeBase<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>
    {
        private readonly IBus _bus;
        public GetUnitByIdUseCase(IBus bus) => _bus = bus;

        public override async Task<IGetUnitByIdResultContract> Ask(IGetUnitByIdRequestContract input) =>
            await new MasstransitRequest<IGetUnitByIdRequestContract, IGetUnitByIdResultContract, IGetUnitByIdSuccessResultContract, IGetUnitByIdErrorResultContract>(_bus, input)
                .GetResponse();

    }
}