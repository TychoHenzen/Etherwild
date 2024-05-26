using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;

namespace EtherwildTransparencyTest.Boilerplate;
public class Player
{
    private readonly TiledMapTileset _tileset;
    private Vector2 _position;
    private int _x, _y;

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public Rectangle BoundingBox => 
        new((int)_position.X, (int)_position.Y, _tileset.TileWidth, _tileset.TileHeight);

    public Player(TiledMapTileset tiles, int x, int y)
    {
        _tileset = tiles;
        _x = x;
        _y = y;
        _position = new Vector2(100, 100); // Initial position
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var rect = _tileset.GetRegion(_x, _y).Bounds;
        spriteBatch.Draw(_tileset.Texture, _position, rect, Color.White);
    }
}
