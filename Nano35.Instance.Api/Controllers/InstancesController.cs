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
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllInstancesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllInstancesErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllInstances(
            [FromQuery] GetAllInstancesHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllInstancesHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllInstancesHttpQuery>)) as IValidator<GetAllInstancesHttpQuery>,
                    new ConvertedGetAllInstancesOnHttpContext(
                    new LoggedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                        new GetAllInstancesUseCase(
                            _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Current")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllCurrentInstances()
        {
            return await
                new ValidatedPipeNode<GetAllInstancesHttpQuery, IActionResult>(
                        _services.GetService(typeof(IValidator<GetAllInstancesHttpQuery>)) as IValidator<GetAllInstancesHttpQuery>, 
                        new ConvertedGetAllCurrentInstancesOnHttpContext(
                            new LoggedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                                _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                                new GetAllCurrentInstancesUseCase(
                                    _services.GetService(typeof(IBus)) as IBus, 
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(new GetAllInstancesHttpQuery());
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetInstanceById(Guid id)
        {
            return await
                new ValidatedPipeNode<Guid, IActionResult>(
                        _services.GetService(typeof(IValidator<Guid>)) as IValidator<Guid>, 
                        new ConvertedGetInstanceByIdOnHttpContext( 
                            new LoggedPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(
                                _services.GetService(typeof(ILogger<IGetInstanceByIdRequestContract>)) as ILogger<IGetInstanceByIdRequestContract>,
                                new GetInstanceByIdUseCase(
                                    _services.GetService(typeof(IBus)) as IBus ))))
                .Ask(id);
        }

        [Authorize]
        [HttpPost]
        [Route("Instance")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateInstanceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateInstanceErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateInstance(
            [FromBody] CreateInstanceHttpBody body)
        {
            return await
                new ValidatedPipeNode<CreateInstanceHttpBody, IActionResult>(
                        _services.GetService(typeof(IValidator<CreateInstanceHttpBody>)) as IValidator<CreateInstanceHttpBody>,
                    new ConvertedCreateInstanceOnHttpContext(
                        new LoggedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(
                            _services.GetService(typeof(ILogger<ICreateInstanceRequestContract>)) as ILogger<ICreateInstanceRequestContract>,
                            new CreateInstanceUseCase(
                                _services.GetService(typeof(IBus)) as IBus, 
                                _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPost]
        [Route("CashOutput")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCashOutputSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCashOutputErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateCashOutput(
            [FromBody] CreateCashOutputHttpBody body)
        {
            return await
                new ValidatedPipeNode<CreateCashOutputHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateCashOutputHttpBody>)) as IValidator<CreateCashOutputHttpBody>, 
                    new ConvertedCreateCashOutputOnHttpContext( 
                        new LoggedPipeNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract>(
                            _services.GetService(typeof(ILogger<ICreateCashOutputRequestContract>)) as ILogger<ICreateCashOutputRequestContract>,
                            new CreateCashOutputUseCase(
                                _services.GetService(typeof(IBus)) as IBus, 
                                _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPost]
        [Route("CashInput")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCashInputSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateCashInputErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateCashInput(
            [FromBody] CreateCashInputHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateCashInputHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateCashInputHttpBody>)) as IValidator<CreateCashInputHttpBody>,
                    new ConvertedCreateCashInputOnHttpContext(
                        new LoggedPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract>(
                            _services.GetService(typeof(ILogger<ICreateCashInputRequestContract>)) as ILogger<ICreateCashInputRequestContract>,
                            new CreateCashInputUseCase(
                                _services.GetService(typeof(IBus)) as IBus, 
                                _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
        
        [Authorize]
        [HttpPatch]
        [Route("Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceEmailErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceEmail(
            [FromBody] UpdateInstanceEmailHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateInstanceEmailHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateInstanceEmailHttpBody>)) as IValidator<UpdateInstanceEmailHttpBody>,
                    new ConvertedUpdateInstanceEmailOnHttpContext( 
                        new LoggedPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateInstanceEmailRequestContract>)) as ILogger<IUpdateInstanceEmailRequestContract>,
                            new UpdateInstanceEmailUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }
        
        [Authorize]
        [HttpPatch]
        [Route("Info")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceInfoErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceInfo(
            [FromBody] UpdateInstanceInfoHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateInstanceInfoHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateInstanceInfoHttpBody>)) as IValidator<UpdateInstanceInfoHttpBody>,
                    new ConvertedUpdateInstanceInfoOnHttpContext( 
                        new LoggedPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateInstanceInfoRequestContract>)) as ILogger<IUpdateInstanceInfoRequestContract>,
                            new UpdateInstanceInfoUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateInstanceName(
            [FromBody] UpdateInstanceNameHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateInstanceNameHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateInstanceNameHttpBody>)) as IValidator<UpdateInstanceNameHttpBody>,
                    new ConvertedUpdateInstanceNameOnHttpContext( 
                        new LoggedPipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateInstanceNameRequestContract>)) as ILogger<IUpdateInstanceNameRequestContract>,
                            new UpdateInstanceNameUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstancePhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstancePhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstancePhone(
            [FromBody] UpdateInstancePhoneHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateInstancePhoneHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateInstancePhoneHttpBody>)) as IValidator<UpdateInstancePhoneHttpBody>,
                    new ConvertedUpdateInstancePhoneOnHttpContext( 
                        new LoggedPipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateInstancePhoneRequestContract>)) as ILogger<IUpdateInstancePhoneRequestContract>,
                            new UpdateInstancePhoneUseCase(
                                _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }
        
        [Authorize]
        [HttpPatch]
        [Route("RealName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRealNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRealNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceRealName(
            [FromBody] UpdateInstanceRealNameHttpBody body)
        {
            return await new ValidatedPipeNode<UpdateInstanceRealNameHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateInstanceRealNameHttpBody>)) as IValidator<UpdateInstanceRealNameHttpBody>,
                    new ConvertedUpdateInstanceRealNameOnHttpContext( 
                        new LoggedPipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateInstanceRealNameRequestContract>)) as ILogger<IUpdateInstanceRealNameRequestContract>,
                            new UpdateInstanceRealNameUseCase(
                                    _services.GetService(typeof(IBus)) as IBus))))
                .Ask(body);
        }
        
        [Authorize]
        [HttpPatch]
        [Route("Region")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRegionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRegionErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateInstanceRegion(
            [FromBody] UpdateInstanceRegionHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateInstanceRegionHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateInstanceRegionHttpBody>)) as IValidator<UpdateInstanceRegionHttpBody>,
                    new ConvertedUpdateInstanceRegionOnHttpContext( 
                        new LoggedPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateInstanceRegionRequestContract>)) as ILogger<IUpdateInstanceRegionRequestContract>,
                             new UpdateInstanceRegionUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
        
        [AllowAnonymous]
        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteInstance(Guid id)
        {
            return Ok(id);
        }
    }
}