namespace AutoParts.Core.Implementation.AutoParts.MappingProfiles
{
    using AutoMapper;

    using Common.Extensions;

    using Contracts.AutoParts.Models;
    using Contracts.AutoParts.Requests;
    using Contracts.AutoParts.Notifications;

    using Data.Model.Filters;
    using Data.Model.Entities;
    using Data.Model.Projections;

    public class AutoPartMappingProfile : Profile
    {
        public AutoPartMappingProfile()
        {
            CreateMap<AutoPartProjection, AutoPartModel>()
                .ForMember(model => model.Id, conf => conf.MapFrom(projection => projection.Id))
                .ForMember(model => model.Name, conf => conf.MapFrom(projection => projection.Name))
                .ForMember(model => model.Description, conf => conf.MapFrom(projection => projection.Description))
                .ForMember(model => model.ImageUrl, conf => conf.MapFrom(projection => projection.Image))
                .ForMember(model => model.Quantity, conf => conf.MapFrom(projection => projection.Quantity))
                .ForMember(model => model.Price, conf => conf.MapFrom(projection => projection.Price))
                .ForMember(model => model.IsAvailable, conf => conf.MapFrom(projection => projection.IsAvailable))
                .ForMember(model => model.CountryId, conf => conf.MapFrom(projection => projection.CountryId))
                .ForMember(model => model.CountryName, conf => conf.MapFrom(projection => projection.CountryName))
                .ForMember(model => model.ManufacturerId, conf => conf.MapFrom(projection => projection.ManufacturerId))
                .ForMember(model => model.ManufacturerName, conf => conf.MapFrom(projection => projection.ManufacturerName))
                .ForMember(model => model.SupplierId, conf => conf.MapFrom(projection => projection.SupplierId))
                .ForMember(model => model.SupplierName, conf => conf.MapFrom(projection => projection.SupplierName))
                .ForMember(model => model.SubCatalogId, conf => conf.MapFrom(projection => projection.AutoPartsCatalogSubGroupId))
                .ForMember(model => model.SubCatalogName, conf => conf.MapFrom(projection => projection.AutoPartsCatalogSubGroupName));

            CreateMap<GetAutoPartsRequest, AutoPartsFilter>()
                .ForMember(filter => filter.ItemsToSkip, conf => conf.MapFrom(request => request.GetItemsToSkip()))
                .ForMember(filter => filter.ItemsToTake, conf => conf.MapFrom(request => request.GetItemsToTake()))
                .ForMember(filter => filter.SearchText, conf => conf.MapFrom(request => request.SearchText))
                .ForMember(filter => filter.CarModificationId, conf => conf.MapFrom(request => request.CarModificationId))
                .ForMember(filter => filter.ManufacturerId, conf => conf.MapFrom(request => request.ManufacturerId))
                .ForMember(filter => filter.CountryId, conf => conf.MapFrom(request => request.CountryId))
                .ForMember(filter => filter.SupplierId, conf => conf.MapFrom(request => request.SupplierId))
                .ForMember(filter => filter.AutoPartsCatalogSubGroupId, conf => conf.MapFrom(request => request.SubCatalogId))
                .ForMember(filter => filter.AvailableOnly, conf => conf.MapFrom(request => request.AvailableOnly));

            CreateMap<CreateAutoPartNotification, AutoPart>()
                .ForMember(autoPart => autoPart.Id, conf => conf.Ignore())
                .ForMember(autoPart => autoPart.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(autoPart => autoPart.NormalizedName, conf => conf.MapFrom(notification => notification.Name.ToUpperInvariant()))
                .ForMember(autoPart => autoPart.Description, conf => conf.MapFrom(notification => notification.Description))
                .ForMember(autoPart => autoPart.Image, conf => conf.Ignore())
                .ForMember(autoPart => autoPart.Price, conf => conf.MapFrom(notification => notification.Price))
                .ForMember(autoPart => autoPart.Quantity, conf => conf.MapFrom(notification => notification.Quantity))
                .ForMember(autoPart => autoPart.IsAvailable, conf => conf.MapFrom(notification => notification.IsAvailable))
                .ForMember(autoPart => autoPart.CarModificationId, conf => conf.MapFrom(notification => notification.CarModificationId))
                .ForMember(autoPart => autoPart.ManufacturerId, conf => conf.MapFrom(notification => notification.ManufacturerId))
                .ForMember(autoPart => autoPart.SupplierId, conf => conf.MapFrom(notification => notification.SupplierId))
                .ForMember(autoPart => autoPart.CountryId, conf => conf.MapFrom(notification => notification.CountryId))
                .ForMember(autoPart => autoPart.AutoPartsCatalogSubGroupId, conf => conf.MapFrom(notification => notification.SubCatalogId));

            CreateMap<UpdateAutoPartNotification, AutoPart>()
                .ForMember(autoPart => autoPart.Id, conf => conf.MapFrom(notification => notification.AutoPartId))
                .ForMember(autoPart => autoPart.Name, conf => conf.MapFrom(notification => notification.Name))
                .ForMember(autoPart => autoPart.NormalizedName, conf => conf.MapFrom(notification => notification.Name.ToUpperInvariant()))
                .ForMember(autoPart => autoPart.Description, conf => conf.MapFrom(notification => notification.Description))
                .ForMember(autoPart => autoPart.Image, conf => conf.Ignore())
                .ForMember(autoPart => autoPart.Price, conf => conf.MapFrom(notification => notification.Price))
                .ForMember(autoPart => autoPart.Quantity, conf => conf.MapFrom(notification => notification.Quantity))
                .ForMember(autoPart => autoPart.IsAvailable, conf => conf.MapFrom(notification => notification.IsAvailable))
                .ForMember(autoPart => autoPart.SupplierId, conf => conf.MapFrom(notification => notification.SupplierId));
        }
    }
}
