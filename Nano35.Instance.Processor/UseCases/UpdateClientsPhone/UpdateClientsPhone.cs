using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhone : EndPointNodeBase<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>
    {
        private readonly ApplicationContext _context;
        public UpdateClientsPhone(ApplicationContext context) => _context = context;
        public override async Task<UseCaseResponse<IUpdateClientsPhoneResultContract>> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            var entityOfClient = await _context
                .ClientProfiles
                .FirstAsync(a => a.Id == input.ClientId, cancellationToken);
            
            if (entityOfClient == null) return new UseCaseResponse<IUpdateClientsPhoneResultContract>("Клиент не найден.");
            entityOfClient.Phone = input.Phone;
            
            return new UseCaseResponse<IUpdateClientsPhoneResultContract>(new UpdateClientsPhoneResultContract());
        }
    }
}