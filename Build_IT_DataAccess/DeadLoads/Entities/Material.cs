using Build_IT_Data.Units.Enums;
using System.Collections.Generic;

namespace Build_IT_DataAccess.DeadLoads.Entities
{
    public sealed class Material
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public double MinimumDensity { get; init; }
        public double MaximumDensity { get; init; }
        public LoadUnit Unit { get; init; }
        public string DocumentName { get; init; }
        public string Comments { get; init; }
        public Subcategory Subcategory { get; init; }
        public int SubcategoryId { get; init; }
        public ICollection<MaterialAddition> MaterialAdditions { get; private set; }

        public Material()
        {
            MaterialAdditions = new HashSet<MaterialAddition>();
        }
    }
}
