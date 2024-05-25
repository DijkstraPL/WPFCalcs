using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_Data.Sections
{
    public class SectionProperties : Section
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area">A in cm2</param>
        /// <param name="momentOfInteria">I in cm4</param>
        public SectionProperties(double area, double momentOfInteria) : base()
        {
            Area = area > 0 ? area : throw new ArgumentOutOfRangeException(nameof(area));
            MomentOfInteria = momentOfInteria > 0 ? momentOfInteria 
                : throw new ArgumentOutOfRangeException(nameof(momentOfInteria));
        }

        public override void SetSectionProperties()
        {
        }
    }
}
