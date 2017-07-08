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
    internal static class FilterFieldHandlerFactory
    {
        private static readonly ConcurrentDictionary<string, Func<IFilterFieldHandler>> handlers = new ConcurrentDictionary<string, Func<IFilterFieldHandler>>();

        static FilterFieldHandlerFactory()
        {
            RegisterInternalFilterFieldHandlers();
        }

        private static void RegisterInternalFilterFieldHandlers()
        {

            Register(() =>
            {
                return new LikeFilterFieldHandler();
            });

            Register(() =>
            {
                return new EqualFilterFieldHandler();
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
                return new ContainsFilterFieldHandler();
            });

            Register(() =>
            {
                return new FreeDomRangeFilterFieldHandler();
            });
        }

        public static IFilterFieldHandler GetHandler(Type filterFieldType)
        {
            var name = TypeHelper.GetGenericTypeUniqueName(filterFieldType);
            return handlers.ContainsKey(name) ? handlers[name]() : new EmptyFilterFieldHandler();
        }

        public static TFilterFieldHandler GetHandler<TFilterFieldHandler>(Type filterFieldType)
            where TFilterFieldHandler : class, IFilterFieldHandler
        {
            return (TFilterFieldHandler)GetHandler(filterFieldType);
        }

        public static void Register<TFilterFieldHandler>(Func<TFilterFieldHandler> creationHandler)
            where TFilterFieldHandler : class, IFilterFieldHandler
        {
            handlers[creationHandler().FilterFieldTypeUniqueName] = creationHandler;
        }

        public static void Clear()
        {
            handlers.Clear();
        }

        public static void Reset()
        {
            Clear();
            RegisterInternalFilterFieldHandlers();
        }
    }
}
