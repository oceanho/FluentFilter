using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluent.DataFilter
{
    public class FieldFilterInfo<TFilterField> : FieldFilterInfo
        where TFilterField : IFilterField
    {
        private Type _fieldFilterType;
        private TFilterField _fieldFilterInstace;

        public FieldFilterInfo(TFilterField fieldInstace)
        {
            _fieldFilterType = typeof(TFilterField);
            _fieldFilterInstace = fieldInstace;
        }

        public TFilterField Instance
        {
            get => _fieldFilterInstace;
            set => _fieldFilterInstace = value;
        }

        public override Type FilterFieldType
        {
            get => _fieldFilterType;
        }
        public override IFilterField FilterFieldInstace
        {
            get => Instance;
        }
    }
}
