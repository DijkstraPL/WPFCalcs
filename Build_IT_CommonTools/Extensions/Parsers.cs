using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CommonTools.Extensions
{
    public static class Parsers
    {
        public static double ToRadians(this double degrees)
            => degrees * Math.PI / 180;
    }
}
