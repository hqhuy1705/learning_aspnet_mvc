using System;
using System.Collections;
using System.Collections.Generic;
using WebAPI.Entity;

namespace WebAPI.Test.TestHelper
{
    public class CategoryComparer : IComparer, IComparer<Category>
    {
        public int Compare(object expected, object actual)
        {
            if (!(expected is Category lhs) || !(actual is Category rhs)) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Category expected, Category actual)
        {
            int temp;
            return (temp = expected.Id.CompareTo(actual.Id)) != 0 ? temp : expected.DisplayName.CompareTo(actual.DisplayName);
        }
    }
}
