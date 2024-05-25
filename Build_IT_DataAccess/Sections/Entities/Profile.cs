using Build_IT_DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Sections.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string ProfileName { get; set; }
        public ICollection<ContourPoint> Contours { get; private set; }

        public Profile()
        {
            Contours = new Collection<ContourPoint>();
        }
    }
}
