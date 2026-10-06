using ModusCRM.Common;
using System.ComponentModel;

namespace ModusCRM.Models;

public class Address : ObservableObject
{
    public int Id { get; set; }

    [DisplayName("Страна")]
    public string Country
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Регион")]
    public string Region
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Населенный пункт")]
    public string Settlement
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Улица")]
    public string Street
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Номер дома")]
    public string House
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }
}
