using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Mappings
{
    public class MappingInfo
    {
        public string ExprName { get; set; }
        public PropertyInfo Property { get; set; }
    }
}
