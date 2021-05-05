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
        public IActionResult GetAllInstances([FromQuery] GetAllInstancesHttpQuery query)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as
                        ILogger<IGetAllInstancesRequestContract>,
                    new GetAllInstancesUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(new GetAllInstancesRequestContract() {InstanceTypeId = query.InstanceTypeId, RegionId = query.RegionId, UserId = query.UserId})
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpGet("Current")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetAllCurrentInstances()
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                        new GetAllCurrentInstancesUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new GetAllInstancesRequestContract())
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetInstanceByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetInstanceByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult GetInstanceById(Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(
                        _services.GetService(typeof(ILogger<IGetInstanceByIdRequestContract>)) as ILogger<IGetInstanceByIdRequestContract>,
                        new GetInstanceByIdUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetInstanceByIdRequestContract {InstanceId = id})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateInstanceSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateInstanceErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult CreateInstance([FromBody] CreateInstanceHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(
                        _services.GetService(typeof(ILogger<ICreateInstanceRequestContract>)) as ILogger<ICreateInstanceRequestContract>,
                        new CreateInstanceUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new CreateInstanceRequestContract()
                    {
                        Email = body.Email,
                        Info = body.Info,
                        Name = body.Name,
                        RealName = body.RealName,
                        NewId = body.NewId,
                        Phone = body.Phone,
                        RegionId = body.RegionId,
                        TypeId = body.TypeId
                    })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Email")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceEmailSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceEmailErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult UpdateInstanceEmail([FromBody] UpdateInstanceEmailHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceEmailRequestContract>)) as
                        ILogger<IUpdateInstanceEmailRequestContract>,
                    new UpdateInstanceEmailUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(new UpdateInstanceEmailRequestContract() { Email = body.Email, InstanceId = body.InstanceId })
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Info")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceInfoSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceInfoErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult UpdateInstanceInfo([FromBody] UpdateInstanceInfoHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceInfoRequestContract>)) as
                        ILogger<IUpdateInstanceInfoRequestContract>,
                    new UpdateInstanceInfoUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateInstanceInfoRequestContract() { InstanceId = body.InstanceId, Info = body.Info })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceNameErrorHttpResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateInstanceName([FromBody] UpdateInstanceNameHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceNameRequestContract>)) as
                        ILogger<IUpdateInstanceNameRequestContract>,
                    new UpdateInstanceNameUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateInstanceNameRequestContract() { InstanceId = body.InstanceId, Name = body.Name })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstancePhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstancePhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult UpdateInstancePhone([FromBody] UpdateInstancePhoneHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstancePhoneRequestContract>)) as
                        ILogger<IUpdateInstancePhoneRequestContract>,
                    new UpdateInstancePhoneUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateInstancePhoneRequestContract() { InstanceId = body.InstanceId, Phone = body.Phone })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/RealName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRealNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRealNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult UpdateInstanceRealName([FromBody] UpdateInstanceRealNameHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateInstanceRealNameRequestContract,
                    IUpdateInstanceRealNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRealNameRequestContract>)) as
                        ILogger<IUpdateInstanceRealNameRequestContract>,
                    new UpdateInstanceRealNameUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new UpdateInstanceRealNameRequestContract() { InstanceId = body.InstanceId, RealName = body.RealName })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [Authorize]
        [HttpPatch("{id}/Region")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateInstanceRegionSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateInstanceRegionErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult UpdateInstanceRegion([FromBody] UpdateInstanceRegionHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRegionRequestContract>)) as
                        ILogger<IUpdateInstanceRegionRequestContract>,
                    new UpdateInstanceRegionUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateInstanceRegionRequestContract() { InstanceId = body.InstanceId, RegionId = body.RegionId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult DeleteInstance(Guid id) => Ok(id);
    }
}