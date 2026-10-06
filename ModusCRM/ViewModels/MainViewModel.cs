using ModusCRM.Common;
using ModusCRM.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ModusCRM.ViewModels;

public class MainViewModel : ObservableObject
{
    private IList CurrentList => CurrentItems as IList;

    public ObservableCollection<Employee> Employees { get; } = new();

    public ObservableCollection<Customer> Customers { get; } = new();

    public ObservableCollection<Address> Addresses { get; } = new();

    public ICommand AddCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand EditCommand { get; }

    public ICommand SaveCommand { get; }

    public object SelectedItem { get; set; }

    public event Action EditRequested;

    public List<string> TableNames { get; } = new()
    {
        "Сотрудники",
        "Клиенты",
        "Адреса"
    };    

    public IEnumerable CurrentItems
    {
        get
        {
            return SelectedTable switch
            {
                "Сотрудники" => Employees,
                "Клиенты" => Customers,
                "Адреса" => Addresses,
                _ => null
            };
        }
    }

    public string SelectedTable
    {
        get => field;
        set
        {
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentItems));
        }
    }

    public MainViewModel()
    {
        SelectedTable = TableNames.FirstOrDefault();

        AddCommand = new RelayCommand(_ => AddItem());
        EditCommand = new RelayCommand(_ => EditItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
    }

    private void AddItem()
    {
        if (CurrentList == null)
        {
            return;
        }

        object newItem = SelectedTable switch
        {
            "Сотрудники" => new Employee
            {
                FirstName = "Новый",
                LastName = "Сотрудник", 
                Position = "Не указана", 
                Login = "Не указан", 
                Password = "Не указан", 
                PhoneNumber = "Не указан"
            },
            "Клиенты" => new Customer
            {
                FirstName = "Новый",
                LastName = "Клиент", 
                PhoneNumber = "Не указан", 
                PersonalAccount = "Не указан"
            },
            "Адреса" => new Address 
            { 
                Country = "Не указана", 
                Region = "Не указан", 
                Settlement = "Не указан", 
                Street = "Не указана", 
                House = "Не указан" 
            },
            _ => null
        };

        if (newItem != null)
        {
            CurrentList.Add(newItem);
        }
    }

    private void EditItem()
    {
        EditRequested?.Invoke();
    }

    private void DeleteItem()
    {
        if (CurrentList == null || SelectedItem == null)
        {
            return;
        }

        CurrentList.Remove(SelectedItem);
    }

    public void SaveChanges(object item)
    {
        if (item == null)
        {
            return;
        }
    }
}
