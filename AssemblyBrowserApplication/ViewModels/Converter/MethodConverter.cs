using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Disassembler.Entity;

namespace AssemblyBrowser.ViewModels.Converter
{
    public class MethodConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var methodInfo = value as MethodInfo;
            var typeNameGenArgs = TypenameBuilder.BuildTypename(methodInfo.Info.ReturnType.Name,
                methodInfo.Info.ReturnType.GetGenericArguments(), true);
            var nameGenArgs =
                TypenameBuilder.BuildTypename(methodInfo.Info.Name, methodInfo.Info.GetGenericArguments(), false);
            var parameters = '(' + string.Join(",",
                methodInfo.Info.GetParameters().Select(p =>
                    TypenameBuilder.BuildTypename(p.ParameterType.Name, p.ParameterType.GetGenericArguments(), true) +
                    ' ' + p.Name)) + ')';
            var ext = methodInfo.IsExtension ? "(extension) " : null;
            return ext + typeNameGenArgs + ' ' + nameGenArgs + parameters;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}