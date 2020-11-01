using System.Reflection;
using Disassembler.Entity;

namespace Disassembler.Service
{
    public interface IDisassembler
    {
        AssemblyInfo Disassemble(Assembly assembly);
    }
}