using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

using System.Runtime.InteropServices;
using System;

namespace UTerminal;

public partial class MainWindow : Window
{
  public MainWindow()
  {
    InitializeComponent();
    Title += " v1.0.0";
    //bool res = Console.CapsLock;
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
        IntPtr display = XOpenDisplay(null);
        if (display == IntPtr.Zero)
        {
          throw new Exception("Не удалось открыть дисплей X.");
        }
        try
        {
            // Проверяем, поддерживается ли XKB
            /*int opcode, eventCode, errorCode;
            if (XkbQueryExtension(display, out opcode, out eventCode, out errorCode) == 0)
            {
              res = true;
                throw new Exception("XKB не поддерживается.");
            }*/

            // Получаем состояние индикаторов
            uint state;
            int result = XkbGetIndicatorState(display, XkbUseCoreKbd, out state);
            if (result != 0)
            {
              res = true;
                throw new Exception("Не удалось получить состояние индикаторов.");
            }
            XCloseDisplay(display);
            return res;

            // Проверяем состояние Caps Lock
            return (state & (1 << 0)) != 0; // 0 - это LED для Caps Lock
        }
        finally
        {
            XCloseDisplay(display);
        }
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
    bool res = KeyboardHelper.IsCapsLockOn();
    if (res)
      tbText.Text += "CapsLock On\r\n";
    else
      tbText.Text += "CapsLock OFF\r\n";
    int a = 10;
    a++;
  }

  private void btnSend_Click(object sender, RoutedEventArgs e)
  {
    tbText.Text += $"{InputCmd.Text}\r\n";
  }
}