using System;
using prep.utility;

namespace prep.collections
{
  public class ComparableCriteriaFactory<ItemToMatch,PropertyType> where PropertyType : IComparable<PropertyType>
  {

    Func<ItemToMatch, PropertyType> accessor;


    public ComparableCriteriaFactory(Func<ItemToMatch, PropertyType> accessor)
    {
      this.accessor = accessor;
    }

    public IMatchAn<ItemToMatch> greater_than(PropertyType value)
    {
      return new AnonymousMatch<ItemToMatch>(x => new IsGreaterThan<PropertyType>(value).matches(accessor(x)));
    }

    public IMatchAn<ItemToMatch> between(PropertyType start,PropertyType end)
    {
      return new AnonymousMatch<ItemToMatch>(x =>
                                               accessor(x).CompareTo(start) >= 0 &&
                                                 accessor(x).CompareTo(end) <= 0);
    }
      public IMatchAn<ItemToMatch> equal_to(PropertyType value)
      {
          return new CriteriaFactory<ItemToMatch, PropertyType>(x => accessor(x)).equal_to(value);
      }

      public IMatchAn<ItemToMatch> equal_to_any(params PropertyType[] values)
      {
          return new CriteriaFactory<ItemToMatch, PropertyType>(x => accessor(x)).equal_to_any(values);
      }
      public IMatchAn<ItemToMatch> not_equal_to(PropertyType value)
      {
          return new CriteriaFactory<ItemToMatch, PropertyType>(x => accessor(x)).not_equal_to(value);
      }

  }
}