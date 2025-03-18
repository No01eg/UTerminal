using Avalonia.Controls;
using Avalonia.Interactivity;

namespace UTerminal;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    Title += " v1.0.0";
  }

  private void btnSend_Click(object sender, RoutedEventArgs e)
  {
    tbText.Text += $"{InputCmd.Text}\r\n";
  }
}