using Build_IT_DataAccess.Common;
using System.Collections.Generic;

namespace Build_IT_DataAccess.Projects.Entites
{
    public sealed class DeadLoad : BaseAuditableEntity
    {
        public string Name { get; init; }
        public ICollection<DeadLoadLayer> DeadLoadLayers { get; private set; }

        public DeadLoad()
        {
            DeadLoadLayers = new HashSet<DeadLoadLayer>();
        }
    }
}
