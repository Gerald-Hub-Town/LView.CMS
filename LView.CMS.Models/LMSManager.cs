using System;
using System.ComponentModel.DataAnnotations;
using SQLBuilder.Core;

namespace LView.CMS.Models
{
    [Table("LMSMANAGER")]
    public partial class LMSManager
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SQLBuilder.Core.Key("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [Required]
        [Column("ROLEID")]
        public string RoleId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string RoleName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(32)]
        [Column("USERNAME")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(128)]
        [Column("PASSWORD")]
        public string Password { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(256)]
        [Column("AVATAR")]
        public string Avatar { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [MaxLength(32)]
        [Column("NICKNAME")]
        public string NickName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(16)]
        [Column("MOBILE")]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [MaxLength(128)]
        [Column("EMAIL")]
        public string Email { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [MaxLength(10)]
        [Column("LOGINCOUNT")]
        public int? LoginCount { get; set; }

        /// <summary>
        /// 最后一次登录IP
        /// </summary>
        [MaxLength(64)]
        [Column("LOGINLASTIP")]
        public string LoginLastIp { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        [MaxLength(23)]
        [Column("LOGINLASTTIME")]
        public DateTime? LoginLastTime { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [Required]
        [MaxLength(10)]
        [Column("ADDMANAGERID")]
        public string AddManagerId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        [MaxLength(23)]
        [Column("ADDTIME")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        //[MaxLength(10)]
        [Column("MODIFYMANAGERID")]
        public string ModifyManagerId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [MaxLength(23)]
        [Column("MODIFYTIME")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        [Required]
        [MaxLength(1)]
        [Column("ISLOCK")]
        public int IsLock { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        [MaxLength(1)]
        [Column("ISDELETE")]
        public int IsDelete { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        [Column("REMARK")]
        public string Remark { get; set; }
    }
}
