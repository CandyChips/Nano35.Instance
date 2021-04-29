using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.GetAllCashOperations
{
    public class GetAllCashOperationsUseCase :
        EndPointNodeBase<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public GetAllCashOperationsUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public override async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input) => (await (new GetAllCashOperationsRequest(_bus, input)).GetResponse());
    }
}