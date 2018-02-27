using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Math;

namespace DiceExpressions.Model
{
    public static class GenericMathExtension
    {
        //TODO
        public static T One<T>()
        {
            return (T)(dynamic)(1);
        }
        public static T Sum<T>(params T[] source)
        {
            return Sum<T>(source.ToList());
        }
        public static T Sum<T>(IEnumerable<T> source)
        {
            var sum = GenericMath<T>.Zero;
            foreach (var value in source)
            {
                if (value != null)
                {
                    sum = GenericMath<T>.Add(sum, value);
                }
            }
            return sum;
        }

        public static T Product<T>(params T[] source)
        {
            return Product<T>(source.ToList());
        }
        public static T Product<T>(IEnumerable<T> source)
        {
            var product = One<T>();
            foreach (var value in source)
            {
                if (value != null)
                {
                    product = GenericMath<T>.Multiply(product, value);
                }
            }
            return product;
        }

        public static T Average<T>(params T[] source)
        {
            return Average<T>(source.ToList());
        }
        public static T Average<T>(IEnumerable<T> source)
        {
            var count = 0;
            var sum = GenericMath<T>.Zero;
            foreach (var value in source)
            {
                if (value != null)
                {
                    sum = GenericMath<T>.Add(sum, value);
                    count++;
                }
            }
            return sum;
        }

        public static IEnumerable<T> GetNLargest<T>(IEnumerable<T> source, int count)
        {
            if (count < 0)
            {
                throw new NotImplementedException();
            }
            if (count == 0)
            {
                return Enumerable.Empty<T>();
            }
            var sourceList = source.ToList();
            count = Math.Min(sourceList.Count, count);
            var resSet = sourceList.Take(count).ToList();
            var currentMin = Min<T>(resSet);

            foreach (var t in sourceList.Skip(count))
            {
                if (GenericMath.LessThanOrEqual(t,currentMin))
                {
                    continue;
                }
                resSet.Remove(currentMin);
                resSet.Add(t);
                currentMin = Min<T>(resSet);
            }
            return resSet.OrderByDescending(i => i);
        }

        public static IEnumerable<T> GetNSmallest<T>(IEnumerable<T> source, int count)
        {
            if (count < 0)
            {
                throw new NotImplementedException();
            }
            if (count == 0)
            {
                return Enumerable.Empty<T>();
            }
            var sourceList = source.ToList();
            count = Math.Min(sourceList.Count, count);
            var resSet = sourceList.Take(count).ToList();
            var currentMax = Max<T>(resSet);

            foreach (var value in sourceList.Skip(count))
            {
                if (GenericMath.GreaterThanOrEqual(value,currentMax))
                {
                    continue;
                }
                resSet.Remove(currentMax);
                resSet.Add(value);
                currentMax = Max<T>(resSet);
            }
            return resSet.OrderBy(i => i);
        }

        public static T SumNLargest<T>(IEnumerable<T> source, int count)
        {
            var nlargest = GetNLargest(source, count);
            return Sum(nlargest);
        }

        public static T SumNSmallest<T>(IEnumerable<T> source, int count)
        {
            var nsmallest = GetNSmallest(source, count);
            return Sum(nsmallest);
        }

        public static T Abs<T>(T value)
        {
            return GenericMath<T>.GreaterThanOrEqual(value, GenericMath<T>.Zero)
                ? value
                : GenericMath<T>.Negate(value);
        }

        // public static bool WithinDelta<T>(T input1, T input2, T delta)
        // {
        //     return GenericMath<T>.LessThanOrEqual(Abs(GenericMath<T>.Subtract(input1, input2)), delta);
        // }

        public static T Max<T>(params T[] source)
        {
            return Max<T>(source.ToList());
        }
        public static T Max<T>(IEnumerable<T> source)
        {
            var sourceList = source.ToList();
            var max = sourceList.FirstOrDefault(s => !ReferenceEquals(s, null));
            foreach (var value in sourceList)
            {
                if (value != null && GenericMath<T>.GreaterThan(value, max))
                {
                    max = value;
                }
            }
            return max;
        }

        public static T Min<T>(params T[] source)
        {
            return Min<T>(source.ToList());
        }
        public static T Min<T>(IEnumerable<T> source)
        {
            var sourceList = source.ToList();
            var min = sourceList.FirstOrDefault(s => !ReferenceEquals(s, null));
            foreach (var value in sourceList)
            {
                if (value != null && GenericMath<T>.LessThan(value, min))
                {
                    min = value;
                }
            }
            return min;
        }

        public static T Sqrt<T>(T input)
        {
            return GenericMath.Convert<double, T>(Math.Sqrt(GenericMath.Convert<T, double>(input)));
        }

        public static T Pow<T>(T input, T pow)
        {
            return GenericMath.Convert<double, T>(Math.Pow(GenericMath.Convert<T, double>(input), GenericMath.Convert<T, double>(pow)));
        }

        public static T Pow<T>(T input, int pow)
        {
            return GenericMath.Convert<double, T>(Math.Pow(GenericMath.Convert<T, double>(input), pow));
        }

        public static int Round<T>(T input)
        {
            return (int)(dynamic)(Math.Round((dynamic)input));
        }
    }
}