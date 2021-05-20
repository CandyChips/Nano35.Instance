using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerById
{
    public class GetWorkerById : EndPointNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly ApplicationContext _context;
        private readonly IBus _bus;
        public GetWorkerById(ApplicationContext context, IBus bus) { _context = context; _bus = bus; }
        public override async Task<UseCaseResponse<IGetWorkerByIdResultContract>> Ask(
            IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            var worker = await _context
                .Workers
                .FirstOrDefaultAsync(f => f.Id == input.WorkerId, cancellationToken);
            var tmp = new WorkerViewModel()
            {
                Id = worker.Id, 
                Comment = worker.Comment,
                Roles = worker.WorkersRoles.Select(role => role.Role.Id).ToList()
            };
            var response = await new MasstransitRequest<IGetUserByIdRequestContract, IGetUserByIdResultContract>(_bus, new GetUserByIdRequestContract { UserId = worker.Id }).GetResponse();
            if (response.IsSuccess())
            {
                var result = response.Success.Data;
                tmp.Name = result.Name;
                tmp.Email = result.Email;
                tmp.Phone = result.Phone;
            }
            else return Pass("");
            return Pass(new GetWorkerByIdResultContract() { Data = tmp });
        }
    }
}