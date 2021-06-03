using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class CreateClient : EndPointNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly ApplicationContext _context;
        public CreateClient(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<ICreateClientResultContract>> Ask(
            ICreateClientRequestContract input,
            CancellationToken cancellationToken)
        {
            if (_context.ClientProfiles.Any(e => e.Id == input.NewId))
                return Pass("Повторите попытку позже.");
            if (_context.ClientProfiles.Any(e => e.Id == input.InstanceId))
                return Pass("Повторите попытку позже.");
            if (_context.ClientProfiles.Any(e => e.Id == input.ClientStateId))
                return Pass("Повторите попытку позже.");
            
            var profile = _context.ClientProfiles.FirstOrDefault(p => p.Phone == input.Phone);
            
            if (profile == null)
            {
                profile = new ClientProfile() { Id = input.NewId, Phone = input.Phone };
                await _context.AddAsync(profile, cancellationToken);
            }
            
            var client = new Client()
                {Id = input.NewId,
                 InstanceId = input.InstanceId,
                 Name = input.Name,
                 Email = input.Email,
                 Deleted = false,
                 ClientStateId = input.ClientStateId};
            
            await _context.AddAsync(client, cancellationToken);
            
            return Pass(new CreateClientResultContract());
        }
    }
}