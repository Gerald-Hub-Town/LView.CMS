using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LView.CMS.Core.Extensions
{
    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
