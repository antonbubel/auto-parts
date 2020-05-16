﻿namespace AutoParts.Web.Server.MappingProfiles
{
    using AutoMapper;

    using Protos;

    using Core.Contracts.AutoParts.Notifications;
    using GetAutoPartsRequestBL = Core.Contracts.AutoParts.Requests.GetAutoPartsRequest;

    public class AutoPartMappingProfile : Profile
    {
        public AutoPartMappingProfile()
        {
            CreateMap<GetAutoPartsRequest, GetAutoPartsRequestBL>()
                .ForMember(requestBL => requestBL.PageNumber, conf => conf.MapFrom(request => request.PageNumber))
                .ForMember(requestBL => requestBL.PageSize, conf => conf.MapFrom(request => request.PageSize))
                .ForMember(requestBL => requestBL.SearchText, conf => conf.MapFrom(request => request.SearchText))
                .ForMember(requestBL => requestBL.AvailableOnly, conf => conf.MapFrom(request => request.AvailableOnly))
                .ForMember(requestBL => requestBL.SortBy, conf => conf.MapFrom(request => request.SortBy))
                .ForMember(requestBL => requestBL.CarModificationId, conf => conf.MapFrom(request => ConvertIdFromRequestToOptional(request.CarModificationId)))
                .ForMember(requestBL => requestBL.ManufacturerId, conf => conf.MapFrom(request => ConvertIdFromRequestToOptional(request.ManufacturerId)))
                .ForMember(requestBL => requestBL.CountryId, conf => conf.MapFrom(request => ConvertIdFromRequestToOptional(request.CountryId)))
                .ForMember(requestBL => requestBL.SupplierId, conf => conf.MapFrom(request => ConvertIdFromRequestToOptional(request.SupplierId)))
                .ForMember(requestBL => requestBL.SubCatalogId, conf => conf.MapFrom(request => ConvertIdFromRequestToOptional(request.SubCatalogId)));

            CreateMap<CreateAutoPartRequest, CreateAutoPartNotification>()
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.Description, conf => conf.MapFrom(request => request.Description))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageFileName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.ImageFileBugger.ToByteArray()))
                .ForMember(notification => notification.Price, conf => conf.MapFrom(request => request.Price))
                .ForMember(notification => notification.Quantity, conf => conf.MapFrom(request => request.Quantity))
                .ForMember(notification => notification.IsAvailable, conf => conf.MapFrom(request => request.IsAvailable))
                .ForMember(notification => notification.ManufacturerId, conf => conf.MapFrom(request => request.ManufacturerId))
                .ForMember(notification => notification.CountryId, conf => conf.MapFrom(request => request.CountryId))
                .ForMember(notification => notification.CarModificationId, conf => conf.MapFrom(request => request.CarModificationId))
                .ForMember(notification => notification.SubCatalogId, conf => conf.MapFrom(request => request.SubCatalogId))
                .ForMember(notification => notification.SupplierId, conf => conf.Ignore());

            CreateMap<UpdateAutoPartRequest, UpdateAutoPartNotification>()
                .ForMember(notification => notification.Name, conf => conf.MapFrom(request => request.Name))
                .ForMember(notification => notification.Description, conf => conf.MapFrom(request => request.Description))
                .ForMember(notification => notification.ImageFileName, conf => conf.MapFrom(request => request.ImageFileName))
                .ForMember(notification => notification.ImageFileBuffer, conf => conf.MapFrom(request => request.ImageFileBugger.ToByteArray()))
                .ForMember(notification => notification.Price, conf => conf.MapFrom(request => request.Price))
                .ForMember(notification => notification.Quantity, conf => conf.MapFrom(request => request.Quantity))
                .ForMember(notification => notification.IsAvailable, conf => conf.MapFrom(request => request.IsAvailable))
                .ForMember(notification => notification.SupplierId, conf => conf.Ignore());
        }

        private long? ConvertIdFromRequestToOptional(long id)
        {
            return id == default
                ? null
                : (long?)id;
        }
    }
}