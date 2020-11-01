using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Disassembler.Entity;
using MethodInfo = System.Reflection.MethodInfo;

namespace Disassembler.Service.Impl
{
    public class Disassembler : IDisassembler
    {
        public AssemblyInfo Disassemble(Assembly assembly)
        {
            var assemblyInfo = new AssemblyInfo(assembly);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (!IsNotCompilerGeneratedAttribute(type)) return assemblyInfo;

                var namespaceInfo = new NamespaceInfo(type.Namespace ?? "<global>");
                if (assemblyInfo.Namespaces.Any(n => n.Name == namespaceInfo.Name) || IsExtenstionClass(type))
                    namespaceInfo = assemblyInfo.Namespaces.First(ns => ns.Name == namespaceInfo.Name);
                else
                    assemblyInfo.Namespaces.Add(namespaceInfo);

                var classInfo = new ClassInfo(type);
                namespaceInfo.Classes.Add(classInfo);
                classInfo.Constructors.AddRange(type.GetConstructors().Where(IsNotCompilerGeneratedAttribute));
                classInfo.Properties.AddRange(type.GetProperties().Where(IsNotCompilerGeneratedAttribute));
                classInfo.Fields.AddRange(type.GetFields().Where(IsNotCompilerGeneratedAttribute));
                foreach (var methodInfo in type.GetMethods())
                    if (IsExtensionMethod(methodInfo))
                    {
                        var extType = methodInfo.GetParameters()[0].ParameterType;
                        var extNamespaceInfo = new NamespaceInfo(extType.Namespace ?? "<global>");
                        if (assemblyInfo.Namespaces.Any(n => n.Name != extNamespaceInfo.Name) &&
                            !IsExtenstionClass(type))
                            assemblyInfo.Namespaces.Add(extNamespaceInfo);
                        else
                            extNamespaceInfo =
                                assemblyInfo.Namespaces.First(n => n.Name == extNamespaceInfo.Name);

                        var extClassInfo = new ClassInfo(extType);
                        if (!extNamespaceInfo.Classes.Any(n => n.ClassType.Name == extClassInfo.ClassType.Name))
                            extNamespaceInfo.Classes.Add(extClassInfo);
                        else
                            extClassInfo = extNamespaceInfo.Classes.First(n =>
                                n.ClassType.Name == extClassInfo.ClassType.Name);

                        if (IsNotCompilerGeneratedAttribute(methodInfo))
                            extClassInfo.Methods.Add(new Entity.MethodInfo(methodInfo));
                    }
                    else
                    {
                        if (IsExtensionMethod(methodInfo)) classInfo.Methods.Add(new Entity.MethodInfo(methodInfo));
                    }
            }

            return assemblyInfo;
        }

        private static bool IsExtenstionClass(Type type)
        {
            return type.IsAbstract && type.IsSealed && type.GetMethods().All(info => IsExtensionMethod(info));
        }

        private static bool IsExtensionMethod(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static bool IsNotCompilerGeneratedAttribute(MemberInfo type)
        {
            var compilerGeneratedAttribute = typeof(CompilerGeneratedAttribute);
            return Attribute.GetCustomAttribute(type, compilerGeneratedAttribute) == null;
        }
    }
}