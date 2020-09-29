using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Codeizi.CQRS.Saga.Utils
{
    internal class ReflectionUtil
    {
        private readonly ConcurrentDictionary<string, Type> _cacheTypes;

        public ReflectionUtil()
            => _cacheTypes = new ConcurrentDictionary<string, Type>();

        internal Type GetTypeByName(string nameType)
        {
            if (_cacheTypes.ContainsKey(nameType))
                return _cacheTypes[nameType];

            var type = LoadTypeByName(nameType);

            _cacheTypes.TryAdd(nameType, type);
            return _cacheTypes[nameType];
        }

        internal Type LoadTypeByName(string nameType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Reverse())
            {
                var type = assembly.GetType(nameType);
                if (type != null)
                    return type;
            }

            return null;
        }
    }
}