using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels
{
    public class ChangeStatusModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 修改后的状态
        /// </summary>
        public int Status { get; set; }
    }
}
