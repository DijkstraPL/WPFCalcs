using Build_IT_DataAccess.ScriptInterpreter.Entities.Enums;
using Build_IT_DataAccess.ScriptInterpreter.Entities.Translations;
using System;
using System.Collections.Generic;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class Script
    {
        public int Id { get; set; }
        public int? GroupName { get; set; }
        public ScriptGroup Group { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Added { get; set; }
        public DateTime Modified { get; set; }
        public string AccordingTo { get; set; }
        public string Notes { get; set; }
        public int? PreviousScriptId { get; set; }
        public Script PreviousScript { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<ScriptTag> Tags { get; private set; }
        public ICollection<Parameter> Parameters { get; private set; }
        public ICollection<ScriptTranslation> ScriptTranslations { get; private set; }

        public Script()
        {
            Tags = new HashSet<ScriptTag>();
            Parameters = new HashSet<Parameter>();
            ScriptTranslations = new HashSet<ScriptTranslation>();
        }
    }
}
