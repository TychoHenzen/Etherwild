using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Interfaces;

public interface IDrawable
{
  void Draw(SpriteBatch spriteBatch);
}
