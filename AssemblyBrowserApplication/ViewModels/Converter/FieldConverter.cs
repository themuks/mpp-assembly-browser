using System;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace AssemblyBrowser.ViewModels.Converter
{
    public class FieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fieldInfo = value as FieldInfo;
            var typeName = fieldInfo?.FieldType.Name ?? (value as PropertyInfo)?.PropertyType.Name;
            var fieldName = fieldInfo?.Name ?? (value as PropertyInfo)?.Name;
            var genArguments = fieldInfo?.FieldType.GetGenericArguments() ??
                               (value as PropertyInfo)?.PropertyType.GetGenericArguments();
            return TypenameBuilder.BuildTypename(typeName, genArguments, true) + ' ' + fieldName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}