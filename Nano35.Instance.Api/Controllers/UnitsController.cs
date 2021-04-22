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

        public UnitsController(IServiceProvider services) { _services = services; }
        
        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetAllUnitsSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetAllUnitsErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetAllUnits(
            [FromQuery] GetAllUnitsHttpQuery query)
        {
            return await 
                new ValidatedPipeNode<GetAllUnitsHttpQuery, IActionResult>(
                    _services.GetService(typeof(IValidator<GetAllUnitsHttpQuery>)) as IValidator<GetAllUnitsHttpQuery>,
                    new ConvertedGetAllUnitsOnHttpContext(
                        new LoggedPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>(
                            _services.GetService(typeof(ILogger<IGetAllUnitsRequestContract>)) as ILogger<IGetAllUnitsRequestContract>,
                            new GetAllUnitsUseCase(
                                    _services.GetService(typeof(IBus)) as IBus))))
                .Ask(query);
        }
        
        [Authorize]
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetUnitByIdSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GetUnitByIdErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> GetUnitById(Guid id)
        {
            return await 
                new ValidatedPipeNode<Guid, IActionResult>(
                    _services.GetService(typeof(IValidator<Guid>)) as IValidator<Guid>,
                    new ConvertedGetUnitByIdOnHttpContext(
                        new LoggedPipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>(
                            _services.GetService(typeof(ILogger<IGetUnitByIdRequestContract>)) as ILogger<IGetUnitByIdRequestContract>,
                            new GetUnitByIdUseCase(
                                    _services.GetService(typeof(IBus)) as IBus))))
                .Ask(id);
        }
    

        [Authorize]
        [HttpPost]
        [Route("Unit")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateUnitSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CreateUnitErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> CreateUnit(
            [FromBody] CreateUnitHttpBody body)
        {
            return await 
                new ValidatedPipeNode<CreateUnitHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<CreateUnitHttpBody>)) as IValidator<CreateUnitHttpBody>,
                    new ConvertedCreateUnitOnHttpContext(
                        new LoggedPipeNode<ICreateUnitRequestContract, ICreateUnitResultContract>(
                            _services.GetService(typeof(ILogger<ICreateUnitRequestContract>)) as ILogger<ICreateUnitRequestContract>,
                            new CreateUnitUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Name")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsNameSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsNameErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsName(
            [FromBody] UpdateUnitsNameHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateUnitsNameHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateUnitsNameHttpBody>)) as IValidator<UpdateUnitsNameHttpBody>,
                    new ConvertedUpdateUnitsNameOnHttpContext(
                        new LoggedPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateUnitsNameRequestContract>)) as ILogger<IUpdateUnitsNameRequestContract>,
                            new UpdateUnitsNameUseCase(
                                    _services.GetService(typeof(IBus)) as IBus, 
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Phone")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsPhoneSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsPhoneErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsPhone(
            [FromBody] UpdateUnitsPhoneHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateUnitsPhoneHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateUnitsPhoneHttpBody>)) as IValidator<UpdateUnitsPhoneHttpBody>,
                    new ConvertedUpdateUnitsPhoneOnHttpContext(
                        new LoggedPipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateUnitsPhoneRequestContract>)) as ILogger<IUpdateUnitsPhoneRequestContract>,
                            new UpdateUnitsPhoneUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Address")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsAddressSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsAddressErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsAddress(
            [FromBody] UpdateUnitsAddressHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateUnitsAddressHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateUnitsAddressHttpBody>)) as IValidator<UpdateUnitsAddressHttpBody>,
                    new ConvertedUpdateUnitsAddressOnHttpContext(
                        new LoggedPipeNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateUnitsAddressRequestContract>)) as ILogger<IUpdateUnitsAddressRequestContract>,
                            new UpdateUnitsAddressUseCase(
                                    _services.GetService(typeof(IBus)) as IBus,
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }

        [Authorize]
        [HttpPatch]
        [Route("Type")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsTypeSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsTypeErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsType(
            [FromBody] UpdateUnitsTypeHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateUnitsTypeHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateUnitsTypeHttpBody>)) as IValidator<UpdateUnitsTypeHttpBody>,
                    new ConvertedUpdateUnitsTypeOnHttpContext(
                        new LoggedPipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateUnitsTypeRequestContract>)) as ILogger<IUpdateUnitsTypeRequestContract>,
                            new UpdateUnitsTypeUseCase(
                                    _services.GetService(typeof(IBus)) as IBus, 
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
        
        [Authorize]
        [HttpPatch]
        [Route("WorkingFormat")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UpdateUnitsWorkingFormatSuccessHttpResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(UpdateUnitsWorkingFormatErrorHttpResponse))] 
        [ProducesResponseType(StatusCodes.Status401Unauthorized)] 
        public async Task<IActionResult> UpdateUnitsWorkingFormat(
            [FromBody] UpdateUnitsWorkingFormatHttpBody body)
        {
            return await 
                new ValidatedPipeNode<UpdateUnitsWorkingFormatHttpBody, IActionResult>(
                    _services.GetService(typeof(IValidator<UpdateUnitsWorkingFormatHttpBody>)) as IValidator<UpdateUnitsWorkingFormatHttpBody>,
                    new ConvertedUpdateUnitsWorkingFormatOnHttpContext(
                        new LoggedPipeNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>(
                            _services.GetService(typeof(ILogger<IUpdateUnitsWorkingFormatRequestContract>)) as ILogger<IUpdateUnitsWorkingFormatRequestContract>,
                            new UpdateUnitsWorkingFormatUseCase(
                                    _services.GetService(typeof(IBus)) as IBus, 
                                    _services.GetService(typeof(ICustomAuthStateProvider)) as ICustomAuthStateProvider))))
                .Ask(body);
        }
    }
}