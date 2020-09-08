using System;
using System.Collections.Generic;
using System.Text;
using SQLBuilder.Core;

namespace LView.CMS.Models
{
    [Table("ROLEPERMISSION")]
    public class RolePermission
    {
        [SQLBuilder.Core.Key("ID")]
        public string Id { get; set; }

        [Column("ROLEID")]
        public int RoleId { get; set; }

        [Column("MENUID")]
        public string MenuId { get; set; }

        [Column("PERMISSION")]
        public string Permission { get; set; }
    }
}
