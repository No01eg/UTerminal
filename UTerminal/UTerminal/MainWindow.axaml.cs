using Avalonia.Controls;
using Avalonia.Interactivity;

namespace UTerminal;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
  }

  private void btnSend_Click(object sender, RoutedEventArgs e)
  {
    tbText.Text += "You Click Button\r\n";
  }
}