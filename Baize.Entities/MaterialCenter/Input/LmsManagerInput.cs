using Baize.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Baize.Core.Helpers;

namespace Baize.Entities.MaterialCenter.Input
{

    /// <summary>
    /// 数据库导入Excel类
    /// </summary>
    public class LmsManagerExcelInput
    {
        /* Excel导入,仅供参考

        /// <summary>
        /// 工号/NN
        /// </summary>
        [ExcelColumn("工号")]
        public string Account { get; set; }

        */
    }
    
    /// <summary>
    /// 后台管理员表单新增参数
    /// </summary>
    public class LmsManagerAddInput : BaseInput
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        
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
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 登录次数
        /// </summary>
        public int? LoginCount { get; set; }
        
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LoginLastTime { get; set; }
        
        /// <summary>
        /// 添加人
        /// </summary>
        public string AddManagerId { get; set; }
        
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime { get; set; }
        
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyManagerId { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int? IsLock { get; set; }
        
        /// <summary>
        /// 是否删除
        /// </summary>
        public int? IsDelete { get; set; }
        
        /// <summary>
        /// 上次登录IP
        /// </summary>
        public string LoginLastIP { get; set; }
        
    }

    /// <summary>
    /// 后台管理员表单修改参数
    /// </summary>
    public class LmsManagerModifyInput : LmsManagerAddInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Required]
        public string Id { get; set; }
    }

    /// <summary>
    /// 后台管理员表单查询参数
    /// </summary>
    public class LmsManagerQueryInput
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否有效 -1:所有 0:无效 1:有效
        /// </summary>
        [Range(-1, 1)]
        public int? Enabled { get; set; }
    }
}