using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Math;
using PType = System.Double;

namespace DiceExpressions.Model
{
    public class UniformDensity<T> : Density<T>
    {
        public UniformDensity(params T[] keys) : base(GetUniformDensityDict(keys), "UniformDensity") { }

        protected static IDictionary<T, PType> GetUniformDensityDict(params T[] keys)
        {
            var distinctKeys = keys.Distinct().ToList();
            var count = distinctKeys.Count;
            if (count != keys.Count())
            {
                throw new NotImplementedException();
            }

            var uniformDict = distinctKeys.ToDictionary(l => l, l => ((PType)1) / count);
            return uniformDict;
        }
    }

    public class Constant<T> : UniformDensity<T>
    {
        public T Key => Keys.Single();

        public Constant(T v) : base(v)
        {
            Name = $"{v}";
        }
    }

    public class Zero<T> : Constant<T>
    {
        public Zero() : base(GenericMath<T>.Zero)
         {
             Name = $"{GenericMath<T>.Zero}";
         }
    }

    public class One<T> : Constant<T>
    {
        public One() : base((T)(dynamic)1)
        {
            Name = $"{(T)(dynamic)1}";
        }
    }

    public class Die : UniformDensity<int>
    {
        public int Sides { get; }
        public Die(int n) : base(Enumerable.Range(1, n).ToArray())
        {
            Name = $"d{n}";
            Sides = n;
        }
    }
}