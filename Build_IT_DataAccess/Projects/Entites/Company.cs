using Build_IT_DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class Company : BaseAuditableEntity
    {
        public string Name { get; set; }

        public IList<Project> Projects { get; private set; } = new List<Project>();
    }
}
