using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace LView.CMS.Core.Extensions
{
    public static class AutoMapperExtensions
    {
        private static readonly ConcurrentDictionary<string, MapperConfiguration> autoMapperCache = new ConcurrentDictionary<string, MapperConfiguration>();

        public static T MapTo<T>(this object @this, MapperConfiguration config = null)
        {
            if (@this == null)
            {
                return default(T);
            }

            if (config == null)
            {
                config = autoMapperCache.GetOrAdd(GetKey(@this.GetType(), typeof(T)), (Func<string, MapperConfiguration>)((string x) => new MapperConfiguration((Action<IMapperConfigurationExpression>)delegate (IMapperConfigurationExpression cfg)
                {
                    cfg.CreateMap(@this.GetType(), typeof(T));
                })));
            }

            return config.CreateMapper().Map<T>(@this);
        }

        private static string GetKey(Type sourceType, Type destType)
        {
            return sourceType.FullName + "_" + destType.FullName;
        }
    }
}
