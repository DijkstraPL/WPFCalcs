using System.Threading.Tasks;

namespace Build_IT_Infrastructure.Data.Interfaces
{
    public interface IRequest<T> where T : class
    {
        #region Public_Methods
        
        Task<T> Execute();

        #endregion // Public_Methods
    }
}
