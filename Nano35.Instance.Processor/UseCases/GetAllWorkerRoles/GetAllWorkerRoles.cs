﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class GetAllWorkerRoles : EndPointNodeBase<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>
    {
        private readonly ApplicationContext _context;
        public GetAllWorkerRoles(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetAllWorkerRolesResultContract>> Ask(
            IGetAllWorkerRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Roles
                .Select(a => 
                    new WorkersRoleViewModel()
                        {Id = a.Id,
                         Name = a.Name})
                .ToListAsync(cancellationToken);
            return new UseCaseResponse<IGetAllWorkerRolesResultContract>(new GetAllWorkerRolesResultContract() {Roles = result});
        }
    }
}