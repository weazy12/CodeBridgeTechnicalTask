using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTask.BLL.Extentions
{
    public static class ResourcesExtentions
    {
        public static string FormatWith(this string resource, params object[] args)
        {
            return string.Format(resource, args);
        }
    }
}
