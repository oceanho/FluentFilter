using FluentFilter.Inetnal.ImplOfFilterField.Handlers;
using OhDotNetLib.Reflection;
using OhPrimitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFilter.Inetnal.ImplOfFilterField
{
    public static class FilterFieldHandlerFactory
    {
        private static readonly ConcurrentDictionary<string, Func<IFilterFieldHandler>> handlers = new ConcurrentDictionary<string, Func<IFilterFieldHandler>>();

        static FilterFieldHandlerFactory()
        {
            Register(() =>
            {
                return new LikeFilterFieldHandler();
            });

            Register(() =>
            {
                return new RangeFilterFieldHandler();
            });

            Register(() =>
            {
                return new CompareFilterFieldHandler();
            });

            Register(() =>
            {
                return new BetweenFilterFieldHandler();
            });

            Register(() =>
            {
                return new FreeDomRangeFilterFieldHandler();
            });

        }

        internal static IFilterFieldHandler GetHandler(Type filterFieldType)
        {
            var name = TypeHelper.GetGenericTypeUniqueName(filterFieldType);
            return handlers.ContainsKey(name) ? handlers[name]() : default(IFilterFieldHandler);
        }

        internal static TFilterFieldHandler GetHandler<TFilterFieldHandler>(Type filterFieldType)
            where TFilterFieldHandler : class, IFilterFieldHandler
        {
            return (TFilterFieldHandler)GetHandler(filterFieldType);
        }

        public static void Register<TFilterFieldHandler>(Func<TFilterFieldHandler> creationHandler)
            where TFilterFieldHandler : class, IFilterFieldHandler
        {
            var name = TypeHelper.GetGenericTypeUniqueName(creationHandler().FilterType);
            handlers[name] = creationHandler;
        }
    }
}
