using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesUseCase :
        EndPointNodeBase<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly IBus _bus;
        public GetAllInstanceTypesUseCase(IBus bus) => _bus = bus;
        public override async Task<IGetAllInstanceTypesResultContract> Ask(IGetAllInstanceTypesRequestContract input) =>
            await new MasstransitRequest<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract, IGetAllInstanceTypesSuccessResultContract, IGetAllInstanceTypesErrorResultContract>(_bus, input)
                .GetResponse();
    }
}