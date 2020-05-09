namespace AutoParts.Web.Server.Services
{
    using AutoMapper;

    using MediatR;

    using FluentValidation;

    using Grpc.Core;

    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;

    using Protos;

    using Core.Contracts.Suppliers.Models;
    using Core.Contracts.Suppliers.Requests;
    using Core.Contracts.Suppliers.Exceptions;
    using Core.Contracts.Suppliers.Notifications;

    using Infrastructure.Exceptions;

    public class SupplierService : GrpcSupplierService.GrpcSupplierServiceBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public SupplierService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<InviteSupplierResponse> InviteSupplier(InviteSupplierRequest request, ServerCallContext context)
        {
            var notification = new InviteSupplierNotification
            {
                Email = request.Email,
                Name = request.Name
            };

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new InviteSupplierResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (InviteSupplierException exception)
            {
                return new InviteSupplierResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new InviteSupplierResponse
            {
                IsError = false
            };
        }

        [Authorize(nameof(UserType.Supplier))]
        public override async Task<UpdateSupplierProfileResponse> UpdateSupplierProfile(UpdateSupplierProfileRequest request, ServerCallContext context)
        {
            var notification = mapper.Map<UpdateSupplierProfileNotification>(request);
            notification.UserId = context.GetLoggedInUserId().Value;

            try
            {
                await mediator.Publish(notification);
            }
            catch (ValidationException exception)
            {
                return new UpdateSupplierProfileResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }
            catch (UpdateSupplierProfileException exception)
            {
                return new UpdateSupplierProfileResponse
                {
                    IsError = true,
                    Error = exception.Message
                };
            }

            return new UpdateSupplierProfileResponse
            {
                IsError = false
            };
        }

        [AllowAnonymous]
        public override async Task<GetSupplierEmailFromInvitationResponse> GetSupplierEmailFromInvitation(GetSupplierEmailFromInvitationRequest request, ServerCallContext context)
        {
            var email = string.Empty;

            try
            {
                email = await mediator.Send(new GetSupplierEmailByInvitationTokenRequest { Token = request.InvitationToken });
            }
            catch (NotFoundException)
            {
                return new GetSupplierEmailFromInvitationResponse
                {
                    Email = email,
                    Status = ResponseStatus.NotFound
                };
            }

            return new GetSupplierEmailFromInvitationResponse
            {
                Email = email,
                Status = ResponseStatus.Ok
            };
        }

        [Authorize(nameof(UserType.Supplier))]
        public override async Task<GetCurrentUserSupplierPrivateProfileResponse> GetCurrentUserSupplierPrivateProfile(GetCurrentUserSupplierPrivateProfileRequest request, ServerCallContext context)
        {
            var userId = context.GetLoggedInUserId();
            var profile = await mediator.Send(new GetPrivateSupplierProfileRequest { UserId = userId.Value });

            return new GetCurrentUserSupplierPrivateProfileResponse
            {
                Model = mapper.Map<SupplierPrivateProfile>(profile)
            };
        }

        [Authorize(nameof(UserType.Administrator))]
        public override async Task<GetSupplierPrivateProfileByIdResponse> GetSupplierPrivateProfileById(GetSupplierPrivateProfileByIdRequest request, ServerCallContext context)
        {
            SupplierPrivateProfileModel profile;

            try
            {
                profile = await mediator.Send(new GetPrivateSupplierProfileRequest { UserId = request.Id });
            }
            catch (NotFoundException)
            {
                return new GetSupplierPrivateProfileByIdResponse
                {
                    Status = ResponseStatus.NotFound
                };
            }

            return new GetSupplierPrivateProfileByIdResponse
            {
                Model = mapper.Map<SupplierPrivateProfile>(profile),
                Status = ResponseStatus.Ok
            };
        }

        [AllowAnonymous]
        public override async Task<GetSupplierPublicProfileByIdResponse> GetSupplierPublicProfileById(GetSupplierPublicProfileByIdRequest request, ServerCallContext context)
        {
            SupplierPublicProfileModel profile;

            try
            {
                profile = await mediator.Send(new GetSupplierRequest { SupplierId = request.Id });
            }
            catch (NotFoundException)
            {
                return new GetSupplierPublicProfileByIdResponse
                {
                    Status = ResponseStatus.NotFound
                };
            }

            return new GetSupplierPublicProfileByIdResponse
            {
                Model = mapper.Map<SupplierPublicProfile>(profile),
                Status = ResponseStatus.Ok
            };
        }

        [AllowAnonymous]
        public override async Task<GetSuppliersResponse> GetSuppliers(PaginationFilter request, ServerCallContext context)
        {
            var suppliers = await mediator.Send(new GetSuppliersRequest { PageNumber = request.PageNumber, PageSize = request.PageSize });

            var response = new GetSuppliersResponse();

            mapper.Map(suppliers, response.Suppliers);

            return response;
        }
    }
}
