using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using DiceExpressions.Model.AlgebraicStructureHelper;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public class UniformDensity<M> :
        UniformDensity<FieldType<M>, M>
    {
        public UniformDensity(params M[] keys) : base(keys) { }
    }
    public class UniformDensity<G, M> :
        Density<G, M>
        where G :
            IEqualityComparer<M>,
            new()
    {
        public UniformDensity(params M[] keys) : base(GetUniformDensityDict(keys), "UniformDensity") { }

        protected static IDictionary<M, PType> GetUniformDensityDict(params M[] keys)
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

    public class Constant<M> :
        Constant<FieldType<M>, M>
    {
        public Constant(M v) : base(v) { }
        public static implicit operator Constant<M>(M t)
        {
            return new Constant<M>(t);
        }
    }
    public class Constant<G, M> :
        UniformDensity<G, M>
        where G :
            IEqualityComparer<M>,
            new()
    {
        public M Key => Keys.Single();

        public Constant(M v) : base(v)
        {
            Name = $"{v}";
        }
    }

    public class Zero<M> :
        Zero<FieldType<M>, M>
    {
        public Zero() : base() { }
    }
    public class Zero<G, M> :
        Constant<G, M>
        where G :
            IAdditiveMonoid<M>,
            new()
    {
        public Zero() : base(AlgebraicStructure.Zero()) { }
    }

    public class One<M> :
        One<FieldType<M>, M>
    {
        public One() : base() { }
    }
    public class One<G, M> :
        Constant<G, M>
        where G :
            IMultiplicativeMonoid<M>,
            new()
    {
        public One() : base(AlgebraicStructure.One()) { }
    }

    public class Die :
        UniformDensity<int>
    {
        public int Sides { get; }
        public Die(int n) : base(Enumerable.Range(1, n).ToArray())
        {
            Name = $"d{n}";
            Sides = n;
        }
    }
}