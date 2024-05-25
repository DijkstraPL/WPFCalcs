using Build_IT_CommonTools.Attributes;

namespace Build_IT_Data.Sections.Interfaces
{
    public interface IMomentOfInteria
    {
        [Abbreviation("I")]
        [Unit("cm4")]
        double MomentOfInteria { get; }
    }
}
