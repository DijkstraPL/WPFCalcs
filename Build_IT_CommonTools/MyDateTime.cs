using Build_IT_CommonTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Build_IT_CommonTools
{
    public class MyDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
