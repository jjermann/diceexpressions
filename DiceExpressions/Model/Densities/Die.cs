using System;
using System.Collections.Generic;
using System.Linq;
using DiceExpressions.Model.AlgebraicDefaultImplementations;
using DiceExpressions.Model.AlgebraicStructure;
using PType = System.Double;

namespace DiceExpressions.Model.Densities
{
    public class Density<M> :
        Density<FieldType<M,PType>, M, PType>
    {
        public Density(Density<M> density) : base(density) { }
        public Density(IDictionary<M, PType> dict, string name = null) : base(dict, new FieldType<M,PType>(), new RealFieldType<PType>(), name) { }
    }

    public class UniformDensity<M> :
        UniformDensity<FieldType<M,PType>, M, PType>
    {
        public UniformDensity(params M[] keys) : base(new FieldType<M,PType>(), new RealFieldType<PType>(), keys) { }
    }
    public class UniformDensity<G, M, RF> :
        Density<G, M, RF>
    {
        public UniformDensity(G baseStructure, IRealField<RF> realField, params M[] keys) : base(GetUniformDensityDict(realField, keys), baseStructure, realField, "UniformDensity") { }

        protected static IDictionary<M, RF> GetUniformDensityDict(IRealField<RF> realField, params M[] keys)
        {
            var distinctKeys = keys.Distinct().ToList();
            var count = distinctKeys.Count;
            if (count != keys.Count())
            {
                throw new NotImplementedException();
            }

            var uniformDict = distinctKeys.ToDictionary(l => l, l => realField.Divide(realField.One(), realField.EmbedFrom(count)));
            return uniformDict;
        }
    }

    public class Constant<M> :
        Constant<FieldType<M,PType>, M, PType>
    {
        public Constant(M v) : base(new FieldType<M,PType>(), new RealFieldType<PType>(), v) { }
        public static implicit operator Constant<M>(M t)
        {
            return new Constant<M>(t);
        }
    }
    public class Constant<G, M, RF> :
        UniformDensity<G, M, RF>
    {
        public M Key => this.GetKeys().Single();

        public Constant(G baseStructure, IRealField<RF> realField, M v) : base(baseStructure, realField, v)
        {
            Name = $"{v}";
        }
    }

    public class Zero<M> :
        Zero<FieldType<M,PType>, M, PType>
    {
        public Zero() : base(new FieldType<M,PType>(), new RealFieldType<PType>()) { }
    }
    public class Zero<G, M, RF> :
        Constant<G, M, RF>
        where G :
            IAdditiveMonoid<M>
    {
        public Zero(G baseStructure, IRealField<RF> realField) : base(baseStructure, realField, baseStructure.Zero()) { }
    }

    public class One<M> :
        One<FieldType<M,PType>, M, PType>
    {
        public One() : base(new FieldType<M,PType>(), new RealFieldType<PType>()) { }
    }
    public class One<G, M, RF> :
        Constant<G, M, RF>
        where G :
            IMultiplicativeMonoid<M>
    {
        public One(G baseStructure, IRealField<RF> realField) : base(baseStructure, realField, baseStructure.One()) { }
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