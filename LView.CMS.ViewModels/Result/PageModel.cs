using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels
{
    public class PageModel
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Key { get; set; }
        public PageModel()
        {
            Page = 1;
            Limit = 10;
        }
    }
}
