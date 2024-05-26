using System.Runtime.InteropServices;
using EtherwildTransparencyTest.Boilerplate;
using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace EtherwildTransparencyTest;
public class MapRenderer : IMapRenderer
{
    private readonly IMapLoader _mapLoader;
    private readonly IMapRenderer _mapRenderer;
    private readonly IDisplayScaler _displayScaler;
    private Matrix _scaleMatrix;

    public TiledMapTileset Map { get; private set; }

    public MapRenderer(IMapLoader mapLoader, IDisplayScaler displayScaler = null)
    {
        _mapLoader = mapLoader;
        _mapRenderer = new TiledMapRendererImpl();
        _displayScaler = displayScaler ?? new DisplayScaler();

        // Initialize the scale matrix
        UpdateScaleMatrix();
    }

    public void Initialize(GraphicsDevice graphicsDevice, TiledMap map)
    {
        throw new System.NotImplementedException();
    }

    public void LoadContent(GraphicsDevice graphics, string mapAssetName)
    {
        var map = _mapLoader.LoadMap(mapAssetName);
        _mapRenderer.Initialize(graphics,map);
        Map = map.Tilesets[0];
    }

    public void Draw(SpriteBatch spriteBatch, Matrix scaleMatrix)
    {
        _mapRenderer.Draw(spriteBatch,scaleMatrix*_scaleMatrix);
    }

    public void UpdateScaleMatrix()
    {
        _scaleMatrix = _displayScaler.GetScaleMatrix();
    }
}