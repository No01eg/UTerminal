using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.X11;

using System.Runtime.InteropServices;
using System;
using Avalonia.Media;


namespace UTerminal;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    Title += " v1.0.0";
    //bool res = Console.CapsLock;
    Activated += Main_Actived;
  }

  public static class KeyboardHelper
  {
#if LINUX
    [DllImport("libX11.so.6")]
    private static extern IntPtr XOpenDisplay(string display);

    [DllImport("libX11.so.6")]
    private static extern void XCloseDisplay(IntPtr display);

    [DllImport("libX11.so.6")]
    private static extern int XkbQueryExtension(IntPtr display, out int opcode, out int eventCode, out int errorCode);

    [DllImport("libX11.so.6")]
    private static extern int XkbGetIndicatorState(IntPtr display, uint deviceSpec, out uint state);

    // Константа для использования основной клавиатуры
    private const uint XkbUseCoreKbd = 0;

    public static bool IsCapsLockOn()
    {
      bool res = false;
       return false;
    }
  }
#else
    public static bool IsCapsLockOn()
    {
      return Console.CapsLock;
    }
  }
#endif
  private void MainKey_Press(object sender, KeyEventArgs e)
  {
    if (e.Key == Key.CapsLock)
      MainColoredCaps();
  }

  private void MainColoredCaps()
  {
    if (KeyboardHelper.IsCapsLockOn())
    {
      bCaps.BorderBrush = Brushes.Red;
      tbCaps.Foreground = Brushes.Red;
    }
    else
    {
      bCaps.BorderBrush = Brushes.Gray;
      tbCaps.Foreground = Brushes.Gray;
    }
  }

  private void Main_Actived(object sender, EventArgs e)
  {
    MainColoredCaps();
  }

  private void btnSend_Click(object sender, RoutedEventArgs e)
  {
    tbText.Text += $"{InputCmd.Text}\r\n";
  }
}