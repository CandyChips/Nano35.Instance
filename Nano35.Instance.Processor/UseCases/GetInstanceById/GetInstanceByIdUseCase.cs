﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class GetInstanceByIdUseCase :
        UseCaseEndPointNodeBase<IGetInstanceByIdRequestContract, IGetInstanceByIdSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        public GetInstanceByIdUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IGetInstanceByIdSuccessResultContract>> Ask(
            IGetInstanceByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context.Instances
                .FirstOrDefaultAsync(f => f.Id == input.InstanceId, cancellationToken: cancellationToken);
            
            if (result == null)
            {
                return new UseCaseResponse<IGetInstanceByIdSuccessResultContract>("Организация не найден.");
            }
            
            return 
                new UseCaseResponse<IGetInstanceByIdSuccessResultContract>(
                    new GetInstanceByIdSuccessResultContract()
                    {
                        Data = new InstanceViewModel()
                        {
                            Id = result.Id,
                            CompanyInfo = result.CompanyInfo,
                            OrgEmail = result.OrgEmail,
                            OrgName = result.OrgName,
                            RegionId = result.RegionId,
                            OrgRealName = result.OrgRealName
                        }
                    });
        }
    }
}