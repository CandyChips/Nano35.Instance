using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
    public class GetAllUnitsHttpContext : 
        IGetAllUnitsRequestContract
    {
        public Guid InstanceId { get; set; }
        public Guid UnitTypeId { get; set; }
    }
}