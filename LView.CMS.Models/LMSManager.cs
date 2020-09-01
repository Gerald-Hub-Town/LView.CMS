using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LView.CMS.Models
{

    public partial class LMSManager
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        [MaxLength(10)]
        public int RoleId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(256)]
        public string Avatar { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [MaxLength(32)]
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(16)]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [MaxLength(128)]
        public string Email { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [MaxLength(10)]
        public int? LoginCount { get; set; }

        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        [MaxLength(64)]
        public string LoginLastIp { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        [MaxLength(23)]
        public DateTime? LoginLastTime { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [Required]
        [MaxLength(10)]
        public int AddManagerId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(10)]
        public int? ModifyManagerId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [MaxLength(23)]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Required]
        [MaxLength(1)]
        public int IsLock { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        [MaxLength(1)]
        public int IsDelete { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }
    }
}
