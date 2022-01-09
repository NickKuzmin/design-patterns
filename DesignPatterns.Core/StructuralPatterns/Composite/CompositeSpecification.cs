using System.Linq;

namespace DesignPatterns.Core.StructuralPatterns.Composite
{
    public abstract class Specification<T>
    {
        public abstract bool IsSatisfied(T p);

        public static Specification<T> operator &(
            Specification<T> first, Specification<T> second)
        {
            return new AndSpecification<T>(first, second);
        }
    }

    public abstract class CompositeSpecification<T> : Specification<T>
    {
        protected readonly Specification<T>[] Items;

        protected CompositeSpecification(params Specification<T>[] items)
        {
            Items = items;
        }
    }

    // combinator
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        public AndSpecification(params Specification<T>[] items) : base(items)
        {
        }

        public override bool IsSatisfied(T t)
        {
            // Any -> OrSpecification
            return Items.All(i => i.IsSatisfied(t));
        }
    }
}
