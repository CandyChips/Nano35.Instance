using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.HttpContext;
using Nano35.Instance.Api.Requests.CreateUnit;
using Nano35.Instance.Api.Requests.GetAllUnits;
using Nano35.Instance.Api.Requests.GetAllUnitTypes;
using Nano35.Instance.Api.Requests.UpdateUnitsAddress;
using Nano35.Instance.Api.Requests.UpdateUnitsName;
using Nano35.Instance.Api.Requests.UpdateUnitsPhone;
using Nano35.Instance.Api.Requests.UpdateUnitsType;
using Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat;
using System.Text.Json.Serialization;
using Nano35.Instance.Api.Requests.GetUnitById;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        public class UpdateUnitsNameHttpContext : IUpdateUnitsNameRequestContract
        {
            public Guid UnitId { get; set; }
            public string Name { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
        }

        public class UpdateUnitsPhoneHttpContext : IUpdateUnitsPhoneRequestContract
        {
            public Guid UnitId { get; set; }
            public string Phone { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
        }

        public class UpdateUnitsAddressHttpContext : IUpdateUnitsAddressRequestContract
        {
            public Guid UnitId { get; set; }
            public string Address { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
        }

        public class UpdateUnitsTypeHttpContext : IUpdateUnitsTypeRequestContract
        {
            public Guid UnitId { get; set; }
            public Guid TypeId { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
        }

        public class UpdateUnitsWorkingFormatHttpContext : IUpdateUnitsWorkingFormatRequestContract
        {
            public Guid UnitId { get; set; }
            public string WorkingFormat { get; set; }
            [JsonIgnore]
            public Guid UpdaterId { get; set; }
        }

        
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
            [FromQuery] GetAllUnitsHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetAllUnitsRequest>)_services.GetService(typeof(ILogger<LoggedGetAllUnitsRequest>));
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllUnitsRequest(logger,
                        new ValidatedGetAllUnitsRequest(
                            new GetAllUnitsRequest(bus)))
                    .Ask(query);
            
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
            [FromQuery] GetUnitByIdHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedGetUnitByIdRequest>)_services.GetService(typeof(ILogger<LoggedGetUnitByIdRequest>));
            
            // Send request to pipeline
            var result =
                await new LoggedGetUnitByIdRequest(logger,
                        new ValidatedGetUnitByIdRequest(
                            new GetUnitByIdRequest(bus)))
                    .Ask(query);
            
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
            
            // Send request to pipeline
            var result =
                await new LoggedGetAllUnitTypesRequest(logger,
                    new GetAllUnitTypesRequest(bus))
                    .Ask(new GetAllGetAllUnitTypesHttpContext());
            
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
            [FromBody]CreateUnitHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedCreateUnitRequest>)_services.GetService(typeof(ILogger<LoggedCreateUnitRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedCreateUnitRequest(logger, 
                    new ValidatedCreateUnitRequest(
                        new CreateUnitRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                ICreateUnitSuccessResultContract => Ok(),
                ICreateUnitErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdateUnitsName")]
        public async Task<IActionResult> UpdateUnitsName(
            [FromBody] UpdateUnitsNameHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsNameRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsNameRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsNameRequest(logger, 
                    new ValidatedUpdateUnitsNameRequest(
                        new UpdateUnitsNameRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsNameSuccessResultContract => Ok(),
                IUpdateUnitsNameErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdateUnitsPhone")]
        public async Task<IActionResult> UpdateUnitsPhone(
            [FromBody] UpdateUnitsPhoneHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsPhoneRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsPhoneRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsPhoneRequest(logger, 
                    new ValidatedUpdateUnitsPhoneRequest(
                        new UpdateUnitsPhoneRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsPhoneSuccessResultContract => Ok(),
                IUpdateUnitsPhoneErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdateUnitsAddress")]
        public async Task<IActionResult> UpdateUnitsAddress(
            [FromBody] UpdateUnitsAddressHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsAddressRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsAddressRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsAddressRequest(logger, 
                    new ValidatedUpdateUnitsAddressRequest(
                        new UpdateUnitsAddressRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsAddressSuccessResultContract => Ok(),
                IUpdateUnitsAddressErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdateUnitsType")]
        public async Task<IActionResult> UpdateUnitsType(
            [FromBody] UpdateUnitsTypeHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsTypeRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsTypeRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsTypeRequest(logger, 
                    new ValidatedUpdateUnitsTypeRequest(
                        new UpdateUnitsTypeRequest(bus, auth)))
                    .Ask(body);

            // Check response of create unit request
            return result switch
            {
                IUpdateUnitsTypeSuccessResultContract => Ok(),
                IUpdateUnitsTypeErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPut]
        [Route("UpdateUnitsWorkingFormat")]
        public async Task<IActionResult> UpdateUnitsWorkingFormat(
            [FromBody] UpdateUnitsWorkingFormatHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<LoggedUpdateUnitsWorkingFormatRequest>)_services.GetService(typeof(ILogger<LoggedUpdateUnitsWorkingFormatRequest>));
            
            // Send request to pipeline
            var result = 
                await new LoggedUpdateUnitsWorkingFormatRequest(logger, 
                    new ValidatedUpdateUnitsWorkingFormatRequest(
                        new UpdateUnitsWorkingFormatRequest(bus, auth)))
                    .Ask(body);

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