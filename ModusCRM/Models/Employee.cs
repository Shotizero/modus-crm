using ModusCRM.Common;
using System.ComponentModel;

namespace ModusCRM.Models;

public class Employee : ObservableObject
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
}
