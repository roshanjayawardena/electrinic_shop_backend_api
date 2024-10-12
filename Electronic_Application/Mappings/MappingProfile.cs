using AutoMapper;
using Electronic_Application.Features.Auth.Commands.Login;
using Electronic_Application.Features.Auth.Commands.RefreshToken;
using Electronic_Application.Features.Auth.Commands.Registration;
using Electronic_Application.Features.Category.Commands.CreateCategory;
using Electronic_Application.Features.Category.Queries.GetAllCategories;
using Electronic_Application.Features.Order.Commands.CreateOrder;
using Electronic_Application.Features.Order.Queries.GetOrderItemsById;
using Electronic_Application.Features.Product.Commands.CreateProduct;
using Electronic_Application.Features.Product.Commands.UpdateProduct;
using Electronic_Application.Features.Product.Queries.GetAllProducts;
using Electronic_Application.Features.Product.Queries.GetProductById;
using Electronic_Application.Models.Auth;
using Electronic_Domain.Entities;

namespace Electronic_Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ForMember(i => i.Category, opts => opts.MapFrom(i => i.Category.Name)).ReverseMap();
            CreateMap<Product, ProductDto>().ForMember(i => i.Category, opts => opts.MapFrom(i => i.Category.Name)).ReverseMap();
            CreateMap<LoginUserCommand, LoginUser>().ForMember(i => i.UserName, opts => opts.MapFrom(i => i.Email));
            CreateMap<RegisterUserCommand, RegisterUserModel>();
            CreateMap<RefreshTokenCommand, RefreshTokenDto>();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();

            // OrderItemDto to OrderItem mapping
            CreateMap<OrderItemDto, OrderItem>()
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.Ignore())              
                .ReverseMap();

            // ShippingDetailDto to ShippingDetail mapping
            CreateMap<ShippingDetailDto, ShippingDetail>()
                .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            // OrderDto to Order mapping
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.OrderTotal))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.Products, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Order, CreateOrderCommand>()
                     .ForPath(dest => dest.Order, opt => opt.MapFrom(src => src))
                     .ForPath(dest => dest.Order.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                     .ForPath(dest => dest.Order.OrderTotal, opt => opt.MapFrom(src => src.Total))
                     .ReverseMap();

            CreateMap<OrderItem, OrderItemListDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.Image))
                .ReverseMap();

            //// OrderItemDto to OrderItem mapping
            //CreateMap<OrderItemDto, OrderItem>()
            //    .ForMember(dest => dest.Order, opt => opt.Ignore())
            //    .ForMember(dest => dest.Product, opt => opt.Ignore())
            //    //.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //    //.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            //    //.ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
            //    .ReverseMap();

            //// OrderDto to Order mapping
            //CreateMap<OrderDto, Order>()
            //    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.OrderTotal))
            //    .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            //    .ForMember(dest => dest.Products, opt => opt.Ignore())
            //    .ReverseMap();

            //// ShippingDetailDto to ShippingDetail mapping
            //CreateMap<ShippingDetailDto, ShippingDetail>()
            //    .ForMember(dest => dest.RecipientName, opt => opt.MapFrom(src => src.Name))
            //    //.ForMember(dest => dest.AddressLine1, opt => opt.MapFrom(src => src.AddressLine1))
            //    //.ForMember(dest => dest.AddressLine2, opt => opt.MapFrom(src => src.AddressLine2))
            //    //.ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            //    //.ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            //    //.ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            //    //.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
            //    .ReverseMap();

            //  CreateMap<Order, CreateOrderCommand>().ReverseMap();

        }
    }
}
