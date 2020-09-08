using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LView.CMS.Models.Partial
{
    public partial class LMSManager
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [NotMapped]
        public string RoleName { get; set; }
    }
}
