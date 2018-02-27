using System;
using PType = System.Double;

namespace DiceExpressions.Model
{
    public class DensityExpressionResult<T>
    {
        public Density<T> Density { get; set; }
        public PType? Probability { get; set; }
        public string ErrorString { get; set; }
    }
}