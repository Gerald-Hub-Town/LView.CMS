using Baize.Core.Models;
using SQLBuilder.Core;
using Baize.Core.Helpers;
using System;

namespace Baize.Entities.MaterialCenter.Entity
{
    /// <summary>
    /// 工 具：白泽CodeBuilder Version:1.1.7.9
    /// 创 建：Gerald
    /// 日 期：2020/9/5 11:49:55
    /// 描 述：后台管理员表单
    /// </summary>
    [Table("LMSMANAGER")]
    public class LmsManagerEntity : BaseEntity
    {
		/// <summary>		/// 角色ID  - DB		/// </summary>		[Column("ROLEID")]		[ExcelColumn("角色ID",IsExport =false)]		public string RoleId { get; set; }				/// <summary>		/// 角色ID - Excel		/// </summary>		[Column("ROLEID",Insert =false,Update =false)]		[ExcelColumn("角色ID",IsExport =true)]		public string RoleName { get; set; }				/// <summary>		/// 用户名 		/// </summary>		[Column("USERNAME")]		[ExcelColumn("用户名")]		public string UserName { get; set; }				/// <summary>		/// 密码 		/// </summary>		[Column("PASSWORD")]		[ExcelColumn("密码")]		public string Password { get; set; }				/// <summary>		/// 头像 		/// </summary>		[Column("AVATAR")]		[ExcelColumn("头像")]		public string Avatar { get; set; }				/// <summary>		/// 昵称 		/// </summary>		[Column("NICKNAME")]		[ExcelColumn("昵称")]		public string NickName { get; set; }				/// <summary>		/// 手机号码 		/// </summary>		[Column("MOBILE")]		[ExcelColumn("手机号码")]		public string Mobile { get; set; }				/// <summary>		/// 邮箱 		/// </summary>		[Column("EMAIL")]		[ExcelColumn("邮箱")]		public string Email { get; set; }				/// <summary>		/// 登录次数 		/// </summary>		[Column("LOGINCOUNT")]		[ExcelColumn("登录次数")]		public int? LoginCount { get; set; }				/// <summary>		/// 上次登录时间 		/// </summary>		[Column("LOGINLASTTIME")]		[ExcelColumn("上次登录时间",Format ="date@yyyy-MM-dd HH:mm:ss")]		public DateTime? LoginLastTime { get; set; }				/// <summary>		/// 添加人  - DB		/// </summary>		[Column("ADDMANAGERID")]		[ExcelColumn("添加人",IsExport =false)]		public string AddManagerId { get; set; }				/// <summary>		/// 添加人 - Excel		/// </summary>		[Column("ADDMANAGERID",Insert =false,Update =false)]		[ExcelColumn("添加人",IsExport =true)]		public string AddManagerName { get; set; }				/// <summary>		/// 添加时间 		/// </summary>		[Column("ADDTIME")]		[ExcelColumn("添加时间",Format ="date@yyyy-MM-dd HH:mm:ss")]		public DateTime? AddTime { get; set; }				/// <summary>		/// 修改人  - DB		/// </summary>		[Column("MODIFYMANAGERID")]		[ExcelColumn("修改人",IsExport =false)]		public string ModifyManagerId { get; set; }				/// <summary>		/// 修改人 - Excel		/// </summary>		[Column("MODIFYMANAGERID",Insert =false,Update =false)]		[ExcelColumn("修改人",IsExport =true)]		public string ModifyManagerName { get; set; }				/// <summary>		/// 修改时间 		/// </summary>		[Column("MODIFYTIME")]		[ExcelColumn("修改时间",Format ="date@yyyy-MM-dd HH:mm:ss")]		public DateTime? ModifyTime { get; set; }				/// <summary>		/// 是否锁定 		/// </summary>		[Column("ISLOCK")]		[ExcelColumn("是否锁定")]		public int? IsLock { get; set; }				/// <summary>		/// 是否删除 		/// </summary>		[Column("ISDELETE")]		[ExcelColumn("是否删除")]		public int? IsDelete { get; set; }				/// <summary>		/// 上次登录IP 		/// </summary>		[Column("LOGINLASTIP")]		[ExcelColumn("上次登录IP")]		public string LoginLastIP { get; set; }		
    }
}