using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests.CreateInstance;
using Nano35.Instance.Api.Requests.GetAllInstances;
using Nano35.Instance.Api.Requests.GetAllInstanceTypes;
using Nano35.Instance.Api.Requests.GetAllRegions;
using Nano35.Instance.Api.Requests.GetInstanceById;
using Nano35.Instance.HttpContracts;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstancesController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        public InstancesController(
            IServiceProvider services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("GetAllInstances")]
        public async Task<IActionResult> GetAllInstances(
            [FromQuery] GetAllInstancesHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllInstancesLogger>)_services.GetService(typeof(ILogger<GetAllInstancesLogger>));
            
            // Send request to pipeline
            var result = 
                await new GetAllInstancesLogger(logger,
                    new GetAllInstancesValidator(
                        new GetAllInstancesRequest(bus))
                    ).Ask(query);
            
            // Check response of get all instances request
            return result switch
            {
                IGetAllInstancesSuccessResultContract success => Ok(success.Data),
                IGetAllInstancesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetInstanceById/Id={id}")]
        public async Task<IActionResult> GetInstanceById(
            [FromRoute] GetInstanceByIdHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetInstanceByIdLogger>)_services.GetService(typeof(ILogger<GetInstanceByIdLogger>));
            
            // Send request to pipeline
            var result =
                await new GetInstanceByIdLogger(logger,
                    new GetInstanceByIdValidator(
                        new GetInstanceByIdRequest(bus))
                    ).Ask(query);

            // Check response of get instance by id request
            return result switch
            {
                IGetInstanceByIdSuccessResultContract success => Ok(success.Data),
                IGetInstanceByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllInstanceTypes")]
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllInstanceTypesLogger>)_services.GetService(typeof(ILogger<GetAllInstanceTypesLogger>));
            
            // Send request to pipeline
            var result =
                await new GetAllInstanceTypesLogger(logger,
                    new GetAllInstanceTypesRequest(bus)
                ).Ask(new GetAllInstanceTypesHttpContext());

            // Check response of get all instance types request
            return result switch
            {
                IGetAllInstanceTypesSuccessResultContract success => Ok(success.Data),
                IGetAllInstanceTypesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllRegionsLogger>)_services.GetService(typeof(ILogger<GetAllRegionsLogger>));
            
            // Send request to pipeline
            var result =
                await new GetAllRegionsLogger(logger,
                    new GetAllRegionsRequest(bus)
                ).Ask(new GetAllRegionsHttpContext());

            // Check responce
            return result switch
            {
                IGetAllRegionsSuccessResultContract success => Ok(success.Data),
                IGetAllRegionsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateInstance")]
        public async Task<IActionResult> CreateInstance(
            [FromBody]CreateInstanceHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<CreateInstanceLogger>)_services.GetService(typeof(ILogger<CreateInstanceLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateInstanceLogger(logger, 
                    new CreateInstanceValidator(
                        new CreateInstanceRequest(bus, auth))
                    ).Ask(body);
            
            // Check responce
            return result switch
            {
                ICreateInstanceSuccessResultContract => Ok(),
                ICreateInstanceErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}