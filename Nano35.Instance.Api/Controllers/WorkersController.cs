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
using Nano35.Instance.Api.Requests.GetAllRolesByUser;
using Nano35.Instance.Api.Requests.GetAllWorkers;
using Nano35.Instance.Api.Requests.GetWorkerById;
using Nano35.Instance.Api.Requests.UpdateWorkersComment;
using Nano35.Instance.Api.Requests.UpdateWorkersName;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        private readonly IServiceProvider  _services;
        public WorkersController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetAllWorkers([FromQuery] GetAllWorkersHttpQuery query)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>(_services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>)) as ILogger<IGetAllWorkersRequestContract>,
                        new GetAllWorkersUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllWorkersRequestContract() {InstanceId = query.InstanceId, WorkersRoleId = query.WorkersRoleId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet("{id}")]
        [Produces("application/json")]
        public IActionResult GetWorkerById(Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>(_services.GetService(typeof(ILogger<IGetWorkerByIdRequestContract>)) as ILogger<IGetWorkerByIdRequestContract>,
                        new GetWorkerByIdUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetWorkerByIdRequestContract() { WorkerId = id })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet("{id}/Roles")]
        [Produces("application/json")]
        public IActionResult GetWorkerRolesById(Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>(_services.GetService(typeof(ILogger<IGetAllRolesByUserRequestContract>)) as ILogger<IGetAllRolesByUserRequestContract>,
                        new GetAllRolesByUserUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllRolesByUserRequestContract() { UserId = id })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
        
        [HttpGet("Current/Roles")]
        [Produces("application/json")]
        public IActionResult GetCurrentWorkerRolesById()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>(_services.GetService(typeof(ILogger<IGetAllRolesByUserRequestContract>)) as ILogger<IGetAllRolesByUserRequestContract>,
                    new GetAllRolesByCurrentUserUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new GetAllRolesByUserRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateWorker([FromBody] CreateWorkerHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>(_services.GetService(typeof(ILogger<ICreateWorkerRequestContract>)) as ILogger<ICreateWorkerRequestContract>, 
                        new CreateWorkerUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new CreateWorkerRequestContract()
                        {Comment = body.Comment ?? "",
                         Email = body.Email ?? "",
                         InstanceId = body.InstanceId,
                         Name = body.Name,
                         NewId = body.NewId,
                         Password = body.Password,
                         PasswordConfirm = body.PasswordConfirm,
                         Phone = body.Phone,
                         Roles = body.Roles})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        public IActionResult UpdateWorkersName([FromBody] UpdateWorkersNameHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(_services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>)) as ILogger<IUpdateWorkersNameRequestContract>,
                        new UpdateWorkersNameUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateWorkersNameRequestContract() { WorkersId = body.WorkersId, Name = body.Name})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Comment")]
        [Produces("application/json")]
        public IActionResult UpdateWorkersComment([FromBody] UpdateWorkersCommentHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(_services.GetService(typeof(ILogger<IUpdateWorkersCommentRequestContract>)) as ILogger<IUpdateWorkersCommentRequestContract>,
                        new UpdateWorkersCommentUseCase(_services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateWorkersCommentRequestContract() { WorkersId = body.WorkersId, Comment = body.Comment})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}