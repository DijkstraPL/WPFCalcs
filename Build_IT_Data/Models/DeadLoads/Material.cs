using Build_IT_Data.Units.Enums;
using System.Collections.Generic;

namespace Build_IT_Data.Models.DeadLoads
{
    public class Material
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double MinimumDensity { get; set; }
        public double MaximumDensity { get; set; }
        public LoadUnit Unit { get; set; }
        public string DocumentName { get; set; }
        public string Comments { get; set; }
        public Subcategory Subcategory { get; set; }
        public long SubcategoryId { get; set; }
        public ICollection<MaterialAddition> MaterialAdditions { get; private set; }

        public Material()
        {
            MaterialAdditions = new HashSet<MaterialAddition>();
        }
    }
}
