using System;
using SQLBuilder.Core;
using System.ComponentModel.DataAnnotations;

namespace LView.CMS.Models
{
    /// <summary>
    /// 后台管理菜单
    /// </summary>
    [Table("MENU")]
    public class Menu
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SQLBuilder.Core.Key("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 父菜单ID
        /// </summary>
        [Column("PARENTID")]
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Column("DISPLAYNAME")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        [Column("ICONURL")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Column("LINKURL")]
        public string LinkUrl { get; set; }

        /// <summary>
        /// 排序数字
        /// </summary>
        [Column("SORT")]
        public int Sort { get; set; }

        /// <summary>
        /// 操作权限（按钮权限时使用）
        /// </summary>
        [Column("PERMISSION")]
        public string Permission { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        [Column("ISDISPLAY")]
        public int IsDisplay { get; set; }

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
        public Boolean IsDelete { get; set; }
    }
}
