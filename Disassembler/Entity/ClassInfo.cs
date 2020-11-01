using System;
using System.Collections.Generic;
using System.Reflection;

namespace Disassembler.Entity
{
    public class ClassInfo
    {
        public ClassInfo(Type classType)
        {
            ClassType = classType;
        }

        public Type ClassType { get; set; }
        public List<MethodInfo> Methods { get; set; } = new List<MethodInfo>();
        public List<FieldInfo> Fields { get; set; } = new List<FieldInfo>();
        public List<PropertyInfo> Properties { get; set; } = new List<PropertyInfo>();

        public List<ConstructorInfo> Constructors { get; set; } = new List<ConstructorInfo>();
    }
}