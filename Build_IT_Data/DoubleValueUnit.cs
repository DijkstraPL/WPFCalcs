using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_CommonTools
{
    public class DoubleValueUnit
    {
        public string Name { get; }
        public string Symbol { get; }
        public double Value { get; set; }
        public string Unit { get; }

        public DoubleValueUnit(string name, string symbol, double value, string unit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));

            Name = name;
            Symbol = symbol;
            Value = value;
            Unit = unit;
        }
    }
}
