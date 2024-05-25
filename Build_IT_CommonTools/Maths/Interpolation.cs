using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CommonTools.Maths
{
    public static class Interpolation
    {
        public static double InterpolateLinearBetween(
            (double position, double value) start,
            (double position, double value) end,
            double at)
            => start.value
            + (at - start.position)
            * (end.value - start.value)
            / (end.position - start.position);
    }
}
