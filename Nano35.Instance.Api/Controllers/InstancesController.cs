using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.CreateInstance;
using Nano35.Instance.Api.Requests.GetAllCurrentInstances;
using Nano35.Instance.Api.Requests.GetAllInstances;
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
        public InstancesController(IServiceProvider services) => _services = services;

        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllInstancesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllInstancesErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> GetAllInstances([FromQuery] GetAllInstancesHttpQuery query) =>
            new ConvertedGetAllInstancesOnHttpContext(
                new LoggedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                    new GetAllInstancesUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(query);

        [AllowAnonymous]
        [HttpGet("Current")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> GetAllCurrentInstances() =>
            new ConvertedGetAllCurrentInstancesOnHttpContext(
                new LoggedPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                    new GetAllCurrentInstancesUseCase(
                        _services.GetService(typeof(IBus)) as IBus, 
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(new GetAllInstancesHttpQuery());

        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> GetInstanceById(Guid id) =>
            new ConvertedGetInstanceByIdOnHttpContext( 
                new LoggedPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetInstanceByIdRequestContract>)) as ILogger<IGetInstanceByIdRequestContract>,
                    new GetInstanceByIdUseCase(
                        _services.GetService(typeof(IBus)) as IBus )))
                .Ask(id);

        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateInstanceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateInstanceErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> CreateInstance([FromBody] CreateInstanceHttpBody body) =>
            new ConvertedCreateInstanceOnHttpContext(
                new LoggedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(
                    _services.GetService(typeof(ILogger<ICreateInstanceRequestContract>)) as ILogger<ICreateInstanceRequestContract>,
                    new CreateInstanceUseCase(
                        _services.GetService(typeof(IBus)) as IBus, 
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceEmailErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateInstanceEmail([FromBody] UpdateInstanceEmailHttpBody body) =>
            new ConvertedUpdateInstanceEmailOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceEmailRequestContract>)) as ILogger<IUpdateInstanceEmailRequestContract>,
                    new UpdateInstanceEmailUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Info")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceInfoErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateInstanceInfo([FromBody] UpdateInstanceInfoHttpBody body) =>
            new ConvertedUpdateInstanceInfoOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceInfoRequestContract>)) as ILogger<IUpdateInstanceInfoRequestContract>,
                    new UpdateInstanceInfoUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> UpdateInstanceName([FromBody] UpdateInstanceNameHttpBody body) =>
            new ConvertedUpdateInstanceNameOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceNameRequestContract>)) as ILogger<IUpdateInstanceNameRequestContract>,
                    new UpdateInstanceNameUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstancePhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstancePhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateInstancePhone([FromBody] UpdateInstancePhoneHttpBody body) =>
            new ConvertedUpdateInstancePhoneOnHttpContext( 
                new LoggedPipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstancePhoneRequestContract>)) as ILogger<IUpdateInstancePhoneRequestContract>,
                    new UpdateInstancePhoneUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/RealName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRealNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRealNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateInstanceRealName([FromBody] UpdateInstanceRealNameHttpBody body) =>
            new ConvertedUpdateInstanceRealNameOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRealNameRequestContract>)) as ILogger<IUpdateInstanceRealNameRequestContract>,
                    new UpdateInstanceRealNameUseCase(
                        _services.GetService(typeof(IBus)) as IBus)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Region")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRegionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRegionErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateInstanceRegion([FromBody] UpdateInstanceRegionHttpBody body) =>
            new ConvertedUpdateInstanceRegionOnHttpContext( 
                new LoggedPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRegionRequestContract>)) as ILogger<IUpdateInstanceRegionRequestContract>,
                    new UpdateInstanceRegionUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [AllowAnonymous]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteInstance(Guid id) => Ok(id);
    }
}