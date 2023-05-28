using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Counter
    {
        public readonly double start;
        public readonly double end;
        public readonly double step;
        public double Value { get; private set; }
        public Counter(
            double start = 0,
            double end = double.MaxValue,
            double step = 1)
        {
            Value = start;
            this.start = start;
            this.end = end;
            this.step = step;
        }
        public bool Increase()
        {
            Value =
                Value + step < end ?
                Value + step :
                end;
            return Value == end ? true : false;
        }
        public bool Decrease()
        {
            Value =
                Value - step > start ?
                Value - step : start;
            return Value == start ? true : false;
        }
        public void Reset()
        {
            Value = start;
        }
    }
}
