using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class ConvertedCreateWorkerOnHttpContext :
        PipeInConvert
        <CreateWorkerHttpBody, 
            IActionResult,
            ICreateWorkerRequestContract, 
            ICreateWorkerResultContract>
        {
        public ConvertedCreateWorkerOnHttpContext(IPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(CreateWorkerHttpBody input)
        {
            var converted = new CreateWorkerRequestContract()
            {
                Comment = input.Comment,
                Email = input.Email,
                InstanceId = input.InstanceId,
                Name = input.Name,
                NewId = input.NewId,
                Password = input.Password,
                PasswordConfirm = input.PasswordConfirm,
                Phone = input.Phone,
                RoleId = input.RoleId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateWorkerSuccessResultContract success => new OkObjectResult(success),
                ICreateWorkerErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

