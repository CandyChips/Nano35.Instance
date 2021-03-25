using System;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
using Nano35.Instance.Api.Requests.GetInstanceStringById;
using Nano35.Instance.Api.Requests.UpdateInstanceEmail;
using Nano35.Instance.Api.Requests.UpdateInstanceInfo;
using Nano35.Instance.Api.Requests.UpdateInstanceName;
using Nano35.Instance.Api.Requests.UpdateInstancePhone;
using Nano35.Instance.Api.Requests.UpdateInstanceRealName;
using Nano35.Instance.Api.Requests.UpdateInstanceRegion;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
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
    
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllInstances")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllInstancesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllInstancesErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllInstances(
            [FromQuery] GetAllInstancesHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllInstancesRequest>));
            var validator = (IValidator<IGetAllInstancesRequestContract>) _services.GetService(typeof(IValidator<IGetAllInstancesRequestContract>));
            
            return await 
                new ConvertedGetAllInstancesOnHttpContext(
                new LoggedGetAllInstancesRequest(logger,
                        new ValidatedGetAllInstancesRequest(validator,
                            new GetAllInstancesUseCase(bus)))).Ask(query);
            
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllCurrentInstances")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllCurrentInstances()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedGetAllCurrentInstancesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllCurrentInstancesRequest>));
            var validator = (IValidator<IGetAllInstancesRequestContract>) _services.GetService(typeof(IValidator<IGetAllInstancesRequestContract>));
            
            return await 
                new ConvertedGetAllCurrentInstancesOnHttpContext(
                new LoggedGetAllCurrentInstancesRequest(logger,
                        new ValidatedGetAllCurrentInstancesRequest(validator,
                            new GetAllCurrentInstancesUseCase(bus, auth))))
                    .Ask(new GetAllInstancesHttpQuery());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetInstanceById/Id={InstanceId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetInstanceById(
            [FromRoute] GetInstanceByIdHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetInstanceByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetInstanceByIdRequest>));
            var validator = (IValidator<IGetInstanceByIdRequestContract>) _services.GetService(typeof(IValidator<IGetInstanceByIdRequestContract>));

            return await new ConvertedGetInstanceByIdOnHttpContext( new LoggedGetInstanceByIdRequest(logger,
                    new ValidatedGetInstanceByIdRequest(validator,
                        new GetInstanceByIdUseCase(bus ))))
                    .Ask(query);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllInstanceTypes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllInstanceTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllInstanceTypesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllInstanceTypesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllInstanceTypesRequest>));
            var distributedCache = (IDistributedCache) _services.GetService(typeof(IDistributedCache));
            
           return await 
               new ConvertedGetAllInstanceTypesOnHttpContext( 
                   new LoggedGetAllInstanceTypesRequest(logger,
                        new CachedGetAllInstanceTypesRequest(distributedCache,
                            new GetAllInstanceTypesUseCase(bus))))
                    .Ask(new GetAllInstanceTypesHttpQuery());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllRegions")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllRegionsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllRegionsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllRegions()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllRegionsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllRegionsRequest>));
            
           return await
               new ConvertedGetAllRegionsOnHttpContext(
               new LoggedGetAllRegionsRequest(logger,
                    new GetAllRegionsUseCase(bus)))
                    .Ask(new GetAllRegionsHttpQuery());

        }

        [Authorize]
        [HttpPost]
        [Route("CreateInstance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateInstanceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateInstanceErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateInstance(
            [FromBody] CreateInstanceHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateInstanceRequest>)_services.GetService(typeof(ILogger<LoggedCreateInstanceRequest>));

            return await
                new ConvertedCreateInstanceOnHttpContext(
                new LoggedCreateInstanceRequest(logger, 
                    new ValidatedCreateInstanceRequest(
                        new CreateInstanceUseCase(bus, auth))))
                    .Ask(body);
        }

        [Authorize]
        [HttpPost]
        [Route("CreateCashOutput")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCashOutputSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCashOutputErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateCashOutput(
            [FromBody] CreateCashOutputHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateCashOutputRequest>)_services.GetService(typeof(ILogger<LoggedCreateCashOutputRequest>));
            
           return await 
               new ConvertedCreateCashOutputOnHttpContext( 
                   new LoggedCreateCashOutputRequest(logger,
                       new CreateCashOutputUseCase(bus, auth)))
                    .Ask(body);
        }

        [Authorize]
        [HttpPost]
        [Route("CreateCashInput")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCashInputSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCashInputErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateCashInput(
            [FromBody] CreateCashInputHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateCashInputRequest>)_services.GetService(typeof(ILogger<LoggedCreateCashInputRequest>));
            
            return await 
                new ConvertedCreateCashInputOnHttpContext(
                        new LoggedCreateCashInputRequest(logger, 
                            new CreateCashInputUseCase(bus, auth)))
                    .Ask(body);
          
        }
        
        [Authorize]
        [HttpPatch]
        [Route("UpdateInstanceEmail")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceEmailErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceEmail(
            [FromBody] UpdateInstanceEmailHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceEmailRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceEmailRequest>));
            
            return await 
                new ConvertedUpdateInstanceEmailOnHttpContext( 
                    new LoggedUpdateInstanceEmailRequest(logger, 
                        new ValidatedUpdateInstanceEmailRequest(
                            new UpdateInstanceEmailUseCase(bus))))
                    .Ask(body);

        }
        
        [Authorize]
        [HttpPatch]
        [Route("UpdateInstanceInfo")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceInfoErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceInfo(
            [FromBody] UpdateInstanceInfoHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceInfoRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceInfoRequest>));
            
            return await 
                new ConvertedUpdateInstanceInfoOnHttpContext( 
                    new LoggedUpdateInstanceInfoRequest(logger, 
                        new ValidatedUpdateInstanceInfoRequest(
                            new UpdateInstanceInfoUseCase(bus))))
                    .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateInstanceName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateInstanceName(
            [FromBody] UpdateInstanceNameHttpBody body)
        {
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger =
                (ILogger<LoggedUpdateInstanceNameRequest>) _services.GetService(
                    typeof(ILogger<LoggedUpdateInstanceNameRequest>));

            return await 
                new ConvertedUpdateInstanceNameOnHttpContext( 
                        new LoggedUpdateInstanceNameRequest(logger, 
                            new ValidatedUpdateInstanceNameRequest(
                                new UpdateInstanceNameUseCase(bus))))
                    .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateInstancePhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstancePhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstancePhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstancePhone(
            [FromBody] UpdateInstancePhoneHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstancePhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstancePhoneRequest>));
            
            return await 
                new ConvertedUpdateInstancePhoneOnHttpContext( 
                        new LoggedUpdateInstancePhoneRequest(logger, 
                            new ValidatedUpdateInstancePhoneRequest(
                                new UpdateInstancePhoneUseCase(bus))))
                    .Ask(body);

        }
        
        [Authorize]
        [HttpPatch]
        [Route("UpdateInstanceRealName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRealNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRealNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceRealName(
            [FromBody] UpdateInstanceRealNameHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedUpdateInstanceRealNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceRealNameRequest>));

            return await 
                new ConvertedUpdateInstanceRealNameOnHttpContext( 
                        new LoggedUpdateInstanceRealNameRequest(logger, 
                            new ValidatedUpdateInstanceRealNameRequest(
                                new UpdateInstanceRealNameUseCase(bus))))
                    .Ask(body);

        }
        
        [Authorize]
        [HttpPatch]
        [Route("UpdateInstanceRegion")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRegionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRegionErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceRegion(
            [FromBody] UpdateInstanceRegionHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateInstanceRegionRequest>)_services.GetService(typeof(ILogger<LoggedUpdateInstanceRegionRequest>));

            return await 
                new ConvertedUpdateInstanceRegionOnHttpContext( 
                        new LoggedUpdateInstanceRegionRequest(logger, 
                            new ValidatedUpdateInstanceRegionRequest(
                                new UpdateInstanceRegionUseCase(bus, auth))))
                    .Ask(body);
        }
    }
}