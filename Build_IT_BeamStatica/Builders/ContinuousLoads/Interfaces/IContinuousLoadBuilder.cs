using Build_IT_BeamStatica.Data;

namespace Build_IT_BeamStatica.Builders.ContinuousLoads
{
    public interface IContinuousLoadBuilder
    {
        #region Public_Methods
        
        ContinuousLoadData Build(SpanData spanData);
        bool Validate();

        #endregion // Public_Methods
    }
}