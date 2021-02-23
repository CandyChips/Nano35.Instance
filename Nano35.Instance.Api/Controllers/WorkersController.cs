﻿using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.HttpContext;
using Nano35.Instance.Api.Requests.CreateWorker;
using Nano35.Instance.Api.Requests.GetAllWorkerRoles;
using Nano35.Instance.Api.Requests.GetAllWorkers;
using Nano35.Instance.Api.Requests.UpdateWorkersComment;
using Nano35.Instance.Api.Requests.UpdateWorkersName;
using Nano35.Instance.Api.Requests.UpdateWorkersRole;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
        public class UpdateWorkersRoleHttpContext : IUpdateWorkersRoleRequestContract
        {
            public Guid WorkersId { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
            public Guid RoleId { get; set; }
        }

        public class UpdateWorkersNameHttpContext : IUpdateWorkersNameRequestContract
        {
            public Guid WorkersId { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }

            public string Name { get; set; }
        }

        public class UpdateWorkersCommentHttpContext : IUpdateWorkersCommentRequestContract
        {
            public Guid WorkersId { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
            public string Comment { get; set; }
        }
        
        private readonly IServiceProvider  _services;

        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public WorkersController(
            IServiceProvider services)
        {
            _services = services;
        }
        
        /// <summary>
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
        /// 3. Response pattern match of pipeline response;
        /// </summary>
        [HttpGet]
        [Route("GetAllWorkers")]
        public async Task<IActionResult> GetAllWorkers(
            [FromQuery] GetAllWorkersHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWorkersRequest>)_services.GetService(typeof(ILogger<LoggedGetAllWorkersRequest>));

            // Send request to pipeline
            var result =
                await new LoggedGetAllWorkersRequest(logger,
                        new ValidatedGetAllWorkersRequest(
                            new GetAllWorkersRequest(bus)
                            )
                        ).Ask(query);
            
            // Check response get all workers request
            return result switch
            {
                IGetAllWorkersSuccessResultContract success => Ok(success.Data),
                IGetAllWorkersErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllWorkerRoles")]
        public async Task<IActionResult> GetAllWorkerRoles()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllWorkerRolesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllWorkerRolesRequest>));

            // Send request to pipeline
            var result =
                await new LoggedGetAllWorkerRolesRequest(logger,
                    new GetAllWorkerRolesRequest(bus)
                ).Ask(new GetAllWorkerRolesHttpContext());

            // Check response of get all worker roles request
            return result switch
            {
                IGetAllWorkerRolesSuccessResultContract success => Ok(success.Data),
                IGetAllWorkerRolesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateWorker")]
        public async Task<IActionResult> CreateWorker(
            [FromBody]CreateWorkerHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateWorkerRequest>)_services.GetService(typeof(ILogger<LoggedCreateWorkerRequest>));

            // Send request to pipeline
            var result = 
                await new LoggedCreateWorkerRequest(logger, 
                    new ValidatedCreateWorkerRequest(
                        new CreateWorkerRequest(bus, auth)
                        )
                    ).Ask(body);
            
            // Check response create worker request
            return result switch
            {
                ICreateWorkerSuccessResultContract => Ok(),
                ICreateWorkerErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateWorkersRole")]
        public async Task<IActionResult> UpdateWorkersRole(
            [FromBody] UpdateWorkersRoleHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateWorkersRoleRequest>)_services.GetService(typeof(ILogger<LoggedUpdateWorkersRoleRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateWorkersRoleRequest(logger, 
                        new ValidatedUpdateWorkersRoleRequest(
                            new UpdateWorkersRoleRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateWorkersRoleSuccessResultContract => Ok(),
                IUpdateWorkersRoleErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateWorkersName")]
        public async Task<IActionResult> UpdateWorkersName(
            [FromBody] UpdateWorkersNameHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateWorkersNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateWorkersNameRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateWorkersNameRequest(logger, 
                        new ValidatedUpdateWorkersNameRequest(
                            new UpdateWorkersNameRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateWorkersNameSuccessResultContract => Ok(),
                IUpdateWorkersNameErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateWorkersComment")]
        public async Task<IActionResult> UpdateWorkersComment(
            [FromBody] UpdateWorkersCommentHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateWorkersCommentRequest>)_services.GetService(typeof(ILogger<LoggedUpdateWorkersCommentRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateWorkersCommentRequest(logger, 
                        new ValidatedUpdateWorkersCommentRequest(
                            new UpdateWorkersCommentRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateWorkersCommentSuccessResultContract => Ok(),
                IUpdateWorkersCommentErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}