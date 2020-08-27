using FluentValidation;
using LView.CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LView.CMS.Site.Validation
{
    public class ManagerLockStatusModelValidation : AbstractValidator<ChangeStatusModel>
    {
        public ManagerLockStatusModelValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage("主键不能为空");
            RuleFor(x => x.Status).NotNull().WithMessage("状态不能为空");
        }
    }
}
