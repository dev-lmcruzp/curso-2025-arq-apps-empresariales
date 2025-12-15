using MediatR;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;

public sealed record DeleteCustomerCommand(string CustomerId) : IRequest<Response<bool>>;