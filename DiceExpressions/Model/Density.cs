using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Generic.Math;
using OxyPlot;
using PType = System.Double;

namespace DiceExpressions.Model
{
    //operators make several assumptions on T
    //Expected, Variance, Stdev, Cdf assume that T can be converted to PType
    public class Density<T> : IEquatable<Density<T>>
    {
        private IDictionary<T, PType> _densityDict;

        public Density(Density<T> density) : this(density._densityDict, density.Name) { }

        public Density(IDictionary<T, PType> dict, string name = null)
        {
            _densityDict = dict;
            Name = name ?? "Density";
        }

        public string Name { get; set; }
        public string TrimmedName {
            get {
                var trimmedName = Name;
                while(trimmedName.StartsWith("(") && trimmedName.EndsWith(")"))
                {
                    trimmedName = trimmedName.Substring(1, Math.Max(1, trimmedName.Length-2));
                }
                return trimmedName;
            }
        }
        public PType this[T key] => _densityDict[key];
        public IList<T> Keys => _densityDict.Keys.OrderBy(k => k).ToList();
        public IList<PType> Values => Keys.Select(k => _densityDict[k]).ToList();

        public bool Equals(Density<T> other)
        {
            var isNull = object.ReferenceEquals(other, null);
            if (isNull)
            {
                return false;
            }
            var countsDiffer = Keys.Count != other.Keys.Count;
            if (countsDiffer)
            {
                return false;
            }
            var keysDiffer = Keys.Except(other.Keys).Any();
            if (keysDiffer)
            {
                return false;
            }
            var valuesDiffer = Values.Except(other.Values).Any();
            if (valuesDiffer)
            {
                return false;
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            var dObj = obj as Density<T>;
            return Equals(dObj);
        }
        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                foreach (var key in Keys)
                {
                    hash *= 397;
                    hash ^= key.GetHashCode();
                    hash *= 397;
                    hash ^= this[key].GetHashCode();
                }
            }
            return hash;
        }

        public static implicit operator Density<T>(T t)
        {
            return new Constant<T>(t);
        }

        public static implicit operator Density<PType>(Density<T> d)
        {
            var newDict = d._densityDict.ToDictionary(p => GenericMath.Convert<T, PType>(p.Key), p => p.Value);
            return new Density<PType>(newDict, d.Name);
        }

        public Density<U> Op<U>(Func<T, U> op, Func<string, string> opStrFunc = null)
        {
            var resDensity = new Dictionary<U, PType>();
            foreach (var key in Keys)
            {
                var resKey = op(key);
                if (!resDensity.ContainsKey(resKey))
                {
                    resDensity[resKey] = default(PType);
                }
                resDensity[resKey] += this[key];
            }
            var name = opStrFunc == null
                ? "Density"
                : opStrFunc(Name);
            return new Density<U>(resDensity, name);
        }
        // public static Density<T> operator +(Density<T> d)
        // {
        //     return d.Op<T>(a => +(dynamic)a, s => $"(+{s})");
        // }
        public static Density<T> operator -(Density<T> d)
        {
            return d.Op<T>(a => GenericMath.Negate(a), s => $"(-{s})");
        }
        // public static Density<T> operator !(Density<T> d)
        // {
        //     return d.Op<T>(a => !(dynamic)a, s => $"(!{s})");
        // }
        public static Density<T> operator ~(Density<T> d)
        {
            return d.Op<T>(a => GenericMath.Not(a), s => $"(~{s})");
        }
        // public static Density<T> operator <<(Density<T> d, int n)
        // {
        //     return d.Op<T>(a => (dynamic)a << n, s => $"({s}<<{n})");
        // }
        // public static Density<T> operator >>(Density<T> d, int n)
        // {
        //     return d.Op<T>(a => (dynamic)a >> n, s => $"({s}>>{n})");
        // }
        public Density<T> Abs()
        {
            return Op<T>(a => GenericMathExtension.Abs(a), s => $"|{s}|");
        }

