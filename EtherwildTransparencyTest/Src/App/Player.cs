using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.App;

public sealed class Player(TiledMapTileset tiles, int x, int y)
{
    public Vector2 Position { get; set; } = new(100, 100); // Initial position

    public Rectangle BoundingBox => new((int)Position.X, (int)Position.Y, tiles.TileWidth, tiles.TileHeight);

    public void Draw(SpriteBatch spriteBatch)
    {
        var rect = tiles.GetRegion(x, y).Bounds;
        spriteBatch.Draw(tiles.Texture, Position, rect, Color.White);
    }
}
