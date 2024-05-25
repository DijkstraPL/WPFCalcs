using System.Collections.Generic;

namespace Build_IT_Data.Models.DeadLoads
{
    public class Subcategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DocumentName { get; set; }
        public Category Category { get; set; }
        public long CategoryId { get; set; }
        public ICollection<Material> Materials { get; private set; }

        public Subcategory()
        {
            Materials = new HashSet<Material>();
        }
    }
}
