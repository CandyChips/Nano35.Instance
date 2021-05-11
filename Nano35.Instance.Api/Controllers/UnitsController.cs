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
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IServiceProvider  _services;
        public UnitsController(IServiceProvider services) => _services = services;

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllUnitsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitsErrorHttpResponse))] 
        public IActionResult GetAllUnits([FromQuery] GetAllUnitsHttpQuery query)
        {
            var result =
                new LoggedUseCasePipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>(
                        _services.GetService(typeof(ILogger<IGetAllUnitsRequestContract>)) as ILogger<IGetAllUnitsRequestContract>,
                        new GetAllUnitsUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetAllUnitsRequestContract()
                        {InstanceId = query.InstanceId, UnitTypeId = query.UnitTypeId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUnitByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetUnitByIdErrorHttpResponse))] 
        public IActionResult GetUnitById(Guid id)
        {
            var result =
                new LoggedUseCasePipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUnitByIdRequestContract>)) as ILogger<IGetUnitByIdRequestContract>,
                    new GetUnitByIdUseCase(
                        _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new GetUnitByIdRequestContract {UnitId = id})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateUnitErrorHttpResponse))] 
        public IActionResult CreateUnit([FromBody] CreateUnitHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<ICreateUnitRequestContract, ICreateUnitResultContract>(
                        _services.GetService(typeof(ILogger<ICreateUnitRequestContract>)) as ILogger<ICreateUnitRequestContract>,
                        new CreateUnitUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(new CreateUnitRequestContract()
                    {
                        Address = body.Address,
                        Id = body.Id,
                        InstanceId = body.InstanceId,
                        Name = body.Name,
                        Phone = body.Phone,
                        UnitTypeId = body.UnitTypeId,
                        WorkingFormat = body.WorkingFormat
                    })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsNameErrorHttpResponse))] 
        public IActionResult UpdateUnitsName([FromBody] UpdateUnitsNameHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateUnitsNameRequestContract>)) as ILogger<IUpdateUnitsNameRequestContract>,
                        new UpdateUnitsNameUseCase(
                            _services.GetService(typeof(IBus)) as IBus))
                .Ask(new UpdateUnitsNameRequestContract() { UnitId = body.UnitId, Name = body.Name })
                .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsPhoneErrorHttpResponse))] 
        public IActionResult UpdateUnitsPhone([FromBody] UpdateUnitsPhoneHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateUnitsPhoneRequestContract>)) as ILogger<IUpdateUnitsPhoneRequestContract>,
                        new UpdateUnitsPhoneUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateUnitsPhoneRequestContract() { Phone = body.Phone, UnitId = body.UnitId })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Address")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsAddressSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsAddressErrorHttpResponse))] 
        public IActionResult UpdateUnitsAddress([FromBody] UpdateUnitsAddressHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateUnitsAddressRequestContract>)) as ILogger<IUpdateUnitsAddressRequestContract>,
                        new UpdateUnitsAddressUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateUnitsAddressRequestContract() { Address = body.Address, UnitId = body.UnitId})
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/Type")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsTypeErrorHttpResponse))] 
        public IActionResult UpdateUnitsType([FromBody] UpdateUnitsTypeHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsTypeRequestContract>)) as ILogger<IUpdateUnitsTypeRequestContract>,
                    new UpdateUnitsTypeUseCase(
                        _services.GetService(typeof(IBus)) as IBus,
                        _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateUnitsTypeRequestContract() { TypeId = body.TypeId, UnitId = body.UnitId })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }

        [HttpPatch("{id}/WorkingFormat")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUnitsWorkingFormatSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsWorkingFormatErrorHttpResponse))] 
        public IActionResult UpdateUnitsWorkingFormat([FromBody] UpdateUnitsWorkingFormatHttpBody body)
        {
            var result =
                new LoggedUseCasePipeNode<IUpdateUnitsWorkingFormatRequestContract,
                        IUpdateUnitsWorkingFormatResultContract>(
                        _services.GetService(typeof(ILogger<IUpdateUnitsWorkingFormatRequestContract>)) as ILogger<IUpdateUnitsWorkingFormatRequestContract>,
                        new UpdateUnitsWorkingFormatUseCase(
                            _services.GetService(typeof(IBus)) as IBus,
                            _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))
                    .Ask(new UpdateUnitsWorkingFormatRequestContract() { UnitId = body.UnitId, WorkingFormat = body.WorkingFormat })
                    .Result;
            return result.IsSuccess() ? (IActionResult) Ok(result.Success) : BadRequest(result.Error);
        }
    }
}