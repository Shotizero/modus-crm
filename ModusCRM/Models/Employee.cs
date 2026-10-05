using ModusCRM.Common;
using System.ComponentModel;

namespace ModusCRM.Models;

public class Employee : ObservableObject
{
    public int Id { get; set; }

    [DisplayName("Имя сотрудника")]
    public string FullName 
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Должность")]
    public string Position 
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Логин")]
    public string Login 
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    [DisplayName("Пароль")]
    public string Password 
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
}
