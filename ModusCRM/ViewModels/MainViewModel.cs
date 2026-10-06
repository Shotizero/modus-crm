using Microsoft.EntityFrameworkCore;
using ModusCRM.Common;
using ModusCRM.Data;
using ModusCRM.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace ModusCRM.ViewModels;

public class MainViewModel : ObservableObject
{
    private object _editingItem;

    private AppDbContext _context;

    private IList CurrentList => CurrentItems as IList;

    public ObservableCollection<FieldRow> Fields { get; } = new();

    public ObservableCollection<Employee> Employees { get; } = new();

    public ICommand AddCommand { get; }

    public ICommand DeleteCommand { get; }

    public ICommand EditCommand { get; }

    public ICommand CommitCommand { get; }

    public ICommand CancelCommand { get; }

    public object SelectedItem { get; set; }

    public List<string> TableNames { get; } = new()
    {
        "Сотрудники"
    };    

    public IEnumerable CurrentItems
    {
        get
        {
            return SelectedTable switch
            {
                "Сотрудники" => Employees,
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

    public object EditingItem
    {
        get => _editingItem;
        set
        {
            _editingItem = value;
            OnPropertyChanged();
            BuildFields();
        }
    }

    public MainViewModel()
    {
        SelectedTable = TableNames.FirstOrDefault();

        AddCommand = new RelayCommand(_ => AddItem());
        EditCommand = new RelayCommand(_ => EditItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        CommitCommand = new RelayCommand(_ => CommitItem());
        CancelCommand = new RelayCommand(_ => CancelCommitItem());

        try
        {
            _context = new AppDbContext();
            LoadFromDatabase();
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message, "PostgreSQL Error");
            Environment.Exit(ex.HResult);
        }
    }

    private void AddItem()
    {
        if (CurrentList == null)
        {
            return;
        }

        object newItem = SelectedTable switch
        {
            "Сотрудники" => new Employee(),
            _ => null
        };

        EditingItem = newItem;
    }

    private void EditItem()
    {
        if (SelectedItem == null)
        {
            MessageBox.Show("Выберите строку");
            return;
        }

        EditingItem = SelectedItem;
    }

    private void DeleteItem()
    {
        if (CurrentList == null || SelectedItem == null)
        {
            return;
        }

        CurrentList.Remove(SelectedItem);
        SaveChanges(SelectedItem, EntityState.Deleted);
    }

    private void CommitItem()
    {
        if (EditingItem == null || EditingItem is not IEntity e)
        {
            return;
        }

        try
        {
            // Переносим значения из Fields в объект
            foreach (var f in Fields)
            {
                f.Commit();
            }

            // Добавляем в коллекцию, если это новый объект
            if (!CurrentItems.Cast<object>().Contains(EditingItem))
            {
                (CurrentItems as IList)?.Add(EditingItem);
            }

            // Пишем в БД
            var itemState = e.Id == 0 
                ? EntityState.Added 
                : EntityState.Modified;

            SaveChanges(EditingItem, itemState);

            // Закрываем форму, очищаем Fields
            EditingItem = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.ToString()}", "Error");
        }
    }

    private void CancelCommitItem()
    {
        EditingItem = null;
    }

    private void BuildFields()
    {
        Fields.Clear();

        if (_editingItem == null)
        {
            return;
        }

        var props = _editingItem
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite)
            .Where(p => p.Name != "Id");     // Id не редактируем

        foreach (var p in props)
        {
            Fields.Add(new FieldRow(_editingItem, p));
        }
    }

    public void LoadFromDatabase()
    {
        Employees.Clear();

        foreach (var e in _context.Employees.OrderBy(e => e.Id).ToList())
        {
            Employees.Add(e);
        }
    }

    private void SaveChanges(object item, EntityState state)
    {
        if (item == null || item is not Employee e)
        {
            return;
        }

        switch (state)
        {
            case EntityState.Added:
                _context.Employees.Add(e);
                break;
            case EntityState.Modified:
                break;
            case EntityState.Deleted:
                _context.Employees.Remove(e);
                break;
            default: break;
        }

        _context.SaveChanges();
    }
}
