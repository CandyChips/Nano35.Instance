using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientValidatorErrorResult :
        ICreateClientErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateClientPipeNode:
        IPipelineNode<
            ICreateClientRequestContract,
            ICreateClientResultContract>
    {
        private readonly IPipelineNode<
            ICreateClientRequestContract,
            ICreateClientResultContract> _nextNode;
        
        private readonly IValidator<
            ICreateClientRequestContract> _validator;

        public ValidatedCreateClientPipeNode(
            IValidator<ICreateClientRequestContract> validator,
            IPipelineNode<
                ICreateClientRequestContract, 
                ICreateClientResultContract> nextNode)
        {
            _nextNode = nextNode;
            _validator = validator;
        }

        public async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {    
            var res = _validator.Validate(input, ruleSet: "all");  
  
            if (!res.IsValid)
            {
                return new CreateClientValidatorErrorResult() {Message = res.Errors.FirstOrDefault()?.ErrorMessage};
            }
            return await _nextNode.Ask(input);
        }
    }
}