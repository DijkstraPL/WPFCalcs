using System.Collections.Generic;

namespace Build_IT_DataAccess.DeadLoads.Entities
{
    public sealed class Category
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public ICollection<Subcategory> Subcategories { get; private set; }

        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }           
    }
}
