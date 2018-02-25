using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Math;

namespace DiceExpressions.Model
{
    public static class GenericMathExtension
    {
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