namespace Disassembler.Entity
{
    public class MethodInfo
    {
        public MethodInfo(System.Reflection.MethodInfo info, bool isExtension = false)
        {
            Info = info;
            IsExtension = isExtension;
        }

        public System.Reflection.MethodInfo Info { get; set; }
        public bool IsExtension { get; set; }
    }
}