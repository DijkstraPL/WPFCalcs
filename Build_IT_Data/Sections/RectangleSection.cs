using Build_IT_CommonTools.Attributes;
using Build_IT_Data.Sections.Additional;
using System;

namespace Build_IT_Data.Sections
{
    public class RectangleSection : Section
    {
        [Abbreviation("b")]
        [Unit("mm")]
        public double Width { get; }
        [Abbreviation("h")]
        [Unit("mm")]
        public double Height { get; }

        public override double SolidHeight => Height;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">mm</param>
        /// <param name="height">mm</param>
        public RectangleSection(double width, double height)
        {
            Width = width > 0 ? width : throw new ArgumentOutOfRangeException(nameof(width));
            Height = height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height));

            SetPoints();

            SetSectionProperties();
        }

        protected override void CalculateCimcuference()
        {
            Circumference = (2 * Width + 2 * Height) / 10;
        }

        protected override void CalculateArea()
        {
            Area = Width * Height / 100;
        }

        protected override void CalculateCentroid()
        {
            Centroid = new Point(Width / 2, Height / 2);
        }

        /// <summary>
        /// Divided by 10000 - mm4 to cm4
        /// </summary>
        protected override void CalculateMomentOfInteria()
        {
            MomentOfInteria = Width * Math.Pow(Height, 3) / 12 / 10000;
        }

        private void SetPoints()
        {
            Points.Add(new Point(0, 0));
            Points.Add(new Point(Width, 0));
            Points.Add(new Point(Width, Height));
            Points.Add(new Point(0, Height));
        }
    }
}
