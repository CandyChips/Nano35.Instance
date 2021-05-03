using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesUseCase : UseCaseEndPointNodeBase<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllInstanceTypesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllInstanceTypesSuccessResultContract>> Ask(IGetAllInstanceTypesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}