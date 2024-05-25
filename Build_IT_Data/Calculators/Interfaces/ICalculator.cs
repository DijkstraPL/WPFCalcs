using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_Data.Calculators.Interfaces
{
    public interface ICalculator
    {
        string Description { get; }
        IResult Result { get; }

        void Map(IList<object> args);
        IResult Calculate();
    }
}
