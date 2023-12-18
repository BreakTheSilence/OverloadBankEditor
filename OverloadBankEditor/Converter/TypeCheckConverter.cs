using System.Globalization;
using System.Windows.Data;

namespace OverloadBankEditor.Converter;

public class TypeCheckConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        
        if (value == null || parameter == null) return false;
        var targetTypeName = parameter.ToString();
        if (targetTypeName == null) return false;
        var classType = value.GetType();
        return classType.Name.Equals(targetTypeName);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}