using System.Collections.Generic;

namespace Build_IT_Data.Models.DeadLoads
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Subcategory> Subcategories { get; private set; }

        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }           
    }
}
