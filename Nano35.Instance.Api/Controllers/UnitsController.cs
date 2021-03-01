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
using Nano35.HttpContext.instance;
using Nano35.Instance.Api.Requests.GetUnitById;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        /// <summary>
        /// Controller provide IServiceProvider from asp net core DI
        /// for registration services to pipe nodes
        /// </summary>
        public UnitsController(
            IServiceProvider services)
        {
            _services = services;
        }
        
        /// <summary>
        /// Controllers accept a HttpContext type
        /// All controllers actions works by pipelines
        /// Implementation works with 3 steps
        /// 1. Setup DI services from IServiceProvider;
        /// 2. Building pipeline like a onion
        ///     '(PipeNode1(PipeNode2(PipeNode3(...).Ask()).Ask()).Ask()).Ask()';
        /// 3. Response pattern match of pipeline response;
        /// </summary>
        [HttpGet]
        [Route("GetAllUnits")]
        public async Task<IActionResult> GetAllUnits(
            [FromQuery] GetAllUnitsHttpContext.GetAllUnitsQuery query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitsRequest>));

            var request = new GetAllUnitsRequestContract()
            {
                InstanceId = query.InstanceId,
                UnitTypeId = query.UnitTypeId
            };
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllUnitsRequest(logger,
                        new ValidatedGetAllUnitsRequest(
                            new GetAllUnitsRequest(bus)))
                    .Ask(request);
            
            // Check response of get all units request
            return result switch
            {
                IGetAllUnitsSuccessResultContract success => Ok(success.Data),
                IGetAllUnitsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpGet]
        [Route("GetUnitById")]
        public async Task<IActionResult> GetUnitById(
            [FromQuery] GetUnitByIdHttpContext.GetUnitByIdQuery query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetUnitByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetUnitByIdRequest>));

            var request = new GetUnitByIdRequestContract()
            {
                UnitId = query.UnitId
            };
            
            // Send request to pipeline
            var result =
                await new LoggedGetUnitByIdRequest(logger,
                        new ValidatedGetUnitByIdRequest(
                            new GetUnitByIdRequest(bus)))
                    .Ask(request);
            
            // Check response of get all units request
            return result switch
            {
                IGetUnitByIdSuccessResultContract success => Ok(success.Data),
                IGetUnitByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllUnitTypes")]
        public async Task<IActionResult> GetAllUnitTypes()
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitTypesRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitTypesRequest>));

            var request = new GetAllUnitTypesRequestContract();
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllUnitTypesRequest(logger,
                    new GetAllUnitTypesRequest(bus))
                    .Ask(request);
            
            // Check response of get all unit types request
            return result switch
            {
                IGetAllUnitTypesSuccessResultContract success => Ok(success.Data),
                IGetAllUnitTypesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateUnit")]
        public async Task<IActionResult> CreateUnit(
            [FromBody]CreateUnitHttpContext.CreateUnitBody body)
        {
            // Setup configuration of pipeline
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
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateUnitRequest(logger, 
                    new ValidatedCreateUnitRequest(
                        new CreateUnitRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                ICreateUnitSuccessResultContract => Ok(),
                ICreateUnitErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsName")]
        public async Task<IActionResult> UpdateUnitsName(
            [FromBody] UpdateUnitsNameHttpContext.UpdateUnitsNameBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsNameRequest>));

            var request = new UpdateUnitsNameRequestContract()
            {
                Name = body.Name,
                UnitId = body.UnitId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsNameRequest(logger, 
                    new ValidatedUpdateUnitsNameRequest(
                        new UpdateUnitsNameRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsNameSuccessResultContract => Ok(),
                IUpdateUnitsNameErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsPhone")]
        public async Task<IActionResult> UpdateUnitsPhone(
            [FromBody] UpdateUnitsPhoneHttpContext.UpdateUnitsPhoneBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsPhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsPhoneRequest>));

            var request = new UpdateUnitsPhoneRequestContract()
            {
                Phone = body.Phone,
                UnitId = body.UnitId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsPhoneRequest(logger, 
                    new ValidatedUpdateUnitsPhoneRequest(
                        new UpdateUnitsPhoneRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsPhoneSuccessResultContract => Ok(),
                IUpdateUnitsPhoneErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsAddress")]
        public async Task<IActionResult> UpdateUnitsAddress(
            [FromBody] UpdateUnitsAddressHttpContext.UpdateUnitsAddressBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsAddressRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsAddressRequest>));

            var request = new UpdateUnitsAddressRequestContract()
            {
                Address = body.Address,
                UnitId = body.UnitId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsAddressRequest(logger, 
                    new ValidatedUpdateUnitsAddressRequest(
                        new UpdateUnitsAddressRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsAddressSuccessResultContract => Ok(),
                IUpdateUnitsAddressErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsType")]
        public async Task<IActionResult> UpdateUnitsType(
            [FromBody] UpdateUnitsTypeHttpContext.UpdateUnitsTypeBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsTypeRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsTypeRequest>));

            var request = new UpdateUnitsTypeRequestContract()
            {
                TypeId = body.TypeId,
                UnitId = body.UnitId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsTypeRequest(logger, 
                    new ValidatedUpdateUnitsTypeRequest(
                        new UpdateUnitsTypeRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsTypeSuccessResultContract => Ok(),
                IUpdateUnitsTypeErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPatch]
        [Route("UpdateUnitsWorkingFormat")]
        public async Task<IActionResult> UpdateUnitsWorkingFormat(
            [FromBody] UpdateUnitsWorkingFormatHttpContext.UpdateUnitsWorkingFormatBody body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsWorkingFormatRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsWorkingFormatRequest>));

            var request = new UpdateUnitsWorkingFormatRequestContract()
            {
                WorkingFormat = body.WorkingFormat,
                UnitId = body.UnitId
            };
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsWorkingFormatRequest(logger, 
                    new ValidatedUpdateUnitsWorkingFormatRequest(
                        new UpdateUnitsWorkingFormatRequest(bus, auth)))
                    .Ask(request);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsWorkingFormatSuccessResultContract => Ok(),
                IUpdateUnitsWorkingFormatErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}