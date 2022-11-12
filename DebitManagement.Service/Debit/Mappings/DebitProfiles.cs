using AutoMapper;
using DebitManagement.Service.Debit.Dto;

namespace DebitManagement.Service.Debit.Mappings;

public class DebitProfiles : Profile
{
    public DebitProfiles()
    {
        #region DebitDto

        CreateMap<Core.Entities.Debit, DebitDto>()
            .ForMember(x => x.Username, y => y.MapFrom(src => src.User.Username))
            .ForMember(x => x.ProductCode, y => y.MapFrom(src => src.Product.ProductCode))
            .ForMember(x => x.ProductDescription, y => y.MapFrom(src => src.Product.ProductDescription))
            .ForMember(x => x.ProductId, y => y.MapFrom(src => src.Product.Id))
            .ForMember(x => x.UserId, y => y.MapFrom(src => src.User.Id));
        CreateMap<DebitDto, Core.Entities.Debit>();
        // .ForMember(x => x.User.Username, y => y.MapFrom(src => src.Username))
        // .ForMember(x => x.Product.ProductCode, y => y.MapFrom(src => src.ProductCode))
        // .ForMember(x => x.Product.ProductDescription, y => y.MapFrom(src => src.ProductDescription))

        #endregion

        #region DebitCreateDto

        CreateMap<Core.Entities.Debit, DebitCreateUpdateDto>()
            .ForMember(x => x.ProductId, y => y.MapFrom(src => src.Product.Id))
            .ForMember(x => x.UserId, y => y.MapFrom(src => src.User.Id));
        CreateMap<DebitCreateUpdateDto, Core.Entities.Debit>();

        #endregion
    }
}