        public static Density<U> BinaryOp<U>(
            Density<T> d1,
            Density<T> d2,
            Func<T, T, U> op, Func<string,string,string> binOpStrFunc = null)
        {
            var resDensity = new Dictionary<U, PType>();
            foreach (var key1 in d1.Keys)
            {
                foreach (var key2 in d2.Keys)
                {
                    var resKey = op(key1, key2);
                    if (!resDensity.ContainsKey(resKey))
                    {
                        resDensity[resKey] = default(PType);
                    }
                    resDensity[resKey] += d1[key1]*d2[key2];
                }
            }
            var name = binOpStrFunc == null
                ? "Density"
                : binOpStrFunc(d1.Name, d2.Name);
            return new Density<U>(resDensity, name);
        }
        public static Density<T> operator +(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.Add(a,b), (s,t) => $"({s}+{t})");
        }
        public static Density<T> operator -(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.Subtract(a,b), (s,t) => $"({s}-{t})");
        }
        public static Density<T> operator *(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.Multiply(a,b), (s,t) => $"({s}*{t})");
        }
        public static Density<T> operator /(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.Divide(a,b), (s,t) => $"({s}/{t})");
        }
        // public static Density<T> operator %(Density<T> d1, Density<T> d2)
        // {
        //     return BinaryOp<T>(d1, d2, (a,b) => (dynamic)a % (dynamic)b, (s,t) => $"({s}%{t})");
        // }
        public static Density<T> operator &(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.And(a,b), (s,t) => $"({s}&{t})");
        }
        public static Density<T> operator |(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.Or(a,b), (s,t) => $"({s}|{t})");
        }
        public static Density<T> operator ^(Density<T> d1, Density<T> d2)
        {
            return BinaryOp<T>(d1, d2, (a,b) => GenericMath.Xor(a,b), (s,t) => $"({s}^{t})");
        }

        private static IEnumerable<IEnumerable<U>> CartesianProduct<U>(IEnumerable<IEnumerable<U>> sequences)
        {
            IEnumerable<IEnumerable<U>> emptyProduct = new[] { Enumerable.Empty<U>() };
            var res = sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) =>
                    from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(new[] {item})
                );
            return res;
        }
        public static Density<U> MultiOp<U>(
            IEnumerable<Density<T>> densityList,
            Func<IEnumerable<T>,U> multiOp,
            Func<IEnumerable<string>,string> multiOpStrFunc = null)
        {
            var resDensity = new Dictionary<U, PType>();
            var dListList = densityList.Select(d => d._densityDict.ToList());
            var productList = CartesianProduct(dListList);

            foreach (var product in productList)
            {
                var multiOpArgument = product.Select(p => p.Key).ToList();
                var resultKey = multiOp(multiOpArgument);
                var resultValue = GenericMathExtension.Product(product.Select(p => p.Value));
                if (!resDensity.ContainsKey(resultKey))
                {
                    resDensity[resultKey] = default(PType);
                }
                resDensity[resultKey] += resultValue;
            }

            var name = multiOpStrFunc == null
                ? "Density"
                : multiOpStrFunc(densityList.Select(d => d.Name));
            return new Density<U>(resDensity, name);
        }

        public MultiDensity<T> AsMultiDensity(int n)
        {
            var dList = Enumerable.Repeat(this, n).ToList();
            var multiDensity = new MultiDensity<T>(dList);
            return multiDensity;
        }

        public PType Prob(Func<T, bool> cond)
        {
            var resSum = Keys.Where(cond).Select(k => this[k]).Sum();
            return resSum;
        }
        public static PType BinaryProb(Density<T> d1, Density<T> d2, Func<T, T, bool> cond)
        {
            var resSum = default(PType);
            foreach (var key1 in d1.Keys)
            {
                foreach (var key2 in d2.Keys)
                {
                    if (cond(key1, key2))
                    {
                        resSum += d1[key1]*d2[key2];
                    }
                }
            }
            return resSum;
        }
        public static PType operator ==(Density<T> d1, Density<T> d2)
        {
            return BinaryProb(d1, d2, (a,b) => GenericMath.Equal(a,b));
        }
        public static PType operator !=(Density<T> d1, Density<T> d2)
        {
            return BinaryProb(d1, d2, (a,b) => GenericMath.NotEqual(a,b));
        }
        public static PType operator <(Density<T> d1, Density<T> d2)
        {
            return BinaryProb(d1, d2, (a,b) => GenericMath.LessThan(a,b));
        }
        public static PType operator >(Density<T> d1, Density<T> d2)
        {
            return BinaryProb(d1, d2, (a,b) => GenericMath.GreaterThan(a,b));
        }
        public static PType operator <=(Density<T> d1, Density<T> d2)
        {
            return BinaryProb(d1, d2, (a,b) => GenericMath.LessThanOrEqual(a,b));
        }
        public static PType operator >=(Density<T> d1, Density<T> d2)
        {
            return BinaryProb(d1, d2, (a,b) => GenericMath.GreaterThanOrEqual(a,b));
        }

        protected Density<T> ArithMult(int n)
        {
            if (n<0)
            {
                throw new ArgumentException();
            }
            var dict = new Dictionary<T, PType>();
            if (n == 0)
            {
                return new Zero<T>();
            }
            var d = this;
            foreach(var step in Enumerable.Range(0, n-1))
            {
                d += this;
            }
            d.Name = $"({TrimmedName}).ArithMult({n})";
            return d;
        }

        public Density<T> ArithMult(Density<int> d)
        {
            var resDict = new Dictionary<T, PType>();
            foreach (var key in d.Keys)
            {
                var mulDensity = ArithMult(key);
                foreach (var mulKey in mulDensity.Keys)
                {
                    if (!resDict.ContainsKey(mulKey))
                    {
                        resDict[mulKey] = default(PType);
                    }
                    resDict[mulKey] += mulDensity[mulKey]*d[key];
                }
            }
            var newDensity = new Density<T>(resDict, $"{TrimmedName}.ArithMult({d.TrimmedName})");
            return newDensity;
        }

        public PType Expected()
        {
            return GenericMathExtension.Sum(
                _densityDict.Select(p => GenericMath.MultiplyAlternative(p.Value, p.Key)));
        }
        public PType Variance()
        {
            var expected = Expected();
            return GenericMathExtension.Sum(
                _densityDict.Select(p =>
                {
                    var dist = GenericMath.SubtractAlternative(expected, p.Key);
                    return GenericMath.MultiplyAlternative(p.Value, GenericMath.Multiply(dist, dist));
                }));
        }
        public PType Stdev()
        {
            return GenericMathExtension.Sqrt(Variance());
        }
        public Density<T> ConditionalDensity(Func<T, bool> cond, Func<string,string> condStrFunc = null)
        {
            var resDict = new Dictionary<T, PType>();
            foreach (var key in Keys.Where(cond))
            {
                resDict[key] = this[key];
            }
            var conditionalProbability = resDict.Values.Sum();
            foreach (var key in resDict.Keys)
            {
                resDict[key] /= conditionalProbability;
            }
            var name = condStrFunc == null
                ? "Density"
                : condStrFunc(Name);

            var newDensity = new Density<T>(resDict, name);
            return newDensity;
        }
        public PType Cdf(PType x)
        {
            return Prob(a => GenericMath.LessThanOrEqual(GenericMath.Convert<T, PType>(a),x));
        }

        public Density<T> WithAdvantage(int n = 1)
        {
            if (n < 0)
            {
                throw new NotImplementedException();
            }
            if (n == 0)
            {
                return this;
            }
            if (n == 1)
            {
                return BinaryOp<T>(this, this, (a,b) => GenericMathExtension.Max(a,b), (s,t) => $"a{s}");
            }
            var mDensity = AsMultiDensity(n).MultiOp(e => GenericMathExtension.Max<T>(e), e => $"a{n}{e.First()}");
            return mDensity;
        }

        public Density<T> WithDisadvantage(int n = 1)
        {
            if (n < 0)
            {
                throw new NotImplementedException();
            }
            if (n == 0)
            {
                return this;
            }
            if (n == 1)
            {
                return BinaryOp<T>(this, this, (a,b) => GenericMathExtension.Min(a,b), (s,t) => $"d{s}");
            }
            var mDensity = AsMultiDensity(n).MultiOp(e => GenericMathExtension.Min<T>(e), e => $"d{n}{e.First()}");
            return mDensity;
        }

        public override string ToString()
        {
            return Plot();
        }

        public string Plot(int plotWidth = 70)
        {
            var maxPercentage = GenericMathExtension.Max<PType>(Values.ToArray());
            return AsciiPlotter<T>.GetPlot(
                k => this[k],
                Keys,
                plotWidth:plotWidth,
                minP:GenericMath<PType>.Zero,
                maxP:maxPercentage,
                asPercentage:true);
        }

        public PlotModel OxyPlot(string title = null)
        {
            title = title ?? TrimmedName;
            var subtitle = string.Format(
                CultureInfo.InvariantCulture,
                "Expected = {0:0.0000}, Stdev = {1:0.00}",
                Expected(),
                Stdev()
            );
            var plotModel = OxyPlotter<T>.GetPlot(
                k => this[k],
                Keys,
                title: title,
                subtitle: subtitle,
                yMin: 0.0,
                valueLabelFormatter: p => p.ToString("P", CultureInfo.InvariantCulture));
            return plotModel;
        }

        public string GetOxyPlotSvg(string title)
        {
            var oxyplot = OxyPlot(title);
            var svgExporter = new SvgExporter { Width = 600, Height = 400 };
            var res = svgExporter.ExportToString(oxyplot);
            return res;
        }

        public void SaveOxyPlotPdf(string filename = null)
        {
            filename = filename ?? TrimmedName + ".pdf";
            var oxyplot = OxyPlot(Path.GetFileName(filename));
            using (var stream = File.Create(filename))
            {
                var pdfExporter = new PdfExporter { Width = 600, Height = 400 };
                pdfExporter.Export(oxyplot, stream);
            }
        }
    }
}