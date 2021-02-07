using System;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.HttpContracts
{
    public class CreateUnitHttpContext : 
        ICreateUnitRequestContract
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string WorkingFormat { get; set; }
        public string Phone { get; set; }
        public Guid UnitTypeId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid InstanceId { get; set; }
    }
}