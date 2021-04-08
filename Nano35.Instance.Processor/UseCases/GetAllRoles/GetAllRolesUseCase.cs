﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.UseCases.GetAllRoles
{
    public class GetAllRolesUseCase :
        EndPointNodeBase<
            IGetAllRolesRequestContract, 
            IGetAllRolesResultContract>
    {
        private readonly ApplicationContext _context;

        public GetAllRolesUseCase(
            ApplicationContext context)
        {
            _context = context;
        }
        
        private class GetAllRolesSuccessResultContract : 
            IGetAllRolesSuccessResultContract
        {
            public IEnumerable<IRoleViewModel> Data { get; set; }
        }

        public override async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await (_context.WorkerRoles
                .MapAllToAsync<IRoleViewModel>());
            return new GetAllRolesSuccessResultContract() {Data = result};
        }
    }
}