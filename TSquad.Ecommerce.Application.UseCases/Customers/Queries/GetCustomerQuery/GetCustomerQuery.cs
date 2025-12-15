using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;

public sealed record GetCustomerQuery(string CustomerId) : IRequest<Response<CustomerDto>>;