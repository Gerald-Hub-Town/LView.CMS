using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SQLBuilder.Core;

namespace LView.CMS.Models
{
    [Table("MANAGERROLE")]
    public class ManagerRole
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SQLBuilder.Core.Key("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("ROLENAME")]
        public string RoleName { get; set; }

        /// <summary>
        /// 角色类型1超管2系管
        /// </summary>
        [Column("ROLEID")]
        public int RoleId { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        [Column("ISSYSTEM")]
        public int IsSystem { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        [Column("ADDMANAGERID")]
        public string AddManagerId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("ADDTIME")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("MODIFYMANAGERID")]
        public string ModifyManagerId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("MODIFYTIME")]
        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("ISDELETE")]
        public int IsDelete { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        public string Remark { get; set; }

    }
}
