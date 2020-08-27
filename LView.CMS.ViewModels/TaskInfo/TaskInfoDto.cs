using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels
{
    public class TaskInfoDto
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Group { get; set; }

        public String Description { get; set; }

        public String Assembly { get; set; }

        public String ClassNamme { get; set; }

        public Int32 Status { get; set; }

        public String Cron { get; set; }
    }
}
