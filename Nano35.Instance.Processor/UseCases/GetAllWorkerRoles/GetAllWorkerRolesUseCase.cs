using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class GetAllWorkerRolesUseCase :
        EndPointNodeBase<
            IGetAllWorkerRolesRequestContract, 
            IGetAllWorkerRolesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllWorkerRolesUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllWorkerRolesSuccessResultContract : 
            IGetAllWorkerRolesSuccessResultContract
        {
            public IEnumerable<IWorkersRoleViewModel> Data { get; set; }
        }

        private class GetAllClientStatesErrorResultContract : 
            IGetAllClientStatesErrorResultContract
        {
            public string Message { get; set; }
        }

        public override async Task<IGetAllWorkerRolesResultContract> Ask(
            IGetAllWorkerRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await this._context.WorkerRoles
                .MapAllToAsync<IWorkersRoleViewModel>();
            return new GetAllWorkerRolesSuccessResultContract() {Data = result};
        }
    }
}