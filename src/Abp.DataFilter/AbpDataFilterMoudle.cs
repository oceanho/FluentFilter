using System;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Abp.DataFilter
{
    public class AbpDataFilterMoudle : AbpModule
    {
        public override void PreInitialize()
        {
            
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpDataFilterMoudle).GetAssembly());
        }
    }
}
