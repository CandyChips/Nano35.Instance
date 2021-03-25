using System;
using System.Threading.Tasks;
using FluentValidation;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Requests.GetUnitById;

namespace Nano35.Instance.Api.Controllers
{
    [Authorize]
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
        
        [Authorize]
        [HttpGet]
        [Route("GetAllUnits")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitsErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllUnits(
            [FromQuery] GetAllUnitsHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitsRequest>));
            var validator = (IValidator<IGetAllUnitsRequestContract>) _services.GetService(typeof(IValidator<IGetAllUnitsRequestContract>));

            return await 
                new ConvertedGetAllUnitsOnHttpContext(
                    new LoggedGetAllUnitsRequest(logger,
                        new ValidatedGetAllUnitsRequest(validator,
                            new GetAllUnitsUseCase(bus)))).Ask(query);
        }
        
        [Authorize]
        [HttpGet]
        [Route("GetUnitById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetUnitByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetUnitByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetUnitById(
            [FromQuery] GetUnitByIdHttpQuery query)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetUnitByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetUnitByIdRequest>));
            var validator = (IValidator<IGetUnitByIdRequestContract>) _services.GetService(typeof(IValidator<IGetUnitByIdRequestContract>));

           
            return await 
                new ConvertedGetUnitByIdOnHttpContext(
                    new LoggedGetUnitByIdRequest(logger,
                        new ValidatedGetUnitByIdRequest(validator,
                            new GetUnitByIdUseCase(bus)))).Ask(query);
        }
    
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllUnitTypes")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitTypesSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitTypesErrorHttpResponse))] 
        public async Task<IActionResult> GetAllUnitTypes()
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitTypesRequest>));
            
            return await 
                new ConvertedGetAllUnitTypesOnHttpContext(
                    new LoggedGetAllUnitTypesRequest(logger,
                        new GetAllUnitTypesUseCase(bus)))
                    .Ask(new GetAllUnitTypesHttpQuery());
        }

        [Authorize]
        [HttpPost]
        [Route("CreateUnit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateUnitErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateUnit(
            [FromBody] CreateUnitHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateUnitRequest>)_services.GetService(typeof(ILogger<LoggedCreateUnitRequest>));

            return await 
                new ConvertedCreateUnitOnHttpContext(
                    new LoggedCreateUnitRequest(logger,
                        new ValidatedCreateUnitRequest(
                            new CreateUnitUseCase(bus,auth)))).Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUnitsName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsName(
            [FromBody] UpdateUnitsNameHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsNameRequest>));

            return await 
                new ConvertedUpdateUnitsNameOnHttpContext(
                    new LoggedUpdateUnitsNameRequest(logger,
                        new ValidatedUpdateUnitsNameRequest(
                            new UpdateUnitsNameUseCase(bus,auth)))).Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUnitsPhone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsPhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsPhone(
            [FromBody] UpdateUnitsPhoneHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsPhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsPhoneRequest>));

            return await 
                new ConvertedUpdateUnitsPhoneOnHttpContext(
                    new LoggedUpdateUnitsPhoneRequest(logger,
                        new ValidatedUpdateUnitsPhoneRequest(
                            new UpdateUnitsPhoneUseCase(bus,auth)))).Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUnitsAddress")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsAddressSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsAddressErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsAddress(
            [FromBody] UpdateUnitsAddressHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsAddressRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsAddressRequest>));

            return await 
                new ConvertedUpdateUnitsAddressOnHttpContext(
                    new LoggedUpdateUnitsAddressRequest(logger,
                        new ValidatedUpdateUnitsAddressRequest(
                            new UpdateUnitsAddressUseCase(bus,auth)))).Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUnitsType")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsTypeErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsType(
            [FromBody] UpdateUnitsTypeHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsTypeRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsTypeRequest>));

            return await 
                new ConvertedUpdateUnitsTypeOnHttpContext(
                    new LoggedUpdateUnitsTypeRequest(logger,
                        new ValidatedUpdateUnitsTypeRequest(
                            new UpdateUnitsTypeUseCase(bus,auth)))).Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("UpdateUnitsWorkingFormat")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsWorkingFormatSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsWorkingFormatErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsWorkingFormat(
            [FromBody] UpdateUnitsWorkingFormatHttpBody body)
        {
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsWorkingFormatRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsWorkingFormatRequest>));

            return await 
                new ConvertedUpdateUnitsWorkingFormatOnHttpContext(
                    new LoggedUpdateUnitsWorkingFormatRequest(logger,
                        new ValidatedUpdateUnitsWorkingFormatRequest(
                            new UpdateUnitsWorkingFormatUseCase(bus,auth)))).Ask(body);
        }
    }
}