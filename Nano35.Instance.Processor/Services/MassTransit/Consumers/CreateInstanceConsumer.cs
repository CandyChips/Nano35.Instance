using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class CreateInstanceConsumer : 
        IConsumer<ICreateInstanceRequestContract>
    {
        private readonly ILogger<CreateInstanceConsumer> _logger;
        private readonly ApplicationContext _context;
        public CreateInstanceConsumer(
            ApplicationContext context,
            ILogger<CreateInstanceConsumer> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Consume(ConsumeContext<ICreateInstanceRequestContract> context)
        {
            _logger.LogInformation("ICreateInstanceRequestContract tracked");
            var message = context.Message;

            await using var transaction = this._context.Database.BeginTransaction();
            try
            {
                var instanceType = this._context.InstanceTypes.Find(message.TypeId);
                var region = this._context.Regions.Find(message.RegionId);
                var role = this._context.WorkerRoles.FirstOrDefault();
                var instance = new Models.Instance(){
                    Id = message.NewId,
                    OrgEmail = message.Email,
                    OrgName = message.Name,
                    OrgRealName = message.RealName,
                    CompanyInfo = message.Info,
                    InstanceType = instanceType,
                    InstanceTypeId = instanceType.Id,
                    Region = region,
                    RegionId = region.Id
                };
                await this._context.AddAsync(instance);
                await this._context.SaveChangesAsync(context.CancellationToken);
                instance = await this._context.Instances.FindAsync(message.NewId);
                var defaultUser = new Worker(){
                    Id = message.UserId,
                    Instance = instance,
                    WorkersRole = role,
                    Name = "Администратор системы",
                    Comment = ""
                };
                await this._context.AddAsync(defaultUser);
                await this._context.SaveChangesAsync(context.CancellationToken);
                await transaction.CommitAsync(context.CancellationToken);
                await context.RespondAsync<ICreateInstanceSuccessResultContract>(new {
                    Id = instance.Id
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(context.CancellationToken).ConfigureAwait(false);
                this._logger.LogError(ex.ToString());
                await context.RespondAsync<ICreateInstanceErrorResultContract>(new {
                    Error = ""
                });
                throw;
            }
        }
    }
}