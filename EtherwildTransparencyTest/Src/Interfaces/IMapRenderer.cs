using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Interfaces;

public interface IMapRenderer
{
  void Initialize(GraphicsDevice graphicsDevice, TiledMap map);
  void LoadContent(GraphicsDevice graphicsDevice,string mapName);
  void Draw(SpriteBatch spriteBatch,Matrix scaleMatrix);
}
