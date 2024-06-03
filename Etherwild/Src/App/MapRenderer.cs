using EtherwildTransparencyTest.Core;
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using IDrawable = EtherwildTransparencyTest.Interfaces.IDrawable;

namespace EtherwildTransparencyTest.App;

public sealed class MapRenderer : IDrawable
{
  private readonly IMapLoader _mapLoader;
  private TiledMapRenderer _mapRenderer;
  private readonly IDisplayScaler _displayScaler;
  private Matrix _scaleMatrix;

  public TiledMapTileset? Map { get; private set; }

  public MapRenderer(IMapLoader mapLoader, IDisplayScaler? displayScaler = null)
  {
    _mapLoader = mapLoader;
    _displayScaler = displayScaler ?? new DisplayScaler();

    // Initialize the scale matrix
    UpdateScaleMatrix();
  }
  public void LoadContent(GraphicsDevice graphicsDevice, string mapName)
  {
    var map = _mapLoader.LoadMap(mapName);
    _mapRenderer = new TiledMapRenderer(graphicsDevice, map);
    Map = map.Tilesets[0];
  }

  public void Draw(SpriteBatch spriteBatch)
  {
    _mapRenderer.Draw(Matrix.Identity);
  }

  private void UpdateScaleMatrix()
  {
    _scaleMatrix = _displayScaler.GetScaleMatrix();
  }
}
