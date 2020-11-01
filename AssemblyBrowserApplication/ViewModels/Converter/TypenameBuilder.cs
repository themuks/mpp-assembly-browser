using System;
using System.Linq;

namespace AssemblyBrowser.ViewModels.Converter
{
    public class TypenameBuilder
    {
        public static string BuildTypename(string name, Type[] genericArgs, bool hasDecoratedName)
        {
            if (genericArgs.Length == 0) return name;
            if (hasDecoratedName)
                name = name?.Remove(name.Length - 2);
            name += '<' + string.Join(",",
                genericArgs.Select(a =>
                    a.GetGenericArguments().Length == 0
                        ? a.Name
                        : BuildTypename(a.Name, a.GetGenericArguments(), true))) + '>';
            return name;
        }
    }
}