using Build_IT_Data.Sections.Additional;
using Build_IT_CommonTools;
using System;
using Build_IT_CommonTools.Attributes;

namespace Build_IT_Data.Sections
{
    public class IBeamSection : Section
    {
        [Abbreviation("b")]
        [Unit("mm")]
        public double Width { get; }
        [Abbreviation("h")]
        [Unit("mm")]
        public double Height { get; }

        [Abbreviation("t_f_")]
        [Unit("mm")]
        public double FlangeWidth { get; }
        [Abbreviation("t_w_")]
        [Unit("mm")]
        public double WebWidth { get; }
        [Abbreviation("r")]
        [Unit("mm")]
        public double Radius { get; }

        public override double SolidHeight => Height;

        public IBeamSection(double width, double height,
            double flangeWidth, double webWidth, double radius)
        {
            Width = width > 0 ? width : throw new ArgumentOutOfRangeException(nameof(width));
            Height = height > 0 ? height : throw new ArgumentOutOfRangeException(nameof(height));
            FlangeWidth = flangeWidth > 0 ? flangeWidth : throw new ArgumentOutOfRangeException(nameof(flangeWidth));
            WebWidth = webWidth > 0 ? webWidth : throw new ArgumentOutOfRangeException(nameof(webWidth));
            Radius = radius >= 0 ? radius : throw new ArgumentOutOfRangeException(nameof(radius));

            SetPoints();

            SetSectionProperties();
        }

        protected override void CalculateCimcuference()
        {
            Circumference = (2 * Width
                + 2 * (Height - 2 * FlangeWidth - 2 * Radius)
                + 4 * FlangeWidth
                + 4 * (Width - WebWidth - 2 * Radius) / 2
                + 2 * Math.PI * Radius) / 10;
        }

        protected override void CalculateArea()
        {
            Area = (2 * Width * FlangeWidth
                + (Height - 2 * FlangeWidth) * WebWidth
                + Math.Pow(2 * Radius, 2) - Math.PI * Math.Pow(Radius, 2)) / 100;
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
            MomentOfInteria = (2 * Width * Math.Pow(FlangeWidth, 3) / 12
                + 2 * Width * FlangeWidth * Math.Pow(Height / 2 - FlangeWidth / 2, 2)
                + WebWidth * Math.Pow(Height - 2 * FlangeWidth, 3) / 12 
                + 4 * Math.Pow(Radius, 4) / 12
                + 4 * Math.Pow(Radius, 2) * Math.Pow(Height / 2 - FlangeWidth - Radius / 2, 2)
                - 4 *  Math.Pow(Radius, 4 ) * (1.0 / 16 * Math.PI - 4 / (9 * Math.PI))
                - 4 * Math.PI * Math.Pow(Radius,2) / 4 * Math.Pow(Height / 2 - FlangeWidth - Radius + 4* Radius / (3 * Math.PI),2)
                ) / 10000;
        }

        private void SetPoints()
        {
            Points.Add(new Point(0, 0));
            Points.Add(new Point(Width, 0));
            Points.Add(new Point(Width, FlangeWidth));
            for (int i = 0; i < 7; i++)
            {
                Points.Add(new Point(
                    Width / 2 + WebWidth / 2 + Radius - Radius * Math.Sin(Math.PI * 15 * i / 180),
                    FlangeWidth + Radius - Radius * Math.Cos(Math.PI * 15 * i / 180)
                    ));
            }
            for (int i = 0; i < 7; i++)
            {
                Points.Add(new Point(
                    Width / 2 + WebWidth / 2 + Radius - Radius * Math.Cos(Math.PI * 15 * i / 180),
                    Height - FlangeWidth - Radius + Radius * Math.Sin(Math.PI * 15 * i / 180)
                    ));
            }

            Points.Add(new Point(Width, Height - FlangeWidth));
            Points.Add(new Point(Width, Height));
            Points.Add(new Point(0, Height));
            Points.Add(new Point(0, Height - FlangeWidth));

            for (int i = 0; i < 7; i++)
            {
                Points.Add(new Point(
                    Width / 2 - WebWidth / 2 - Radius + Radius * Math.Sin(Math.PI / 180 * 15 * i),
                    Height - FlangeWidth - Radius + Radius * Math.Cos(Math.PI / 180 * 15 * i)
                    ));
            }
            for (int i = 0; i < 7; i++)
            {
                Points.Add(new Point(
                    Width / 2 - WebWidth / 2 - Radius + Radius * Math.Cos(Math.PI / 180 * 15 * i),
                    FlangeWidth + Radius - Radius * Math.Sin(Math.PI / 180 * 15 * i)
                    ));
            }

            Points.Add(new Point(0, FlangeWidth));
        }
    }
}
