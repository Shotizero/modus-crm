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