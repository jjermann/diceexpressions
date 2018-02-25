using System;
using System.Collections.Generic;
using System.Linq;
using Generic.Math;
using PType = System.Double;

namespace DiceExpressions.Model
{
    public class UniformDensity<T> : Density<T>
    {
        protected override string Name => "UniformDensity";

        public UniformDensity(params T[] keys) : base(GetUniformDensityDict(keys)) { }

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
        protected override string Name => $"Constant({Key})";
        public T Key => Keys.Single();

        public Constant(T v) : base(v) { }
    }

    public class Zero<T> : Constant<T>
    {
        protected override string Name => "Zero";
        public Zero() : base(GenericMath<T>.Zero) { }
    }

    public class One<T> : Constant<T>
    {
        protected override string Name => "One";
        public One() : base((T)(dynamic)1) { }
    }

    public class Die : UniformDensity<int>
    {
        protected override string Name => $"d{Sides}";
        public int Sides { get; }
        public Die(int n) : base(Enumerable.Range(1, n).ToArray()) {
            Sides = n;
        }
    }
}