﻿using System;
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

        public InstancesController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        [HttpGet]
        [Route("GetAllInstances")]
        public async Task<IActionResult> GetAllInstances(
            [FromQuery] GetAllInstancesHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllInstancesRequest>));

            var request = new GetAllInstancesRequestContract()
            {
                InstanceTypeId = query.InstanceTypeId,
                RegionId = query.RegionId,
                UserId = query.UserId
            };
            
            var result = 
                await new LoggedGetAllInstancesRequest(logger,
                        new ValidatedGetAllInstancesRequest(
                            new GetAllInstancesRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetAllInstancesSuccessResultContract success => Ok(success),
                IGetAllInstancesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpGet]
        [Route("GetAllCurrentInstances")]
        public async Task<IActionResult> GetAllCurrentInstances()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedGetAllCurrentInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllCurrentInstancesRequest>));

            var result = 
                await new LoggedGetAllCurrentInstancesRequest(logger,
                        new ValidatedGetAllCurrentInstancesRequest(
                            new GetAllCurrentInstancesRequest(bus, auth)))
                    .Ask(new GetAllInstancesRequestContract());
            
            return result switch
            {
                IGetAllInstancesSuccessResultContract success => Ok(success),
                IGetAllInstancesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetInstanceById/Id={id}")]
        public async Task<IActionResult> GetInstanceById(
            [FromRoute] GetInstanceByIdHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetInstanceByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetInstanceByIdRequest>));

            var request = new GetInstanceByIdRequestContract()
            {
                InstanceId = query.InstanceId
            };
            
            var result =
                await new LoggedGetInstanceByIdRequest(logger,
                    new ValidatedGetInstanceByIdRequest(
                        new GetInstanceByIdRequest(bus)))
                    .Ask(request);
            return result switch
            {
                IGetInstanceByIdSuccessResultContract success => Ok(success),
                IGetInstanceByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllInstanceTypes")]
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstanceTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllInstanceTypesRequest>));

            var request = new GetAllInstanceTypesRequestContract();
            
            var result =
                await new LoggedGetAllInstanceTypesRequest(logger,
                    new GetAllInstanceTypesRequest(bus))
                    .Ask(request);

            return result switch
            {
                IGetAllInstanceTypesSuccessResultContract success => Ok(success),
                IGetAllInstanceTypesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllRegionsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllRegionsRequest>));

            var request = new GetAllRegionsRequestContract();
            
            var result =
                await new LoggedGetAllRegionsRequest(logger,
                    new GetAllRegionsRequest(bus))
                    .Ask(request);

            return result switch
            {
                IGetAllRegionsSuccessResultContract success => Ok(success),
                IGetAllRegionsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateInstance")]
        public async Task<IActionResult> CreateInstance(
            [FromBody] CreateInstanceHttpBody body)
        {
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
            
            var result = 
                await new LoggedCreateInstanceRequest(logger, 
                    new ValidatedCreateInstanceRequest(
                        new CreateInstanceRequest(bus, auth)))
                    .Ask(request);
            
            return result switch
            {
                ICreateInstanceSuccessResultContract success => Ok(success),
                ICreateInstanceErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateCashOutput")]
        public async Task<IActionResult> CreateCashOutput(
            [FromBody] CreateCashOutputHttpBody body)
        {
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
            
            var result = 
                await new LoggedCreateCashOutputRequest(logger, 
                    new CreateCashOutputRequest(bus, auth))
                    .Ask(request);
            
            return result switch
            {
                ICreateCashOutputSuccessResultContract success => Ok(success),
                ICreateCashOutputErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateCashInput")]
        public async Task<IActionResult> CreateCashInput(
            [FromBody] CreateCashInputHttpBody body)
        {
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
            
            var result = 
                await new LoggedCreateCashInputRequest(logger, 
                    new CreateCashInputRequest(bus, auth))
                    .Ask(request);
            
            return result switch
            {
                ICreateCashInputSuccessResultContract success => Ok(success),
                ICreateCashInputErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceEmail")]
        public async Task<IActionResult> UpdateInstanceEmail(
            [FromBody] UpdateInstanceEmailHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceEmailRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceEmailRequest>));

            var request = new UpdateInstanceEmailRequestContract()
            {
                Email = body.Email, 
                InstanceId = body.InstanceId
            };
            
            var result = 
                await new LoggedUpdateInstanceEmailRequest(logger, 
                        new ValidatedUpdateInstanceEmailRequest(
                            new UpdateInstanceEmailRequest(bus)))
                    .Ask(request);

            return result switch
            {
                IUpdateInstanceEmailSuccessResultContract success => Ok(success),
                IUpdateInstanceEmailErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceInfo")]
        public async Task<IActionResult> UpdateInstanceInfo(
            [FromBody] UpdateInstanceInfoHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceInfoRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceInfoRequest>));

            var request = new UpdateInstanceInfoRequestContract()
            {
                Info = body.Info,
                InstanceId = body.InstanceId
            };
            
            var result = 
                await new LoggedUpdateInstanceInfoRequest(logger, 
                        new ValidatedUpdateInstanceInfoRequest(
                            new UpdateInstanceInfoRequest(bus)))
                    .Ask(request);

            return result switch
            {
                IUpdateInstanceInfoSuccessResultContract success => Ok(success),
                IUpdateInstanceInfoErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceName")]
        public async Task<IActionResult> UpdateInstanceName(
            [FromBody] UpdateInstanceNameHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceNameRequest>));

            var request = new UpdateInstanceNameRequestContract()
            {
                Name = body.Name,
                InstanceId = body.InstanceId
            };
            
            var result = 
                await new LoggedUpdateInstanceNameRequest(logger, 
                        new ValidatedUpdateInstanceNameRequest(
                            new UpdateInstanceNameRequest(bus)))
                    .Ask(request);

            return result switch
            {
                IUpdateInstanceNameSuccessResultContract success => Ok(success),
                IUpdateInstanceNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstancePhone")]
        public async Task<IActionResult> UpdateInstancePhone(
            [FromBody] UpdateInstancePhoneHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstancePhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstancePhoneRequest>));

            var request = new UpdateInstancePhoneRequestContract()
            {
                Phone = body.Phone,
                InstanceId = body.InstanceId
            };
            
            var result = 
                await new LoggedUpdateInstancePhoneRequest(logger, 
                        new ValidatedUpdateInstancePhoneRequest(
                            new UpdateInstancePhoneRequest(bus)))
                    .Ask(request);

            return result switch
            {
                IUpdateInstancePhoneSuccessResultContract success => Ok(success),
                IUpdateInstancePhoneErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceRealName")]
        public async Task<IActionResult> UpdateInstanceRealName(
            [FromBody] UpdateInstanceRealNameHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceRealNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceRealNameRequest>));

            var request = new UpdateInstanceRealNameRequestContract()
            {
                RealName = body.RealName,
                InstanceId = body.InstanceId
            };
            
            var result = 
                await new LoggedUpdateInstanceRealNameRequest(logger, 
                        new ValidatedUpdateInstanceRealNameRequest(
                            new UpdateInstanceRealNameRequest(bus)))
                    .Ask(request);

            return result switch
            {
                IUpdateInstanceRealNameSuccessResultContract success => Ok(success),
                IUpdateInstanceRealNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpPatch]
        [Route("UpdateInstanceRegion")]
        public async Task<IActionResult> UpdateInstanceRegion(
            [FromBody] UpdateInstanceRegionHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateInstanceRegionRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceRegionRequest>));

            var request = new UpdateInstanceRegionRequestContract()
            {
                RegionId = body.RegionId,
                InstanceId = body.InstanceId
            };
            
            var result = 
                await new LoggedUpdateInstanceRegionRequest(logger, 
                        new ValidatedUpdateInstanceRegionRequest(
                            new UpdateInstanceRegionRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateInstanceRegionSuccessResultContract success => Ok(success),
                IUpdateInstanceRegionErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}