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

    public ICommand AddCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand EditCommand { get; }

    public ICommand SaveCommand { get; }

    public object SelectedItem { get; set; }

    public event Action EditRequested;

    public List<string> TableNames { get; } = new()
    {
        "Сотрудники",
        "Клиенты"
    };

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

    public IEnumerable CurrentItems
    {
        get
        {
            return SelectedTable switch
            {
                "Сотрудники" => Employees,
                "Клиенты" => Customers,
                _ => null
            };
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
            "Сотрудники" => new Employee { FullName = "Новый сотрудник" },
            "Клиенты" => new Customer { FullName = "Новый клиент" },
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
