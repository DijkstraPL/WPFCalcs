using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_Data.Models.SnowLoads.Enums
{
    public enum Zones
    {
        None = 0,
        FirstZone = 1,
        BetweenFirst_Second = 3,
        SecondZone = 2,
        BetweenSecond_Third = 6,
        ThirdZone = 4,
        BetweenThird_Fourth = 12,
        FourthZone = 8,
        BetweenThird_Fifth = 24,
        FifthZone = 16
    }
}
