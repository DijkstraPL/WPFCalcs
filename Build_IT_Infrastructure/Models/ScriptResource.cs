using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Build_IT_Infrastructure.Models
{
    public class ScriptResource
    {
        #region Properties

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TagResource> Tags { get; set; }
        public string GroupName { get; set; }
        public string Author { get; set; }
        public DateTime Added { get; set; }
        public DateTime Modified { get; set; }
        public string AccordingTo { get; set; }
        public string Notes { get; set; }
        public Language DefaultLanguage { get; set; }

        #endregion // Properties

        #region Constructors

        public ScriptResource()
        {
            Tags = new HashSet<TagResource>();
        }

        #endregion // Constructors
    }
}
