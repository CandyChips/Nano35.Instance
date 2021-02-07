﻿using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
        
    public class GetAllClientHttpContext : 
        IGetAllClientsRequestContract
    {
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid InstanceId { get; set; }
    }
}
