
using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace EtherwildTransparencyTest.Core;

public sealed class TiledMapRendererImpl : IMapRenderer
{
  private TiledMapRenderer _mapRenderer = null!;

  public void Initialize(GraphicsDevice graphicsDevice,TiledMap map)
  {
    _mapRenderer = new TiledMapRenderer(graphicsDevice, map);
  }

  public void LoadContent(GraphicsDevice graphicsDevice, string mapName)
  {
    throw new System.NotImplementedException();
  }

  public void Draw(SpriteBatch spriteBatch, Matrix scaleMatrix)
  {
    _mapRenderer.Draw(scaleMatrix);
  }

}
