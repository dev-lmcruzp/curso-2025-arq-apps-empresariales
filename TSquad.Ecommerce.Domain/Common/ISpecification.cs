namespace TSquad.Ecommerce.Domain.Common;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T entity);
}