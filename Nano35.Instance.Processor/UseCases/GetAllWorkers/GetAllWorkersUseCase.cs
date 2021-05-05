using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class GetAllWorkersUseCase : UseCaseEndPointNodeBase<IGetAllWorkersRequestContract, IGetAllWorkersSuccessResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public GetAllWorkersUseCase(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<IGetAllWorkersSuccessResultContract>> Ask(
            IGetAllWorkersRequestContract input,
            CancellationToken cancellationToken)
        {
            var result = await _context
                .Workers
                .Where(c => c.InstanceId == input.InstanceId)
                .Select(a =>
                    new WorkerViewModel()
                        {Id = a.Id,
                         Comment = a.Comment,
                         Role = a.WorkersRole.Name})
                .ToListAsync(cancellationToken);
            
            foreach (var item in result)
            {
                var response = await new MasstransitUseCaseRequest<IGetUserByIdRequestContract, IGetUserByIdSuccessResultContract>(_bus, new GetUserByIdRequestContract(){UserId = item.Id}).GetResponse();
                if (response.IsSuccess())
                {
                    var tmp = response.Success;
                    if (tmp.Data != null)
                    {
                        item.Name = tmp.Data.Name;
                        item.Email = tmp.Data.Email;
                        item.Phone = tmp.Data.Phone;
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            return new UseCaseResponse<IGetAllWorkersSuccessResultContract>(new GetAllWorkersSuccessResultContract() {Data = result});
        }
    }
}