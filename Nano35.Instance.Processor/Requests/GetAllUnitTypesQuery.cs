using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Requests.Behaviours;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Requests
{
    public class GetAllUnitTypesQuery : 
        IGetAllUnitTypesRequestContract,
        IQueryRequest<IGetAllUnitTypesResultContract>
    {
        private class GetAllUnitTypesResultContract : 
            IGetAllUnitTypesSuccessResultContract
        {
            public IEnumerable<IUnitTypeViewModel> Data { get; set; }
        }

        public class GetAllUnitTypesHandler 
            : IRequestHandler<GetAllUnitTypesQuery, IGetAllUnitTypesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllUnitTypesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllUnitTypesResultContract> Handle(
                GetAllUnitTypesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.UnitTypes
                    .MapAllToAsync<IUnitTypeViewModel>());
                return new GetAllUnitTypesResultContract() {Data = result};
            }
        }
    }
}