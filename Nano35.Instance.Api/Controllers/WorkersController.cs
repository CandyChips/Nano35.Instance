using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
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

        public WorkersController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        [HttpGet]
        [Route("GetAllWorkers")]
        public async Task<IActionResult> GetAllWorkers(
            [FromQuery] GetAllWorkersHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllWorkersLogger>)_services.GetService(typeof(ILogger<GetAllWorkersLogger>));

            // Send request to pipeline
            var result =
                await new GetAllWorkersLogger(logger,
                        new GetAllWorkersValidator(
                            new GetAllWorkersRequest(bus))
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
            var logger = (ILogger<GetAllWorkerRolesLogger>)_services.GetService(typeof(ILogger<GetAllWorkerRolesLogger>));

            // Send request to pipeline
            var result =
                await new GetAllWorkerRolesLogger(logger,
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
            var logger = (ILogger<CreateWorkerLogger>)_services.GetService(typeof(ILogger<CreateWorkerLogger>));

            // Send request to pipeline
            var result = 
                await new CreateWorkerLogger(logger, 
                    new CreateWorkerValidator(
                        new CreateWorkerRequest(bus))).Ask(body);
            
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