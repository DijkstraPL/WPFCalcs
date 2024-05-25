using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_Data.Calculators.Interfaces
{
    public interface IResult
    {
        object this[string name] { get; set; }
        IDictionary<string, string> Descriptions { get; }

        IDictionary<string, object> GetProperties();
    }
}
