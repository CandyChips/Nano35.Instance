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

        public WorkersController(
            IServiceProvider services)
        {
            _services = services;
        }
        
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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWorkersRequest>)_services.GetService(typeof(ILogger<LoggedGetAllWorkersRequest>));
            var validator = (IValidator<IGetAllWorkersRequestContract>) _services.GetService(typeof(IValidator<IGetAllWorkersRequestContract>));
            
            return await 
                new ConvertedGetAllWorkersOnHttpContext(
                    new LoggedGetAllWorkersRequest(logger,
                        new ValidatedGetAllWorkersRequest(validator,
                            new GetAllWorkersUseCase(bus)))).Ask(query);
        }
    
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllWorkerRoles")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllWorkerRolesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllWorkerRolesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllWorkerRoles()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWorkerRolesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllWorkerRolesRequest>));
            
            return await 
                new ConvertedGetAllWorkerRolesOnHttpContext(
                    new LoggedGetAllWorkerRolesRequest(logger,
                            new GetAllWorkerRolesUseCase(bus)))
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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateWorkerRequest>)_services.GetService(typeof(ILogger<LoggedCreateWorkerRequest>));

            return await 
                new ConvertedCreateWorkerOnHttpContext(
                    new LoggedCreateWorkerRequest(logger,
                        new ValidatedCreateWorkerRequest(
                            new CreateWorkerUseCase(bus, auth)))).Ask(body);
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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateWorkersRoleRequest>)_services.GetService(typeof(ILogger<LoggedUpdateWorkersRoleRequest>));

            
            return await 
                new ConvertedUpdateWorkersRoleOnHttpContext(
                    new LoggedUpdateWorkersRoleRequest(logger,
                        new ValidatedUpdateWorkersRoleRequest(
                            new UpdateWorkersRoleUseCase(bus, auth)))).Ask(body);
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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateWorkersNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateWorkersNameRequest>));

            
            return await 
                new ConvertedUpdateWorkersNameOnHttpContext(
                    new LoggedUpdateWorkersNameRequest(logger,
                        new ValidatedUpdateWorkersNameRequest(
                            new UpdateWorkersNameUseCase(bus, auth)))).Ask(body);
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
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateWorkersCommentRequest>)_services.GetService(typeof(ILogger<LoggedUpdateWorkersCommentRequest>));

            
            return await 
                new ConvertedUpdateWorkersCommentOnHttpContext(
                    new LoggedUpdateWorkersCommentRequest(logger,
                        new ValidatedUpdateWorkersCommentRequest(
                            new UpdateWorkersCommentUseCase(bus, auth)))).Ask(body);
        }
    }
}