using System;
using System.Globalization;
using System.Windows.Data;
using Disassembler.Entity;

namespace AssemblyBrowser.ViewModels.Converter
{
    public class ClassConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var classInfo = value as ClassInfo;
            return TypenameBuilder.BuildTypename(classInfo.ClassType.Name, classInfo.ClassType.GetGenericArguments(),
                true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}