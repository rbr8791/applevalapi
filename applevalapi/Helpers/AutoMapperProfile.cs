using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using applevalApi.DTO.MapperObjects;
using applevalApi.Entities;

namespace applevalApi.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<User, UserDTO>();
            //CreateMap<UserDTO, User>();
            //CreateMap<ActiveToken, ActiveTokenDTO>();
            //CreateMap<ActiveTokenDTO, ActiveToken>();

            CreateMap<ActiveToken, ActiveTokenDto>();
            CreateMap<ActiveTokenDto, ActiveToken>();

            CreateMap<AuditLog, AuditLogDto>();
            CreateMap<AuditLogDto, AuditLog>();

            CreateMap<AuditLogPurchase, AuditLogPurchaseDto>();
            CreateMap<AuditLogPurchaseDto, AuditLogPurchase>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();

            CreateMap<InvoiceDetail, InvoiceDetailDto>();
            CreateMap<InvoiceDetailDto, InvoiceDetail>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();


            CreateMap<OrderDetail, OrderDetailDto>();
            CreateMap<OrderDetailDto, OrderDetail>();


            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();


            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();


            CreateMap<Stock, StockDto>();
            CreateMap<StockDto, Stock>();


            CreateMap<StockMovement, StockMovementDto>();
            CreateMap<StockMovementDto, StockMovement>();


            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();


            //CreateMap<>();
            //CreateMap<>();


        }
    }
}
