using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.Models
{
    public class ManagerListModel
    {
        /// <summary>
		/// 主键
		/// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        //public string RoleName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int? LoginCount { get; set; }

        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        public string LoginLastIp { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime? LoginLastTime { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public int IsLock { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
