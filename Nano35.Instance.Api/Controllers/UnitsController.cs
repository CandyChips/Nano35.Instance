using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests.CreateUnit;
using Nano35.Instance.Api.Requests.GetAllUnits;
using Nano35.Instance.Api.Requests.GetAllUnitTypes;
using Nano35.Instance.Api.Requests.UpdateUnitsAddress;
using Nano35.Instance.Api.Requests.UpdateUnitsName;
using Nano35.Instance.Api.Requests.UpdateUnitsPhone;
using Nano35.Instance.Api.Requests.UpdateUnitsType;
using Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Requests.GetUnitById;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        public UnitsController(
            IServiceProvider services)
        {
            _services = services;
        }
        
        [HttpGet]
        [Route("GetAllUnits")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitsErrorHttpResponse))] 
        public async Task<IActionResult> GetAllUnits(
            [FromQuery] GetAllUnitsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitsRequest>));

            var request = new GetAllUnitsRequestContract()
            {
                InstanceId = query.InstanceId,
                UnitTypeId = query.UnitTypeId
            };
            
            var result =
                await new LoggedGetAllUnitsRequest(logger,
                        new ValidatedGetAllUnitsRequest(
                            new GetAllUnitsRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetAllUnitsSuccessResultContract success => Ok(success),
                IGetAllUnitsErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
        
        [HttpGet]
        [Route("GetUnitById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetUnitByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetUnitByIdErrorHttpResponse))] 
        public async Task<IActionResult> GetUnitById(
            [FromQuery] GetUnitByIdHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetUnitByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetUnitByIdRequest>));

            var request = new GetUnitByIdRequestContract()
            {
                UnitId = query.UnitId
            };
            
            var result =
                await new LoggedGetUnitByIdRequest(logger,
                        new ValidatedGetUnitByIdRequest(
                            new GetUnitByIdRequest(bus)))
                    .Ask(request);
            
            return result switch
            {
                IGetUnitByIdSuccessResultContract success => Ok(success),
                IGetUnitByIdErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllUnitTypes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitTypesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllUnitTypes()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitTypesRequest>));

            var request = new GetAllUnitTypesRequestContract();
            
            var result =
                await new LoggedGetAllUnitTypesRequest(logger,
                    new GetAllUnitTypesRequest(bus))
                    .Ask(request);
            
            return result switch
            {
                IGetAllUnitTypesSuccessResultContract success => Ok(success),
                IGetAllUnitTypesErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateUnitErrorHttpResponse))] 
        public async Task<IActionResult> CreateUnit(
            [FromBody] CreateUnitHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateUnitRequest>)_services.GetService(typeof(ILogger<LoggedCreateUnitRequest>));

            var request = new CreateUnitRequestContract()
            {
                Address = body.Address,
                CreatorId = body.CreatorId,
                Id = body.Id,
                InstanceId = body.InstanceId,
                Name = body.Name,
                Phone = body.Phone,
                UnitTypeId = body.UnitTypeId,
                WorkingFormat = body.WorkingFormat
            };
            
            var result = 
                await new LoggedCreateUnitRequest(logger, 
                    new ValidatedCreateUnitRequest(
                        new CreateUnitRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                ICreateUnitSuccessResultContract success => Ok(success),
                ICreateUnitErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsNameErrorHttpResponse))] 
        public async Task<IActionResult> UpdateUnitsName(
            [FromBody] UpdateUnitsNameHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsNameRequest>));

            var request = new UpdateUnitsNameRequestContract()
            {
                Name = body.Name,
                UnitId = body.UnitId
            };
            
            var result = 
                await new LoggedUpdateUnitsNameRequest(logger, 
                    new ValidatedUpdateUnitsNameRequest(
                        new UpdateUnitsNameRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateUnitsNameSuccessResultContract success => Ok(success),
                IUpdateUnitsNameErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsPhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsPhoneErrorHttpResponse))] 
        public async Task<IActionResult> UpdateUnitsPhone(
            [FromBody] UpdateUnitsPhoneHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsPhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsPhoneRequest>));

            var request = new UpdateUnitsPhoneRequestContract()
            {
                Phone = body.Phone,
                UnitId = body.UnitId
            };
            
            var result = 
                await new LoggedUpdateUnitsPhoneRequest(logger, 
                    new ValidatedUpdateUnitsPhoneRequest(
                        new UpdateUnitsPhoneRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateUnitsPhoneSuccessResultContract success => Ok(success),
                IUpdateUnitsPhoneErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsAddress")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsAddressSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsAddressErrorHttpResponse))] 
        public async Task<IActionResult> UpdateUnitsAddress(
            [FromBody] UpdateUnitsAddressHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsAddressRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsAddressRequest>));

            var request = new UpdateUnitsAddressRequestContract()
            {
                Address = body.Address,
                UnitId = body.UnitId
            };
            
            var result = 
                await new LoggedUpdateUnitsAddressRequest(logger, 
                    new ValidatedUpdateUnitsAddressRequest(
                        new UpdateUnitsAddressRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateUnitsAddressSuccessResultContract success => Ok(success),
                IUpdateUnitsAddressErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsTypeErrorHttpResponse))] 
        public async Task<IActionResult> UpdateUnitsType(
            [FromBody] UpdateUnitsTypeHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsTypeRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsTypeRequest>));

            var request = new UpdateUnitsTypeRequestContract()
            {
                TypeId = body.TypeId,
                UnitId = body.UnitId
            };
            
            var result = 
                await new LoggedUpdateUnitsTypeRequest(logger, 
                    new ValidatedUpdateUnitsTypeRequest(
                        new UpdateUnitsTypeRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateUnitsTypeSuccessResultContract success => Ok(success),
                IUpdateUnitsTypeErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsWorkingFormat")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsWorkingFormatSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsWorkingFormatErrorHttpResponse))] 
        public async Task<IActionResult> UpdateUnitsWorkingFormat(
            [FromBody] UpdateUnitsWorkingFormatHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsWorkingFormatRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsWorkingFormatRequest>));

            var request = new UpdateUnitsWorkingFormatRequestContract()
            {
                WorkingFormat = body.WorkingFormat,
                UnitId = body.UnitId
            };
            
            var result = 
                await new LoggedUpdateUnitsWorkingFormatRequest(logger, 
                    new ValidatedUpdateUnitsWorkingFormatRequest(
                        new UpdateUnitsWorkingFormatRequest(bus, auth)))
                    .Ask(request);

            return result switch
            {
                IUpdateUnitsWorkingFormatSuccessResultContract success => Ok(success),
                IUpdateUnitsWorkingFormatErrorResultContract error => BadRequest(error),
                _ => BadRequest()
            };
        }
    }
}