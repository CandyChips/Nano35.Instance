using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
{
    public class UpdateWorkersCommentUseCase : UseCaseEndPointNodeBase<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;
        public UpdateWorkersCommentUseCase(IBus bus, ICustomAuthStateProvider auth) { _bus = bus; _auth = auth; }
        public override async Task<UseCaseResponse<IUpdateWorkersCommentResultContract>> Ask(IUpdateWorkersCommentRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            return await new MasstransitUseCaseRequest<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(_bus, input)
                .GetResponse();
        }
    }
}