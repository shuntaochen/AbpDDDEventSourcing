using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EP.Query.DataSource.Profiles
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            //CreateMap<>
            CreateMap<DataSource, DataSourceDto>().ForMember(m => m.DataSourceFields, f => f.MapFrom(t => t.DataSourceFields.ToList()));
            CreateMap<DataSourceDto, DataSource>();

            CreateMap<DataSourceFolder, DataSourceFieldDto>();
            CreateMap<DataSourceFieldDto, DataSourceFolder>();

            CreateMap<DataSourceField, DataSourceFieldDto>();
            CreateMap<DataSourceFieldDto, DataSourceField>();
        }

    }
}
