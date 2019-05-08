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
            CreateMap<DataSource, DataSourceDto>();
            CreateMap<DataSourceDto, DataSource>();

            CreateMap<DataSourceFolder, DataSourceFolderDto>().ForMember(src => src.DataSources, opt => opt.Ignore());//忽略延迟加载的属性
            CreateMap<DataSourceFolderDto, DataSourceFolder>().ForMember(dest => dest.DomainEvents, opt => opt.Ignore());//不映射domain events属性，因为缺少继承

            CreateMap<DataSourceField, DataSourceFieldDto>();
            CreateMap<DataSourceFieldDto, DataSourceField>();//.ForMember(s => s.DataSource, opt => opt.Ignore());
        }

    }
}
