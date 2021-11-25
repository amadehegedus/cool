using AutoMapper;
using Cool.Common.DTOs;
using Cool.Dal.Entities;

namespace Cool.Bll.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Caff, CaffDto>()
                .ForMember(x => x.PreviewBitmap, o => o.Ignore());

            CreateMap<Tag, TagDto>();
            CreateMap<Comment, CommentDto>();
        }
    }
}
