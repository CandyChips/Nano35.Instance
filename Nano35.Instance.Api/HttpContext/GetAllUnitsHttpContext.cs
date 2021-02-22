using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.HttpContext
{
    public class GetAllUnitsHttpContext : 
        IGetAllUnitsRequestContract
    {
        public Guid InstanceId { get; set; }
        public Guid UnitTypeId { get; set; }
    }
    
    public class GetUnitByIdHttpContext : IGetUnitByIdRequestContract
    {
        public Guid UnitId { get; set; }
    }
}