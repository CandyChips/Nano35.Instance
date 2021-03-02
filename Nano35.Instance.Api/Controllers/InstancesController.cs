using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests.CreateCashInput;
using Nano35.Instance.Api.Requests.CreateCashOutput;
using Nano35.Instance.Api.Requests.CreateInstance;
using Nano35.Instance.Api.Requests.GetAllCurrentInstances;
using Nano35.Instance.Api.Requests.GetAllInstances;
using Nano35.Instance.Api.Requests.GetAllInstanceTypes;
using Nano35.Instance.Api.Requests.GetAllRegions;
using Nano35.Instance.Api.Requests.GetInstanceById;
using Nano35.Instance.Api.Requests.UpdateInstanceEmail;
using Nano35.Instance.Api.Requests.UpdateInstanceInfo;
using Nano35.Instance.Api.Requests.UpdateInstanceName;
using Nano35.Instance.Api.Requests.UpdateInstancePhone;
using Nano35.Instance.Api.Requests.UpdateInstanceRealName;
using Nano35.Instance.Api.Requests.UpdateInstanceRegion;

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
            [FromQuery] GetAllInstancesHttpContext.GetAllInstancesQuery query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllInstancesRequest>));

            var request = new GetAllInstancesRequestContract()
            {
                InstanceTypeId = query.InstanceTypeId,
                RegionId = query.RegionId,
                UserId = query.UserId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedGetAllInstancesRequest(logger,
                        new ValidatedGetAllInstancesRequest(
                            new GetAllInstancesRequest(bus)))
                    .Ask(request);
            
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
        [Route("GetAllCurrentInstances")]
        public async Task<IActionResult> GetAllCurrentInstances()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedGetAllCurrentInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllCurrentInstancesRequest>));

            // Send request to pipeline
            var result = 
                await new LoggedGetAllCurrentInstancesRequest(logger,
                        new ValidatedGetAllCurrentInstancesRequest(
                            new GetAllCurrentInstancesRequest(bus, auth)))
                    .Ask(new GetAllInstancesRequestContract());
            
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
            [FromRoute] GetInstanceByIdHttpContext.GetInstanceByIdQuery query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetInstanceByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetInstanceByIdRequest>));

            var request = new GetInstanceByIdRequestContract()
            {
                InstanceId = query.InstanceId
            };
            
            // Send request to pipeline
            var result =
                await new LoggedGetInstanceByIdRequest(logger,
                    new ValidatedGetInstanceByIdRequest(
                        new GetInstanceByIdRequest(bus)))
                    .Ask(request);

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

            var request = new GetAllInstanceTypesRequestContract();
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllInstanceTypesRequest(logger,
                    new GetAllInstanceTypesRequest(bus))
                    .Ask(request);

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

            var request = new GetAllRegionsRequestContract();
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllRegionsRequest(logger,
                    new GetAllRegionsRequest(bus))
                    .Ask(request);

            // Check response
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
            [FromBody]CreateInstanceHttpContext.CreateInstanceBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateInstanceRequest>)_services.GetService(typeof(ILogger<LoggedCreateInstanceRequest>));

            var request = new CreateInstanceRequestContract()
            {
                Email = body.Email,
                Info = body.Info,
                Name = body.Name,
                RealName = body.RealName,
                NewId = body.NewId,
                Phone = body.Phone,
                RegionId = body.RegionId,
                TypeId = body.TypeId,
                UserId = body.UserId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateInstanceRequest(logger, 
                    new ValidatedCreateInstanceRequest(
                        new CreateInstanceRequest(bus, auth)))
                    .Ask(request);
            
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
            [FromBody] CreateCashOutputHttpContext.CreateCashOutputBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateCashOutputRequest>)_services.GetService(typeof(ILogger<LoggedCreateCashOutputRequest>));

            var request = new CreateCashOutputRequestContract()
            {
                Cash = body.Cash,
                Description = body.Description,
                InstanceId = body.InstanceId,
                NewId = body.NewId,
                UnitId = body.UnitId,
                WorkerId = body.UpdaterId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCashOutputRequest(logger, 
                    new CreateCashOutputRequest(bus, auth))
                    .Ask(request);
            
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
            [FromBody]CreateCashInputHttpContext.CreateCashInputBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateCashInputRequest>)_services.GetService(typeof(ILogger<LoggedCreateCashInputRequest>));
            
            var request = new CreateCashInputRequestContract()
            {
                Cash = body.Cash,
                Description = body.Description,
                InstanceId = body.InstanceId,
                NewId = body.NewId,
                UnitId = body.UnitId,
                WorkerId = body.UpdaterId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateCashInputRequest(logger, 
                    new CreateCashInputRequest(bus, auth))
                    .Ask(request);
            
            // Check response
            return result switch
            {
                ICreateCashInputSuccessResultContract => Ok(),
                ICreateCashInputErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceEmail")]
        public async Task<IActionResult> UpdateInstanceEmail(
            [FromBody] UpdateInstanceEmailHttpContext.UpdateInstanceEmailBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceEmailRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceEmailRequest>));

            var request = new UpdateInstanceEmailRequestContract()
            {
                Email = body.Email, 
                InstanceId = body.InstanceId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateInstanceEmailRequest(logger, 
                        new ValidatedUpdateInstanceEmailRequest(
                            new UpdateInstanceEmailRequest(bus)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateInstanceEmailSuccessResultContract => Ok(),
                IUpdateInstanceEmailErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceInfo")]
        public async Task<IActionResult> UpdateInstanceInfo(
            [FromBody] UpdateInstanceInfoHttpContext.UpdateInstanceInfoBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceInfoRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceInfoRequest>));

            var request = new UpdateInstanceInfoRequestContract()
            {
                Info = body.Info,
                InstanceId = body.InstanceId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateInstanceInfoRequest(logger, 
                        new ValidatedUpdateInstanceInfoRequest(
                            new UpdateInstanceInfoRequest(bus)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateInstanceInfoSuccessResultContract => Ok(),
                IUpdateInstanceInfoErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceName")]
        public async Task<IActionResult> UpdateInstanceName(
            [FromBody] UpdateInstanceNameHttpContext.UpdateInstanceNameBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceNameRequest>));

            var request = new UpdateInstanceNameRequestContract()
            {
                Name = body.Name,
                InstanceId = body.InstanceId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateInstanceNameRequest(logger, 
                        new ValidatedUpdateInstanceNameRequest(
                            new UpdateInstanceNameRequest(bus)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateInstanceNameSuccessResultContract => Ok(),
                IUpdateInstanceNameErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstancePhone")]
        public async Task<IActionResult> UpdateInstancePhone(
            [FromBody] UpdateInstancePhoneHttpContext.UpdateInstancePhoneBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstancePhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstancePhoneRequest>));

            var request = new UpdateInstancePhoneRequestContract()
            {
                Phone = body.Phone,
                InstanceId = body.InstanceId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateInstancePhoneRequest(logger, 
                        new ValidatedUpdateInstancePhoneRequest(
                            new UpdateInstancePhoneRequest(bus)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateInstancePhoneSuccessResultContract => Ok(),
                IUpdateInstancePhoneErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceRealName")]
        public async Task<IActionResult> UpdateInstanceRealName(
            [FromBody] UpdateInstanceRealNameHttpContext.UpdateInstanceRealNameBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceRealNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceRealNameRequest>));

            var request = new UpdateInstanceRealNameRequestContract()
            {
                RealName = body.RealName,
                InstanceId = body.InstanceId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateInstanceRealNameRequest(logger, 
                        new ValidatedUpdateInstanceRealNameRequest(
                            new UpdateInstanceRealNameRequest(bus)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateInstanceRealNameSuccessResultContract => Ok(),
                IUpdateInstanceRealNameErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceRegion")]
        public async Task<IActionResult> UpdateInstanceRegion(
            [FromBody] UpdateInstanceRegionHttpContext.UpdateInstanceRegionBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateInstanceRegionRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceRegionRequest>));

            var request = new UpdateInstanceRegionRequestContract()
            {
                RegionId = body.RegionId,
                InstanceId = body.InstanceId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateInstanceRegionRequest(logger, 
                        new ValidatedUpdateInstanceRegionRequest(
                            new UpdateInstanceRegionRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateInstanceRegionSuccessResultContract => Ok(),
                IUpdateInstanceRegionErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}