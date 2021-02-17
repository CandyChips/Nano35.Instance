using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.HttpContext
{
    public class GetInstanceByIdHttpContext : 
        IGetInstanceByIdRequestContract
    {
        public Guid InstanceId { get; set; }
    }
}