using Build_IT_CommonTools.Attributes;

namespace Build_IT_Data.Sections.Interfaces
{
    public interface IArea
    {
        [Abbreviation("A")]
        [Unit("cm2")]
        double Area { get; }
    }
}
