using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels.Manager
{
    public class ChangePasswordModel
    {
        /// <summary>
        /// 当前管理员主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 重复新密码
        /// </summary>
        public string ReNewPassword { get; set; }
    }
}
