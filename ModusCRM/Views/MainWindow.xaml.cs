using ModusCRM.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ModusCRM.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Loaded += (s, e) =>
        {
            if (DataContext is MainViewModel vm)
            {
                vm.EditRequested += BeginEditSelectedRow;
            }
        };
    }

    private void BeginEditSelectedRow()
    {
        if (MainGrid.CurrentCell.Item == null)
        {
            return;
        }

        MainGrid.IsReadOnly = false;
        MainGrid.BeginEdit();
    }

    private void MainGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
    {
        if (e.EditAction != DataGridEditAction.Commit)
        {
            // Cancel — просто возвращаем запрет, ничего не сохраняем
            Dispatcher.BeginInvoke(new Action(() => MainGrid.IsReadOnly = true),
                DispatcherPriority.Background);
            return;
        }

        // Важно: сохраняем в Dispatcher, потому что в момент события
        // объект ещё не до конца обновлён — WPF коммитит значение после
        Dispatcher.BeginInvoke(new Action(() =>
        {
            var item = e.Row.Item;

            if (DataContext is MainViewModel vm)
            {
                vm.SaveChanges(item);
            }

            // Возвращаем запрет на редактирование
            MainGrid.IsReadOnly = true;
        }), DispatcherPriority.Background);
    }

    private void MainGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        if (e.PropertyName == "Id")
        {
            e.Cancel = true;
        }

        if (e.PropertyDescriptor is PropertyDescriptor descriptor)
        {
            e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
        }

        e.Column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        e.Column.MinWidth = 100;
    }
}