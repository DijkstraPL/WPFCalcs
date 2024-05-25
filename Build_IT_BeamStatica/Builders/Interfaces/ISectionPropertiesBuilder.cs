using Build_IT_BeamStatica.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_BeamStatica.Builders.Interfaces
{
    public interface ISectionPropertiesBuilder
    {
        #region Public_Methods

        SectionData Build();
        bool Validate();

        #endregion // Public_Methods
    }
}
