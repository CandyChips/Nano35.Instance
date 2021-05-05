using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesUseCase : UseCaseEndPointNodeBase<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>
    {
        private readonly IBus _bus;
        public GetAllInstanceTypesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllInstanceTypesResultContract>> Ask(IGetAllInstanceTypesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>(_bus, input)
                .GetResponse();
    }
}