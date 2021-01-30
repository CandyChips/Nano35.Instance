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
    public class GetAllClientTypesQuery : 
        IGetAllClientTypesRequestContract,
        IQueryRequest<IGetAllClientTypesResultContract>
    {
        private class GetAllClientTypesResultContract : 
            IGetAllClientTypesSuccessResultContract
        {
            public IEnumerable<IClientTypeViewModel> Data { get; set; }
        }

        public class GetAllClientTypesHandler 
            : IRequestHandler<GetAllClientTypesQuery, IGetAllClientTypesResultContract>
        {
            private readonly ApplicationContext _context;
            public GetAllClientTypesHandler(
                ApplicationContext context)
            {
                _context = context;
            }
            
            public async Task<IGetAllClientTypesResultContract> Handle(
                GetAllClientTypesQuery message,
                CancellationToken cancellationToken)
            {
                var result = await (_context.ClientTypes
                    .MapAllToAsync<IClientTypeViewModel>());
                return new GetAllClientTypesResultContract() {Data = result};
            }
        }
    }
}