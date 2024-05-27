using System.Runtime.InteropServices;
using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EtherwildTransparencyTest.Core;

public sealed class DisplayScaler : IDisplayScaler
{
  public Matrix GetScaleMatrix()
  {
    var displayScale = GetWindowsDisplayScale();
    var finalScale = 3f / displayScale;
    return Matrix.CreateScale(finalScale, finalScale, 1f);
  }

  private static float GetWindowsDisplayScale()
  {
    var scaledWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
    var scaledHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

    var devMode = new Devmode();
    EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);
    var nativeWidth = devMode.dmPelsWidth;
    var nativeHeight = devMode.dmPelsHeight;

    var scaleX = (float)nativeWidth / scaledWidth;
    var scaleY = (float)nativeHeight / scaledHeight;

    return (scaleX + scaleY) / 2;
  }

  [DllImport("user32.dll", CharSet = CharSet.Unicode)]
  private static extern bool EnumDisplaySettings(string? deviceName, int modeNum, ref Devmode devMode);

  private const int ENUM_CURRENT_SETTINGS = -1;
}
