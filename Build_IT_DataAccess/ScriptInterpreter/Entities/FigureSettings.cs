using System.IO;
using System.Linq;

namespace Build_IT_DataAccess.ScriptInterpreter.Entities
{
    public class FigureSettings
    {
        public int MaxBytes { get; set; }
        public string[] AcceptedFileTypes { get; set; }
                
        public bool IsSupported(string fileName)
            => AcceptedFileTypes.Any(aft => aft == Path.GetExtension(fileName).ToLower());
    }
}
