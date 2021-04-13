using System;
using System.Threading.Tasks;
using FluentValidation;
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
using Nano35.Instance.Api.Requests.GetAllWorkerRoles;
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

        public WorkersController(IServiceProvider services) { _services = services; }
        
        [Authorize]
        [HttpGet]
        [Route("GetAllWorkers")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkersSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWorkersErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllWorkers(
            [FromQuery] GetAllWorkersHttpQuery query)
        {
            return await new ConvertedGetAllWorkersOnHttpContext(
                new LoggedPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>)) as ILogger<IGetAllWorkersRequestContract>,
                    new ValidatedPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllWorkersRequestContract>)) as IValidator<IGetAllWorkersRequestContract>,
                        new GetAllWorkersUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }
    
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllWorkerRoles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkerRolesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWorkerRolesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllWorkerRoles()
        {
            return await new ConvertedGetAllWorkerRolesOnHttpContext(
                new LoggedPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllWorkerRolesRequestContract>)) as ILogger<IGetAllWorkerRolesRequestContract>,
                    new ValidatedPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllWorkerRolesRequestContract>)) as IValidator<IGetAllWorkerRolesRequestContract>,
                        new GetAllWorkerRolesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(new GetAllWorkerRolesHttpQuery());
        }

        [Authorize]
        [HttpPost]
        [Route("CreateWorker")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateWorkerSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateWorkerErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateWorker(
            [FromBody] CreateWorkerHttpBody body)
        {
            return await new ConvertedCreateWorkerOnHttpContext(
                new LoggedPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>(
                    _services.GetService(typeof(ILogger<ICreateWorkerRequestContract>)) as ILogger<ICreateWorkerRequestContract>,
                    new ValidatedPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>(
                        _services.GetService(typeof(IValidator<ICreateWorkerRequestContract>)) as IValidator<ICreateWorkerRequestContract>,
                        new CreateWorkerUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateWorkersRole")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersRoleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersRoleErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateWorkersRole(
            [FromBody] UpdateWorkersRoleHttpBody body)
        {
            return await new ConvertedUpdateWorkersRoleOnHttpContext(
                new LoggedPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersRoleRequestContract>)) as ILogger<IUpdateWorkersRoleRequestContract>,
                    new ValidatedPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateWorkersRoleRequestContract>)) as IValidator<IUpdateWorkersRoleRequestContract>,
                        new UpdateWorkersRoleUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateWorkersName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateWorkersName(
            [FromBody] UpdateWorkersNameHttpBody body)
        {
            return await new ConvertedUpdateWorkersNameOnHttpContext(
                new LoggedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>)) as ILogger<IUpdateWorkersNameRequestContract>,
                    new ValidatedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateWorkersNameRequestContract>)) as IValidator<IUpdateWorkersNameRequestContract>,
                        new UpdateWorkersNameUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateWorkersComment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersCommentErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateWorkersComment(
            [FromBody] UpdateWorkersCommentHttpBody body)
        {
            return await new ConvertedUpdateWorkersCommentOnHttpContext(
                new LoggedPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersCommentRequestContract>)) as ILogger<IUpdateWorkersCommentRequestContract>,
                    new ValidatedPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateWorkersCommentRequestContract>)) as IValidator<IUpdateWorkersCommentRequestContract>,
                        new UpdateWorkersCommentUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
    }
}