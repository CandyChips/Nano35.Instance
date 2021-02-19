using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.HttpContext
{
    public class GetAllWorkersHttpContext : 
        IGetAllWorkersRequestContract
    {
        public Guid InstanceId { get; set; }
        public Guid WorkersRoleId { get; set; }
    }
}