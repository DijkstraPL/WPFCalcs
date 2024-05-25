using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CommonTools.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
