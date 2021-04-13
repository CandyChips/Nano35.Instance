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
using Nano35.Instance.Api.Requests;
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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InstancesController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        public InstancesController(IServiceProvider services) { _services = services; }
    
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
            return await new ConvertedGetAllInstancesOnHttpContext(
                new LoggedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                    new ValidatedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllInstancesRequestContract>)) as IValidator<IGetAllInstancesRequestContract>,
                        new GetAllInstancesUseCase(_services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
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
            return await new ConvertedGetAllCurrentInstancesOnHttpContext(
                new LoggedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                    new ValidatedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                        _services.GetService(typeof(IValidator<IGetAllInstancesRequestContract>)) as IValidator<IGetAllInstancesRequestContract>,
                        new GetAllCurrentInstancesUseCase(_services.GetService(typeof(IBus)) as IBus, _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
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
            return await new ConvertedGetInstanceByIdOnHttpContext( 
                new LoggedPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetInstanceByIdRequestContract>)) as ILogger<IGetInstanceByIdRequestContract>,
                    new ValidatedPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(
                        _services.GetService(typeof(IValidator<IGetInstanceByIdRequestContract>)) as IValidator<IGetInstanceByIdRequestContract>,
                        new GetInstanceByIdUseCase(_services.GetService(typeof(IBus)) as IBus ))))
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
            return await new ConvertedGetAllInstanceTypesOnHttpContext( 
                new LoggedPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstanceTypesRequestContract>)) as ILogger<IGetAllInstanceTypesRequestContract>,
                    new ValidatedPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract>(                      
                        _services.GetService(typeof(IValidator<IGetAllInstanceTypesRequestContract>)) as IValidator<IGetAllInstanceTypesRequestContract>,
                        new GetAllInstanceTypesUseCase(_services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedGetAllRegionsOnHttpContext(
                new LoggedPipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>(
                   _services.GetService(typeof(ILogger<IGetAllRegionsRequestContract>)) as ILogger<IGetAllRegionsRequestContract>,
                   new ValidatedPipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>(
                       _services.GetService(typeof(IValidator<IGetAllRegionsRequestContract>)) as IValidator<IGetAllRegionsRequestContract>,
                       new GetAllRegionsUseCase(_services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedCreateInstanceOnHttpContext(
                new LoggedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(
                    _services.GetService(typeof(ILogger<ICreateInstanceRequestContract>)) as ILogger<ICreateInstanceRequestContract>,
                    new ValidatedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(
                        _services.GetService(typeof(IValidator<ICreateInstanceRequestContract>)) as IValidator<ICreateInstanceRequestContract>,
                        new CreateInstanceUseCase(_services.GetService(typeof(IBus)) as IBus, _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
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
            return await new ConvertedCreateCashOutputOnHttpContext( 
                new LoggedPipeNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract>(
                    _services.GetService(typeof(ILogger<ICreateCashOutputRequestContract>)) as ILogger<ICreateCashOutputRequestContract>,
                    new ValidatedPipeNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract>(
                       _services.GetService(typeof(IValidator<ICreateCashOutputRequestContract>)) as IValidator<ICreateCashOutputRequestContract>,
                    new CreateCashOutputUseCase(_services.GetService(typeof(IBus)) as IBus, _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
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
            return await new ConvertedCreateCashInputOnHttpContext(
                new LoggedPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract>(
                    _services.GetService(typeof(ILogger<ICreateCashInputRequestContract>)) as ILogger<ICreateCashInputRequestContract>,
                    new ValidatedPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract>(
                        _services.GetService(typeof(IValidator<ICreateCashInputRequestContract>)) as IValidator<ICreateCashInputRequestContract>,
                    new CreateCashInputUseCase(_services.GetService(typeof(IBus)) as IBus, _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
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
            return await new ConvertedUpdateInstanceEmailOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceEmailRequestContract>)) as ILogger<IUpdateInstanceEmailRequestContract>,
                    new ValidatedPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateInstanceEmailRequestContract>)) as IValidator<IUpdateInstanceEmailRequestContract>,
                        new UpdateInstanceEmailUseCase(_services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedUpdateInstanceInfoOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceInfoRequestContract>)) as ILogger<IUpdateInstanceInfoRequestContract>,
                    new ValidatedPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateInstanceInfoRequestContract>)) as IValidator<IUpdateInstanceInfoRequestContract>,
                        new UpdateInstanceInfoUseCase(_services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedUpdateInstanceNameOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceNameRequestContract>)) as ILogger<IUpdateInstanceNameRequestContract>,
                    new ValidatedPipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateInstanceNameRequestContract>)) as IValidator<IUpdateInstanceNameRequestContract>,
                        new UpdateInstanceNameUseCase(_services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedUpdateInstancePhoneOnHttpContext( 
                new LoggedPipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstancePhoneRequestContract>)) as ILogger<IUpdateInstancePhoneRequestContract>,
                    new ValidatedPipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateInstancePhoneRequestContract>)) as IValidator<IUpdateInstancePhoneRequestContract>,
                        new UpdateInstancePhoneUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedUpdateInstanceRealNameOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRealNameRequestContract>)) as ILogger<IUpdateInstanceRealNameRequestContract>,
                    new ValidatedPipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateInstanceRealNameRequestContract>)) as IValidator<IUpdateInstanceRealNameRequestContract>,
                        new UpdateInstanceRealNameUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
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
            return await new ConvertedUpdateInstanceRegionOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRegionRequestContract>)) as ILogger<IUpdateInstanceRegionRequestContract>,
                    new ValidatedPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                        _services.GetService(typeof(IValidator<IUpdateInstanceRegionRequestContract>)) as IValidator<IUpdateInstanceRegionRequestContract>,
                        new UpdateInstanceRegionUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
    }
}