using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class CreateClientUseCase : UseCaseEndPointNodeBase<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly ApplicationContext _context;
        public CreateClientUseCase(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<ICreateClientResultContract>> Ask(
            ICreateClientRequestContract input,
            CancellationToken cancellationToken)
        {
            var profile = _context.ClientProfiles.FirstOrDefault(p => p.Phone == input.Phone);
            
            if (profile == null)
            {
                profile = new ClientProfile() { Id = input.NewId, Phone = input.Phone };
                await _context.AddAsync(profile, cancellationToken);
            }
            
            var client = 
                new Client()
                    {Id = profile.Id,
                     InstanceId = input.InstanceId,
                     Name = input.Name,
                     Email = input.Email,
                     Deleted = false,
                     WorkerId = input.UserId,
                     ClientStateId = input.ClientStateId,
                     ClientTypeId =  input.ClientTypeId};
            
            await _context.AddAsync(client, cancellationToken);
            
            return new UseCaseResponse<ICreateClientResultContract>(new CreateClientResultContract());
        }
    }
}