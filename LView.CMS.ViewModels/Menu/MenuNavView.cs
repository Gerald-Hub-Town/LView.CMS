using System;
using System.Collections.Generic;
using System.Text;

namespace LView.CMS.ViewModels
{
    public class MenuNavView
    {
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string IconUrl { get; set; }

        public string LinkUrl { get; set; }

        //public bool Spread { get; set; } = false;

        //public string Target { get; set; }
    }
}
