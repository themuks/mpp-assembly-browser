using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace AssemblyBrowser.ViewModels.Converter
{
    public class ConstructorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var constructorInfo = value as ConstructorInfo;
            var nameGenArgs = TypenameBuilder.BuildTypename(constructorInfo.DeclaringType.Name,
                constructorInfo.DeclaringType.GetGenericArguments(), true);
            var parameters = '(' + string.Join(",",
                constructorInfo.GetParameters().Select(p =>
                    TypenameBuilder.BuildTypename(p.ParameterType.Name, p.ParameterType.GetGenericArguments(), true) +
                    ' ' + p.Name)) + ')';
            return nameGenArgs + parameters;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}