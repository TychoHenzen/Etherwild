using System.Runtime.InteropServices;
using EtherwildTransparencyTest.Boilerplate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace EtherwildTransparencyTest;

public class MapRenderer
{
    private readonly GraphicsDevice _graphicsDevice;
    private readonly ContentManager _content;
    private TiledMap _map;
    private TiledMapRenderer _mapRenderer;
    private Rectangle _viewport;
    private Matrix _scaleMatrix;

    public TiledMapTileset Map => _map.Tilesets[0];
    public MapRenderer(GraphicsDevice graphicsDevice, ContentManager content)
    {
        _graphicsDevice = graphicsDevice;
        _content = content;

        // Define the custom viewport size (e.g., 1920x1080)
        // _viewport = new Rectangle(0, 0, 1920, 1080);

        // Initialize the scale matrix
        UpdateScaleMatrix();
    }

    public void LoadContent(string mapAssetName)
    {
        _map = _content.Load<TiledMap>(mapAssetName);
        _mapRenderer = new TiledMapRenderer(_graphicsDevice, _map);
    }

    public void UpdateScaleMatrix()
    {
        float displayScale = GetWindowsDisplayScale();
        // Desired final scale is 2.4x, map scale is 3x, so we need to cancel out the system scale and then scale up by 3x
        float finalScale = 3f / displayScale;
        _scaleMatrix = Matrix.CreateScale(finalScale, finalScale, 1f);
    }

    private float GetWindowsDisplayScale()
    {
        int scaledWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int scaledHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        var devMode = new Devmode();
        EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref devMode);
        uint nativeWidth = devMode.dmPelsWidth;
        uint nativeHeight = devMode.dmPelsHeight;

        float scaleX = (float)nativeWidth / scaledWidth;
        float scaleY = (float)nativeHeight / scaledHeight;

        // Assuming uniform scaling
        return (scaleX + scaleY) / 2;
    }


    [DllImport("user32.dll")]
    private static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref Devmode devMode);

    private const int ENUM_CURRENT_SETTINGS = -1;

    public void Draw()
    {
        // Set the viewport
        // _graphicsDevice.Viewport = new Viewport(_viewport);

        // Draw the map with the scaling matrix
        _mapRenderer.Draw(_scaleMatrix);
    }
}