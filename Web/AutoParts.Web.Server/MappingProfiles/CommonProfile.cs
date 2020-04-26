namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Google.Protobuf.Collections;
    
    using System.Collections.Generic;

    using Converters;

    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>))
                .ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
        }
    }
}
