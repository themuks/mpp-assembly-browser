using System.Reflection;
using Disassembler.Entity;
using Disassembler.Service;

namespace AssemblyBrowser.Models
{
    public class DisassemblerModel
    {
        public AssemblyInfo GetAssemblyInfo(Assembly assembly)
        {
            IDisassembler d = new Disassembler.Service.Impl.Disassembler();
            return d.Disassemble(assembly);
        }
    }
}