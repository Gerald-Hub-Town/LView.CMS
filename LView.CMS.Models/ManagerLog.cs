using System;
using SQLBuilder.Core;

namespace LView.CMS.Models
{
    /// <summary>
    /// Gerald
    /// 2019-03-07 16:50:56
    /// 操作日志
    /// </summary>
    [Table("MANAGERLOG")]
    public class ManagerLog
    {
        /// <summary>
        ///  主键
        /// </summary>
        [SQLBuilder.Core.Key("ID")]
        public string Id { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Column("ACTIONTYPE")]
        public string ActionType { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Column("ADDMANAGERID")]
        public string AddManagerId { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [Column("ADDMANAGERNICKNAME")]
        public string AddManagerNickName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Column("ADDTIME")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 操作IP
        /// </summary>
        [Column("ADDIP")]
        public string AddIP { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("REMARK")]
        public string Remark { get; set; }


    }
}
