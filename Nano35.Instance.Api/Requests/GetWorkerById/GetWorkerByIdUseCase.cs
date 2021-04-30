using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerById
{
    public class GetWorkerByIdUseCase :
        EndPointNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly IBus _bus;
        public GetWorkerByIdUseCase(IBus bus) => _bus = bus;

        public override async Task<IGetWorkerByIdResultContract> Ask(IGetWorkerByIdRequestContract input) =>
            await new MasstransitRequest<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract, IGetWorkerByIdSuccessResultContract, IGetWorkerByIdErrorResultContract>(_bus, input)
                .GetResponse();
    }
}