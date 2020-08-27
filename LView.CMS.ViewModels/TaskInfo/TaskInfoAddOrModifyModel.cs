using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels
{
    public class TaskInfoAddOrModifyModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Group { get; set; }
        public String Description { get; set; }
        public String Assembly { get; set; }
        public String ClassName { get; set; }
        public String Cron { get; set; }
        public DateTime? AddTime { get; set; }
        public int AddManagerId { get; set; }
    }
}
