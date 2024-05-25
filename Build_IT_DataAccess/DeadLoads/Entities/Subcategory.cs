using System.Collections.Generic;

namespace Build_IT_DataAccess.DeadLoads.Entities
{
    public sealed class Subcategory
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string DocumentName { get; init; }
        public Category Category { get; init; }
        public int CategoryId { get; init; }
        public ICollection<Material> Materials { get; private set; }

        public Subcategory()
        {
            Materials = new HashSet<Material>();
        }
    }
}
