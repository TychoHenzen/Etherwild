// Decompiled with JetBrains decompiler
// Type: OverlayWindow.OverlayGame
// Assembly: OverlayWindow, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 44E23AAA-16CC-4504-B6A0-8D3FB00452AD
// Assembly location: D:\Development\NugetPackages\overlaywindow\1.2.0\lib\net45\OverlayWindow.dll

using Microsoft.Xna.Framework;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace OverlayWindow
{
  public class OverlayGameSelf : Game
  {
    protected GraphicsDeviceManager _graphics;
    private const int GWL_EXSTYLE = -20;
    private const int LWA_ALPHA = 2;
    private const int WS_EX_LAYERED = 0x80000;
    private const int WS_EX_TRANSPARENT = 0x20;
    private const int WS_EX_NOACTIVATE = 0x8000000;
    private IntPtr HWND_TOPMOST = new(-1);
    private const int SWP_NOMOVE = 2;
    private const int SWP_NOSIZE = 1;
    private const int S_OK = 0;

    [DllImport("kernel32.dll")]
    private static extern void SetLastError(uint dwErrCode);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetLayeredWindowAttributes(
      IntPtr hwnd,
      uint crKey,
      byte bAlpha,
      uint dwFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
      IntPtr hWnd,
      IntPtr hWndInsertAfter,
      int X,
      int Y,
      int cx,
      int cy,
      uint uFlags);

    [DllImport("dwmapi.dll")]
    private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref int[] pMarInset);
    public OverlayGameSelf()
    {
      _graphics = new GraphicsDeviceManager(this);
      IntPtr hWnd = Window.Handle;
      SetLastError(0U);
      // Remove WS_EX_TRANSPARENT flag
      if (SetWindowLong(hWnd, GWL_EXSTYLE, GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED) == 0 && Marshal.GetLastWin32Error() != 0)
        throw new Win32Exception(Marshal.GetLastWin32Error());
      if (!SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE))
        throw new Win32Exception(Marshal.GetLastWin32Error());
      if (!SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA))
        throw new Win32Exception(Marshal.GetLastWin32Error());

      IsMouseVisible = true;
      _graphics.IsFullScreen = true;
      _graphics.HardwareModeSwitch = false; // Ensures it uses borderless fullscreen
      Window.IsBorderless = true;
      int[] pMarInset = { -1 };
      int error;
      if ((error = DwmExtendFrameIntoClientArea(Window.Handle, ref pMarInset)) != 0)
        throw new Win32Exception(error);
    }

    protected override void Initialize()
    {
      base.Initialize();

      // Set window to be layered but not transparent to mouse events
      IntPtr hWnd = Window.Handle;
      SetWindowLong(hWnd, GWL_EXSTYLE, (GetWindowLong(hWnd, GWL_EXSTYLE) | WS_EX_LAYERED)); // No WS_EX_TRANSPARENT
      SetLayeredWindowAttributes(hWnd, 0, 255, LWA_ALPHA);
    }
    protected override void Update(GameTime gameTime)
    {
      EnsureTopMost();
      base.Update(gameTime);
    }

    public void EnsureTopMost()
    {
      if (!SetWindowPos(Window.Handle, HWND_TOPMOST, 0, 0, 0, 0, 3U))
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public static int GetScreens() => Screen.AllScreens.Length;

    public static Microsoft.Xna.Framework.Rectangle GetScreenBounds()
    {
      System.Drawing.Rectangle bounds = Screen.PrimaryScreen.Bounds;
      return new Microsoft.Xna.Framework.Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
    }

    public static Microsoft.Xna.Framework.Rectangle GetScreenBounds(int screenIndex)
    {
      System.Drawing.Rectangle bounds = Screen.AllScreens[screenIndex].Bounds;
      return new Microsoft.Xna.Framework.Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
    }

    public static Microsoft.Xna.Framework.Rectangle GetScreenWorkingArea()
    {
      System.Drawing.Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
      return new Microsoft.Xna.Framework.Rectangle(workingArea.X, workingArea.Y, workingArea.Width, workingArea.Height);
    }

    public static Microsoft.Xna.Framework.Rectangle GetScreenWorkingArea(int screenIndex)
    {
      System.Drawing.Rectangle workingArea = Screen.AllScreens[screenIndex].WorkingArea;
      return new Microsoft.Xna.Framework.Rectangle(workingArea.X, workingArea.Y, workingArea.Width, workingArea.Height);
    }

    public static System.Drawing.Rectangle GetVirtualScreenArea()
    {
      return SystemInformation.VirtualScreen;
    }

    public static Size GetVirtualScreenAreaSize()
    {
      System.Drawing.Rectangle virtualScreen = SystemInformation.VirtualScreen;
      int width1 = virtualScreen.Width;
      virtualScreen = SystemInformation.VirtualScreen;
      int left = virtualScreen.Left;
      int width2 = width1 - left;
      virtualScreen = SystemInformation.VirtualScreen;
      int height1 = virtualScreen.Height;
      virtualScreen = SystemInformation.VirtualScreen;
      int top = virtualScreen.Top;
      int height2 = height1 - top;
      return new Size(width2, height2);
    }
  }
}
