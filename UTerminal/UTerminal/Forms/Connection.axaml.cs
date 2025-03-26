using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace UTerminal;

public partial class Connection : Window
{
    public Connection()
    {
        InitializeComponent();
    }

  private void Ok_Click(object sender, RoutedEventArgs e)
  {
    Height = 300;
  }

  private void Cancel_Click(object sender, RoutedEventArgs e)
  {
    Height = 150;
  }
}