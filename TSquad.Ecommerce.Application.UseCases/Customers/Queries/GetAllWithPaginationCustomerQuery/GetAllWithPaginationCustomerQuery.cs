using MediatR;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery;

public sealed record GetAllWithPaginationCustomerQuery(int PageNumber, int PageSize) : IRequest<ResponsePagination<IEnumerable<CustomerDto>>>;