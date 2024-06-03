using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Interfaces;

public interface IMapLoader
{
  TiledMap LoadMap(string mapAssetName);
}
