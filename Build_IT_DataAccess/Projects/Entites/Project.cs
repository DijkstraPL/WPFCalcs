using Build_IT_DataAccess.Common;
using Build_IT_DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class Project : BaseAuditableEntity
    {
        public string  Name { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
    }
}
