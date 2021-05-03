using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.CreateWorker;
using Nano35.Instance.Api.Requests.GetAllWorkers;
using Nano35.Instance.Api.Requests.UpdateWorkersComment;
using Nano35.Instance.Api.Requests.UpdateWorkersName;
using Nano35.Instance.Api.Requests.UpdateWorkersRole;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly IServiceProvider  _services;
        public WorkersController(IServiceProvider services) => _services = services;

        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkersSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWorkersErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult GetAllWorkers([FromQuery] GetAllWorkersHttpQuery query)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllWorkersRequestContract, IGetAllWorkersSuccessResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>)) as
                            ILogger<IGetAllWorkersRequestContract>,
                        new GetAllWorkersUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllWorkersRequestContract()
                        {InstanceId = query.InstanceId, WorkersRoleId = query.WorkersRoleId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateWorkerSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateWorkerErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult CreateWorker([FromBody] CreateWorkerHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<ICreateWorkerRequestContract, ICreateWorkerSuccessResultContract>(
                        _services.GetService(typeof(ILogger<ICreateWorkerRequestContract>)) as ILogger<ICreateWorkerRequestContract>,
                        new CreateWorkerUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new CreateWorkerRequestContract()
                    {
                        Comment = body.Comment,
                        Email = body.Email,
                        InstanceId = body.InstanceId,
                        Name = body.Name,
                        NewId = body.NewId,
                        Password = body.Password,
                        PasswordConfirm = body.PasswordConfirm,
                        Phone = body.Phone,
                        RoleId = body.RoleId
                    })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Role")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersRoleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersRoleErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateWorkersRole([FromBody] UpdateWorkersRoleHttpBody body) =>
            new ConvertedUpdateWorkersRoleOnHttpContext(
                new LoggedPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersRoleRequestContract>)) as ILogger<IUpdateWorkersRoleRequestContract>,
                    new UpdateWorkersRoleUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateWorkersName([FromBody] UpdateWorkersNameHttpBody body) =>
            new ConvertedUpdateWorkersNameOnHttpContext(
                new LoggedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>)) as ILogger<IUpdateWorkersNameRequestContract>,
                    new UpdateWorkersNameUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Comment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersCommentErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateWorkersComment([FromBody] UpdateWorkersCommentHttpBody body) =>
            new ConvertedUpdateWorkersCommentOnHttpContext(
                new LoggedPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersCommentRequestContract>)) as ILogger<IUpdateWorkersCommentRequestContract>,
                    new UpdateWorkersCommentUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);
    }
}