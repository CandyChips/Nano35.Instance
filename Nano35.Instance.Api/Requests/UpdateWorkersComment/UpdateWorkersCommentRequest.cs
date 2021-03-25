using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
{
    public class UpdateWorkersCommentRequest :
        MasstransitRequest
        <IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract,
            IUpdateWorkersCommentSuccessResultContract,
            IUpdateWorkersCommentErrorResultContract>
    {
        public UpdateWorkersCommentRequest(IBus bus, IUpdateWorkersCommentRequestContract request) : base(bus, request) {}
    }
}