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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkersSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWorkersErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllWorkers(
            [FromQuery] GetAllWorkersHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllWorkersHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllWorkersHttpQuery>)) as IValidator<GetAllWorkersHttpQuery>,
                    new ConvertedGetAllWorkersOnHttpContext(
                        new LoggedPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>)) as ILogger<IGetAllWorkersRequestContract>,
                            new GetAllWorkersUseCase(
                                    _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }

        [Authorize]
        [HttpPost]
        [Route("Worker")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateWorkerSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateWorkerErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateWorker(
            [FromBody] CreateWorkerHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateWorkerHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateWorkerHttpBody>)) as IValidator<CreateWorkerHttpBody>,
                    new ConvertedCreateWorkerOnHttpContext(
                        new LoggedPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>(
                            _services.GetService(typeof(ILogger<ICreateWorkerRequestContract>)) as ILogger<ICreateWorkerRequestContract>,
                            new CreateWorkerUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Role")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersRoleSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersRoleErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateWorkersRole(
            [FromBody] UpdateWorkersRoleHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateWorkersRoleHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateWorkersRoleHttpBody>)) as IValidator<UpdateWorkersRoleHttpBody>,
                    new ConvertedUpdateWorkersRoleOnHttpContext(
                        new LoggedPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateWorkersRoleRequestContract>)) as ILogger<IUpdateWorkersRoleRequestContract>,
                            new UpdateWorkersRoleUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateWorkersName(
            [FromBody] UpdateWorkersNameHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateWorkersNameHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateWorkersNameHttpBody>)) as IValidator<UpdateWorkersNameHttpBody>,
                    new ConvertedUpdateWorkersNameOnHttpContext(
                        new LoggedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>)) as ILogger<IUpdateWorkersNameRequestContract>,
                             new UpdateWorkersNameUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Comment")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateWorkersCommentSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateWorkersCommentErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateWorkersComment(
            [FromBody] UpdateWorkersCommentHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateWorkersCommentHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateWorkersCommentHttpBody>)) as IValidator<UpdateWorkersCommentHttpBody>,
                    new ConvertedUpdateWorkersCommentOnHttpContext(
                        new LoggedPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateWorkersCommentRequestContract>)) as ILogger<IUpdateWorkersCommentRequestContract>,
                            new UpdateWorkersCommentUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
    }
}