using System.Collections.Generic;

namespace Disassembler.Entity
{
    public class NamespaceInfo
    {
        public NamespaceInfo(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<ClassInfo> Classes { get; set; } = new List<ClassInfo>();
    }
}