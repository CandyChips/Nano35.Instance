using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Identity.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests.GetAllRoles
{
    public class GetAllRolesRequest :
        IPipelineNode<
            IGetAllRolesRequestContract, 
            IGetAllRolesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllRolesRequest(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllRolesSuccessResultContract : 
            IGetAllRolesSuccessResultContract
        {
            public IEnumerable<IRoleViewModel> Data { get; set; }
        }

        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await this._context.Regions
                .MapAllToAsync<IRoleViewModel>();
            return new GetAllRolesSuccessResultContract() {Data = result};
        }
    }
}