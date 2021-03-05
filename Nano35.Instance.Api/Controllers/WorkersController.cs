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

            var request = new GetAllWorkersRequestContract()
            {
                InstanceId = query.InstanceId,
                WorkersRoleId = query.WorkersRoleId
            };
            
            var result =
                await new LoggedGetAllWorkersRequest(logger,
                        new ValidatedGetAllWorkersRequest(
                            new GetAllWorkersRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetAllWorkersSuccessResultContract success => Ok(success),
                IGetAllWorkersErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new GetAllWorkerRolesRequestContract();
            
            var result =
                await new LoggedGetAllWorkerRolesRequest(logger,
                    new GetAllWorkerRolesRequest(bus))
                    .Ask(request);

            return result switch
            {
                IGetAllWorkerRolesSuccessResultContract success => Ok(success),
                IGetAllWorkerRolesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new CreateWorkerRequestContract()
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
            };
            
            var result = 
                await new LoggedCreateWorkerRequest(logger, 
                    new ValidatedCreateWorkerRequest(
                        new CreateWorkerRequest(bus, auth)))
                    .Ask(request);
            
            return result switch
            {
                ICreateWorkerSuccessResultContract success => Ok(success),
                ICreateWorkerErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateWorkersRoleRequestContract()
            {
                WorkersId = body.WorkersId,
                RoleId = body.RoleId
            };
            
            var result = 
                await new LoggedUpdateWorkersRoleRequest(logger, 
                        new ValidatedUpdateWorkersRoleRequest(
                            new UpdateWorkersRoleRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateWorkersRoleSuccessResultContract success => Ok(success),
                IUpdateWorkersRoleErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateWorkersNameRequestContract()
            {
                Name = body.Name,
                WorkersId = body.WorkersId
            };
            
            var result = 
                await new LoggedUpdateWorkersNameRequest(logger, 
                        new ValidatedUpdateWorkersNameRequest(
                            new UpdateWorkersNameRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateWorkersNameSuccessResultContract success => Ok(success),
                IUpdateWorkersNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
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

            var request = new UpdateWorkersCommentRequestContract()
            {
                Comment = body.Comment,
                WorkersId = body.WorkersId
            };
            
            var result = 
                await new LoggedUpdateWorkersCommentRequest(logger, 
                        new ValidatedUpdateWorkersCommentRequest(
                            new UpdateWorkersCommentRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateWorkersCommentSuccessResultContract success => Ok(success),
                IUpdateWorkersCommentErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}