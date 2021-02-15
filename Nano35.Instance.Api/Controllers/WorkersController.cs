using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.CreateWorker;
using Nano35.Instance.Api.Requests.GetAllClients;
using Nano35.Instance.Api.Requests.GetAllClientTypes;
using Nano35.Instance.Api.Requests.GetAllWorkerRoles;
using Nano35.Instance.Api.Requests.GetAllWorkers;
using Nano35.Instance.HttpContracts;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkersController : ControllerBase
    {
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
    }
}