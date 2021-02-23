using System;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Models
{
    public class CreateClientValidator :
        AbstractValidator<ICreateClientRequestContract>
    {
        public CreateClientValidator()
        {
            RuleSet("all", () =>   
            {  
                RuleFor(x => 
                    x.NewId).Must(CheckGuid).WithMessage("Ошибка валидации");  
                RuleFor(x => 
                    x.UserId).Must(CheckGuid).WithMessage("Ошибка валидации");  
                RuleFor(x => 
                    x.InstanceId).Must(CheckGuid).WithMessage("Ошибка валидации");  
            });    
        }   
        
        private bool CheckGuid(Guid id)  
        {  
            return id == Guid.Empty;  
        }  
    }
}