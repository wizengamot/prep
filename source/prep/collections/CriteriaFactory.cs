using System;
using System.Collections.Generic;
using prep.utility;

namespace prep.collections
{
  public class CriteriaFactory<ItemToMatch, PropertyType> : ICreateMatchers<ItemToMatch, PropertyType>
  {
    Func<ItemToMatch, PropertyType> accessor;

    public CriteriaFactory(Func<ItemToMatch, PropertyType> accessor)
    {
      this.accessor = accessor;
    }

    public IMatchAn<ItemToMatch> equal_to(PropertyType value)
    {
      return equal_to_any(value);
    }

    public IMatchAn<ItemToMatch> equal_to_any(params PropertyType[] values)
    {
      return MatchFactory<ItemToMatch>.AnonymousMatchWith(x => new List<PropertyType>(values).Contains(accessor(x)));
    }

    public IMatchAn<ItemToMatch> not_equal_to(PropertyType value)
    {
      return new NegatingMatch<ItemToMatch>(equal_to(value));
    }

  }
}