using MediatR;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Application.DTO;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;

public sealed record GetAllCustomerQuery : IRequest<Response<IEnumerable<CustomerDto>>>;