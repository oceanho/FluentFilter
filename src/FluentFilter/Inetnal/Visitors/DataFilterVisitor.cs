using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.Visitors
{
    internal class DataFilterVisitor
    {
        private DataFilterVisitorProvider _provider;
        public DataFilterVisitor()
        {
            _provider = new DataFilterVisitorProvider();
        }
        public DataFilterVisitorProvider Provider { get => _provider; }
    }
}
