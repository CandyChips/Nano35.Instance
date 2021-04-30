using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests.CreateUnit;
using Nano35.Instance.Api.Requests.GetAllUnits;
using Nano35.Instance.Api.Requests.UpdateUnitsAddress;
using Nano35.Instance.Api.Requests.UpdateUnitsName;
using Nano35.Instance.Api.Requests.UpdateUnitsPhone;
using Nano35.Instance.Api.Requests.UpdateUnitsType;
using Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.GetUnitById;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IServiceProvider  _services;
        public UnitsController(IServiceProvider services) => _services = services;

        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllUnitsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitsErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult GetAllUnits([FromQuery] GetAllUnitsHttpQuery query) =>
            new LoggedPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllUnitsRequestContract>)) as ILogger<IGetAllUnitsRequestContract>, 
                new GetAllUnitsUseCase(
                    _services.GetService(typeof(IBus)) as IBus))
            .Ask(new GetAllUnitsRequestContract() { InstanceId = query.InstanceId, UnitTypeId = query.UnitTypeId })
            .Result switch
            {
                IGetAllUnitsSuccessResultContract success => new OkObjectResult(success),
                IGetAllUnitsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };

        [Authorize]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUnitByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetUnitByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult GetUnitById(Guid id) =>
                new LoggedPipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUnitByIdRequestContract>)) as ILogger<IGetUnitByIdRequestContract>,
                    new GetUnitByIdUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(new GetUnitByIdRequestContract { UnitId = id })
                .Result switch
                {
                    IGetUnitByIdSuccessResultContract success => new OkObjectResult(success),
                    IGetUnitByIdErrorResultContract error => new BadRequestObjectResult(error),
                    _ => new BadRequestObjectResult("")
                };


        [Authorize]
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateUnitErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public IActionResult CreateUnit([FromBody] CreateUnitHttpBody body) =>
            new LoggedPipeNode<ICreateUnitRequestContract, ICreateUnitResultContract>(
                _services.GetService(typeof(ILogger<ICreateUnitRequestContract>)) as ILogger<ICreateUnitRequestContract>,
                new CreateUnitUseCase(
                    _services.GetService(typeof(IBus)) as IBus,
                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
            .Ask(new CreateUnitRequestContract()
                {Address = body.Address,
                 Id = body.Id,
                 InstanceId = body.InstanceId,
                 Name = body.Name,
                 Phone = body.Phone,
                 UnitTypeId = body.UnitTypeId,
                 WorkingFormat = body.WorkingFormat})
            .Result switch
            {
                ICreateUnitSuccessResultContract success => new OkObjectResult(success),
                ICreateUnitErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };

        [Authorize]
        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateUnitsName([FromBody] UpdateUnitsNameHttpBody body) =>
            new ConvertedUpdateUnitsNameOnHttpContext(
                new LoggedPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsNameRequestContract>)) as ILogger<IUpdateUnitsNameRequestContract>,
                    new UpdateUnitsNameUseCase(
                        _services.GetService(typeof(IBus)) as IBus, 
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsPhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateUnitsPhone([FromBody] UpdateUnitsPhoneHttpBody body) =>
            new ConvertedUpdateUnitsPhoneOnHttpContext(
                new LoggedPipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsPhoneRequestContract>)) as ILogger<IUpdateUnitsPhoneRequestContract>,
                    new UpdateUnitsPhoneUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Address")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsAddressSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsAddressErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateUnitsAddress([FromBody] UpdateUnitsAddressHttpBody body) =>
            new ConvertedUpdateUnitsAddressOnHttpContext(
                new LoggedPipeNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsAddressRequestContract>)) as ILogger<IUpdateUnitsAddressRequestContract>,
                    new UpdateUnitsAddressUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/Type")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsTypeErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateUnitsType([FromBody] UpdateUnitsTypeHttpBody body) =>
            new ConvertedUpdateUnitsTypeOnHttpContext(
                new LoggedPipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsTypeRequestContract>)) as ILogger<IUpdateUnitsTypeRequestContract>,
                    new UpdateUnitsTypeUseCase(
                        _services.GetService(typeof(IBus)) as IBus, 
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);

        [Authorize]
        [HttpPatch("{id}/WorkingFormat")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsWorkingFormatSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsWorkingFormatErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public Task<IActionResult> UpdateUnitsWorkingFormat([FromBody] UpdateUnitsWorkingFormatHttpBody body) =>
            new ConvertedUpdateUnitsWorkingFormatOnHttpContext(
                new LoggedPipeNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsWorkingFormatRequestContract>)) as ILogger<IUpdateUnitsWorkingFormatRequestContract>,
                    new UpdateUnitsWorkingFormatUseCase(
                        _services.GetService(typeof(IBus)) as IBus, 
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider)))
                .Ask(body);
    }
}