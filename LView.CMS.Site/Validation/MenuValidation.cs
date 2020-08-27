using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LView.CMS.ViewModels;

namespace LView.CMS.Site.Validation
{
    public class MenuValidation : AbstractValidator<MenuAddOrModifyModel>
    {
        public MenuValidation()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.ParentId).NotNull().WithMessage("上级菜单不能为空");
            RuleFor(x => x.Name).NotEmpty().Length(5, 32).WithMessage("菜单别名不能为空且最大长度不能超过32个字符");
            RuleFor(x => x.DisplayName).Length(0, 64).WithMessage("菜单显示名称长度不能超过64个字符");
            RuleFor(x => x.IconUrl).Length(0, 128).WithMessage("图标路径长度不能超过128个字符");
            RuleFor(x => x.LinkUrl).Length(0, 128).WithMessage("链接地址长度不能超过128个字符");
            RuleFor(x => x.IsSystem).NotNull().WithMessage("系统默认必须选择");
            RuleFor(x => x.IsDisplay).NotNull().WithMessage("显示必须选择");
        }
    }
}
