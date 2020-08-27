using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.Models
{
    /// <summary>
    /// Gerald
    /// 2019-03-07 16:50:56
    /// 后台管理员角色
    /// </summary>
    public partial class ManagerRole
    {

        /// <summary>
        /// 菜单ID数组  
        /// </summary>
        public virtual int[] MenuIds { get; set; }
    }
}
