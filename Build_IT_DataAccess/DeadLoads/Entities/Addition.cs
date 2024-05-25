using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Build_IT_DataAccess.DeadLoads.Entities
{
    public sealed class Addition
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public double Value { get; init; }
    }
}
