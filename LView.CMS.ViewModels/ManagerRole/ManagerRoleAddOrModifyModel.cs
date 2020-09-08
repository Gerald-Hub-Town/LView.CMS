using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels
{
    public class ManagerRoleAddOrModifyModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色类型1超管2系管3讲师4学员
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public int IsSystem { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public int[] MenuIds { get; set; }
    }
}
