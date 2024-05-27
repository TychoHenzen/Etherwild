using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.App;

public sealed class MapRenderer : IMapRenderer
{
  private readonly IMapLoader _mapLoader;
  private readonly IMapRenderer _mapRenderer;
  private readonly IDisplayScaler _displayScaler;
  private Matrix _scaleMatrix;

  public TiledMapTileset? Map { get; private set; }

  public MapRenderer(IMapLoader mapLoader, IDisplayScaler? displayScaler = null)
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

  public void LoadContent(GraphicsDevice graphicsDevice, string mapName)
  {
    var map = _mapLoader.LoadMap(mapName);
    _mapRenderer.Initialize(graphicsDevice, map);
    Map = map.Tilesets[0];
  }

  public void Draw(SpriteBatch spriteBatch, Matrix scaleMatrix)
  {
    _mapRenderer.Draw(spriteBatch, scaleMatrix * _scaleMatrix);
  }

  private void UpdateScaleMatrix()
  {
    _scaleMatrix = _displayScaler.GetScaleMatrix();
  }
}
