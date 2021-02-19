using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.HttpContext;
using Nano35.Instance.Api.Requests.CreateCashInput;
using Nano35.Instance.Api.Requests.CreateCashOutput;
using Nano35.Instance.Api.Requests.CreateInstance;
using Nano35.Instance.Api.Requests.GetAllInstances;
using Nano35.Instance.Api.Requests.GetAllInstanceTypes;
using Nano35.Instance.Api.Requests.GetAllRegions;
using Nano35.Instance.Api.Requests.GetInstanceById;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstancesController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public InstancesController(
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
        [Route("GetAllInstances")]
        public async Task<IActionResult> GetAllInstances(
            [FromQuery] GetAllInstancesHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllInstancesRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllInstancesRequest(logger,
                    new ValidatedGetAllInstancesRequest(
                        new GetAllInstancesRequest(bus))
                    ).Ask(query);
            
            // Check response of get all instances request
            return result switch
            {
                IGetAllInstancesSuccessResultContract success =>
                    Ok(success.Data),
                IGetAllInstancesErrorResultContract error => 
                    BadRequest(error.Message),
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
            var logger = (ILogger<LoggedGetInstanceByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetInstanceByIdRequest>));
            
            // Send request to pipeline
            var result =
                await new LoggedGetInstanceByIdRequest(logger,
                    new ValidatedGetInstanceByIdRequest(
                        new GetInstanceByIdRequest(bus))
                    ).Ask(query);

            // Check response of get instance by id request
            return result switch
            {
                IGetInstanceByIdSuccessResultContract success => 
                    Ok(success.Data),
                IGetInstanceByIdErrorResultContract error =>
                    BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllInstanceTypes")]
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstanceTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllInstanceTypesRequest>));
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllInstanceTypesRequest(logger,
                    new GetAllInstanceTypesRequest(bus)
                ).Ask(new GetAllInstanceTypesHttpContext());

            // Check response of get all instance types request
            return result switch
            {
                IGetAllInstanceTypesSuccessResultContract success => 
                    Ok(success.Data),
                IGetAllInstanceTypesErrorResultContract error => 
                    BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllRegionsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllRegionsRequest>));
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllRegionsRequest(logger,
                    new GetAllRegionsRequest(bus)
                ).Ask(new GetAllRegionsHttpContext());

            // Check response
            return result switch
            {
                IGetAllRegionsSuccessResultContract success => 
                    Ok(success.Data),
                IGetAllRegionsErrorResultContract error => 
                    BadRequest(error.Message),
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
            var logger = (ILogger<LoggedCreateInstanceRequest>)_services.GetService(typeof(ILogger<LoggedCreateInstanceRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateInstanceRequest(logger, 
                    new ValidatedCreateInstanceRequest(
                        new CreateInstanceRequest(bus, auth)
                    )
                ).Ask(body);
            
            // Check response
            return result switch
            {
                ICreateInstanceSuccessResultContract => Ok(),
                ICreateInstanceErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateCashOutput")]
        public async Task<IActionResult> CreateCashOutput(
            [FromBody]CreateCashOutputHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateCashOutputRequest>)_services.GetService(typeof(ILogger<LoggedCreateCashOutputRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCashOutputRequest(logger, 
                    new CreateCashOutputRequest(bus, auth)
                ).Ask(body);
            
            // Check response
            return result switch
            {
                ICreateCashOutputSuccessResultContract => Ok(),
                ICreateCashOutputErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateCashInput")]
        public async Task<IActionResult> CreateCashInput(
            [FromBody]CreateCashInputHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateCashInputRequest>)_services.GetService(typeof(ILogger<LoggedCreateCashInputRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCashInputRequest(logger, 
                    new CreateCashInputRequest(bus, auth)
                ).Ask(body);
            
            // Check response
            return result switch
            {
                ICreateCashInputSuccessResultContract => Ok(),
                ICreateCashInputErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}