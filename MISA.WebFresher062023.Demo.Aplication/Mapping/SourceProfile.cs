using AutoMapper;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo.Application
{
    public class SourceProfile : Profile
    {
        public SourceProfile()
        {
            CreateMap<Source, SourceDto>();

            CreateMap<SourceCreateDto, Source>();
            CreateMap<SourceUpdateDto, Source>();
        }
    }
}
