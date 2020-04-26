namespace AutoParts.Web.Server.MappingProfiles.Converters
{
    using AutoMapper;
    
    using Google.Protobuf.Collections;

    using System.Collections.Generic;

    public class EnumerableToRepeatedFieldTypeConverter<TSource, TDestination> : ITypeConverter<IEnumerable<TSource>, RepeatedField<TDestination>>
    {
        public RepeatedField<TDestination> Convert(IEnumerable<TSource> source, RepeatedField<TDestination> destination, ResolutionContext context)
        {
            destination = destination ?? new RepeatedField<TDestination>();

            var destinationEnumerable = context.Mapper.Map<IEnumerable<TDestination>>(source);

            destination.AddRange(destinationEnumerable);

            return destination;
        }
    }
}
