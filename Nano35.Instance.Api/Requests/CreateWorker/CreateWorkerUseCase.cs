using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerUseCase :
        EndPointNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ICustomAuthStateProvider _auth;
        private readonly IBus _bus;
        
        public CreateWorkerUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<ICreateWorkerResultContract> Ask(
            ICreateWorkerRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);

            return await new MasstransitRequest<ICreateWorkerRequestContract, ICreateWorkerResultContract, ICreateWorkerSuccessResultContract, ICreateWorkerErrorResultContract>(_bus, input)
                    .GetResponse();
        }
    }
}