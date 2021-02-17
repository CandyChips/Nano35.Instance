using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
    public class GetAllWorkerRolesHttpContext : 
        IGetAllWorkerRolesRequestContract
    {
        public Guid InstanceId { get; set; }
    }
}