using EtherwildTransparencyTest.Interfaces;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Core;

public sealed class TiledMapLoader(ContentManager content) : IMapLoader
{
  public TiledMap LoadMap(string mapAssetName) => content.Load<TiledMap>(mapAssetName);
}
