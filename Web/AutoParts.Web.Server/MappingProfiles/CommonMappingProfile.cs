namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Google.Protobuf.Collections;
    
    using System.Collections.Generic;

    using Converters;

    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap(typeof(IEnumerable<>), typeof(RepeatedField<>))
                .ConvertUsing(typeof(EnumerableToRepeatedFieldTypeConverter<,>));
        }
    }
}
