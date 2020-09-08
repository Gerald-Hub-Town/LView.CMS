using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LView.CMS.ViewModels;

namespace LView.CMS.Site.Validation
{
    public class ManagerRoleValidation : AbstractValidator<ManagerRoleAddOrModifyModel>
    {
        public ManagerRoleValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.RoleName).NotEmpty().Length(1, 64).WithMessage("角色名称不能为空并且长度不能超过64个字符");
            RuleFor(x => x.IsSystem).NotNull().WithMessage("系统默认必须选择");
            RuleFor(x => x.Remark).Length(0, 128).WithMessage("备注信息的长度必须符合规则");
        }
    }
}
