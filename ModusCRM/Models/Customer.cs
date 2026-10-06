using ModusCRM.Common;
using System.ComponentModel;

namespace ModusCRM.Models;

public class Customer : ObservableObject
{
    public int Id { get; set; }

    [DisplayName("Имя")]
    public string FirstName
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Фамилия")]
    public string LastName
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Номер телефона")]
    public string PhoneNumber
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Адрес")]
    public Address Address
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Лицевой счет")]
    public string PersonalAccount
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }
}
