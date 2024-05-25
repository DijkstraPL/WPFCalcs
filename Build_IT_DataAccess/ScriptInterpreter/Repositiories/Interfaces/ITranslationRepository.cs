namespace Build_IT_DataAccess.ScriptInterpreter.Repositiories.Interfaces
{
    public interface ITranslationRepository : IScriptTranslationRepository, 
        IParameterTranslationRepository, IValueOptionTranslationRepository,
        IGroupTranslationRepository
    {
    }
}
