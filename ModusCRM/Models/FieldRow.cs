using ModusCRM.Common;
using System.ComponentModel;
using System.Reflection;

namespace ModusCRM.Models;

public class FieldRow : ObservableObject
{
    private object _value;

    public PropertyInfo Property { get; }   // само свойство

    public string Name { get; }             // DisplayName

    public object Source { get; }           // объект, чьё поле правим    

    public object Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
        }
    }

    public FieldRow(object source, PropertyInfo prop)
    {
        Source = source;
        Property = prop;
        Name = prop.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? prop.Name;
        _value = prop.GetValue(source);
    }

    /// <summary>
    /// Записать значение обратно в объект.
    /// </summary>
    public void Commit()
    {
        var type = Nullable.GetUnderlyingType(Property.PropertyType) ?? Property.PropertyType;
        object converted = _value;

        if (_value is string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                converted = type.IsValueType 
                    ? Activator.CreateInstance(type) 
                    : null;
            }
            else
            {
                converted = Convert.ChangeType(
                    s.Replace(',', '.'),
                    type,
                    System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        Property.SetValue(Source, converted);
    }
}