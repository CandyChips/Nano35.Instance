using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
{
    public class UpdateWorkersCommentUseCase : UseCaseEndPointNodeBase<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>
    {
        private readonly IBus _bus;
        public UpdateWorkersCommentUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<IUpdateWorkersCommentResultContract>> Ask(IUpdateWorkersCommentRequestContract input)
        {
            return await new MasstransitUseCaseRequest<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(_bus, input)
                .GetResponse();
        }
    }
}