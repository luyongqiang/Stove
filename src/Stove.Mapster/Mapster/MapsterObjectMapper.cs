﻿using Mapster;

using Stove.ObjectMapping;

namespace Stove.Mapster.Mapster
{
    public class MapsterObjectMapper : IObjectMapper
    {
        private readonly IStoveMapsterConfiguration mapsterConfiguration;

        public MapsterObjectMapper(IStoveMapsterConfiguration mapsterConfiguration)
        {
            this.mapsterConfiguration = mapsterConfiguration;
        }

        public TDestination Map<TDestination>(object source)
        {
            return source.Adapt<TDestination>(mapsterConfiguration.Configuration);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return source.Adapt(destination, mapsterConfiguration.Configuration);
        }
    }
}
