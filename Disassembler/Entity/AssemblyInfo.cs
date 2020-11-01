using System.Collections.Generic;
using System.Reflection;

namespace Disassembler.Entity
{
    public class AssemblyInfo
    {
        public AssemblyInfo(Assembly assembly)
        {
            Assembly = assembly;
        }

        public Assembly Assembly { get; set; }
        public List<NamespaceInfo> Namespaces { get; set; } = new List<NamespaceInfo>();
    }
}